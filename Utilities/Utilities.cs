using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartExtractor.Utilities
{
    internal class Utilities
    {
        /// <summary>
        /// Sets file type association for the current user in the Windows Registry
        /// </summary>
        /// <param name="extension">File extension without the dot (e.g., "zip", not ".zip")</param>
        /// <param name="typeLabel">Descriptive label for the file type</param>
        /// <param name="openWith">Full path to the application to open the file</param>
        /// <param name="executableName">Name of the executable (with .exe)</param>
        /// <exception cref="ArgumentException">Thrown when input parameters are invalid</exception>
        public static void SetFileTypeAssociationCurrentUser(
            string extension,
            string typeLabel,
            string openWith,
            string executableName)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(extension))
                throw new ArgumentException("Extension cannot be null or empty", nameof(extension));

            if (string.IsNullOrWhiteSpace(typeLabel))
                throw new ArgumentException("Type label cannot be null or empty", nameof(typeLabel));

            if (string.IsNullOrWhiteSpace(openWith) || !File.Exists(openWith))
                throw new ArgumentException("Invalid application path", nameof(openWith));

            if (string.IsNullOrWhiteSpace(executableName))
                throw new ArgumentException("Executable name cannot be null or empty", nameof(executableName));

            try
            {
                // Ensure the extension doesn't start with a dot
                extension = extension.TrimStart('.');

                // Base registry path for current user file type associations
                string baseKeyPath = $@"Software\Classes\.{extension}";
                string fileTypeKeyPath = $@"Software\Classes\{typeLabel}";

                using (RegistryKey baseKey = Registry.CurrentUser.CreateSubKey(baseKeyPath))
                {
                    baseKey?.SetValue("", typeLabel);
                }

                using (RegistryKey fileTypeKey = Registry.CurrentUser.CreateSubKey(fileTypeKeyPath))
                {
                    fileTypeKey?.SetValue("", typeLabel);

                    // Set default icon (optional)
                    using (RegistryKey iconKey = fileTypeKey?.CreateSubKey("DefaultIcon"))
                    {
                        iconKey?.SetValue("", $"{openWith},0");
                    }

                    // Set shell open command
                    using (RegistryKey shellKey = fileTypeKey?.CreateSubKey(@"shell\open\command"))
                    {
                        shellKey?.SetValue("", $"\"{openWith}\" \"%1\"");
                    }
                }

                // Notify system of changes
                SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                System.Diagnostics.Debug.WriteLine($"Error setting file association: {ex.Message}");
                throw; // Optional: re-throw or handle more gracefully
            }
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        public static uint CalculateCRC32(Stream data)
        {
            #region CRC32 Table

            var crc32Table = new uint[256];
            const uint polynomial = 0xEDB88320;

            for (uint i = 0; i < 256; i++)
            {
                var crctemp = i;
                for (var j = 0; j < 8; j++)
                {
                    if ((crctemp & 1) == 1)
                    {
                        crctemp = (crctemp >> 1) ^ polynomial;
                    }
                    else
                    {
                        crctemp >>= 1;
                    }
                }

                crc32Table[i] = crctemp;
            }

            #endregion

            var crc = 0xFFFFFFFF;

            var buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = data.Read(buffer, 0, buffer.Length)) > 0)
            {
                for (var i = 0; i < bytesRead; i++)
                {
                    crc = (crc >> 8) ^ crc32Table[(crc & 0xFF) ^ buffer[i]];
                }
            }

            return crc ^ 0xFFFFFFFF;
        }

        public static uint CalculateCRC32(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return CalculateCRC32(fs);
            }
        }
    }
}