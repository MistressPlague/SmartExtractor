using SharpCompress.Common;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCompress.Archives;

namespace SmartExtractor.Utilities
{
    internal static class BetterExtractionMethods
    {
        /// <summary>
        /// Extract to specific directory, retaining filename
        /// </summary>
        public static void WriteEntryToDirectory(
            IEntry entry,
            string destinationDirectory,
            ExtractionOptions? options,
            Action<string, ExtractionOptions?> write,
            string overrideFileName = null
        )
        {
            string destinationFileName;
            string fullDestinationDirectoryPath = Path.GetFullPath(destinationDirectory);

            //check for trailing slash.
            if (
                fullDestinationDirectoryPath[fullDestinationDirectoryPath.Length - 1]
                != Path.DirectorySeparatorChar
            )
            {
                fullDestinationDirectoryPath += Path.DirectorySeparatorChar;
            }

            if (!Directory.Exists(fullDestinationDirectoryPath))
            {
                throw new ExtractionException(
                    $"Directory does not exist to extract to: {fullDestinationDirectoryPath}"
                );
            }

            options ??= new ExtractionOptions() { Overwrite = true };

            string file = Path.GetFileName(overrideFileName);
            if (options.ExtractFullPath)
            {
                string folder = Path.GetDirectoryName(overrideFileName)!;
                string destdir = Path.GetFullPath(Path.Combine(fullDestinationDirectoryPath, folder));

                if (!Directory.Exists(destdir))
                {
                    if (!destdir.StartsWith(fullDestinationDirectoryPath, StringComparison.Ordinal))
                    {
                        throw new ExtractionException(
                            "Entry is trying to create a directory outside of the destination directory."
                        );
                    }

                    Directory.CreateDirectory(destdir);
                }
                destinationFileName = Path.Combine(destdir, file);
            }
            else
            {
                destinationFileName = Path.Combine(fullDestinationDirectoryPath, file);
            }

            if (!entry.IsDirectory)
            {
                destinationFileName = Path.GetFullPath(destinationFileName);

                if (
                    !destinationFileName.StartsWith(
                        fullDestinationDirectoryPath,
                        StringComparison.Ordinal
                    )
                )
                {
                    throw new ExtractionException(
                        "Entry is trying to write a file outside of the destination directory."
                    );
                }
                write(destinationFileName, options);
            }
            else if (options.ExtractFullPath && !Directory.Exists(destinationFileName))
            {
                Directory.CreateDirectory(destinationFileName);
            }
        }

        public static void WriteToDirectory(
            this IArchiveEntry entry,
            string destinationDirectory,
            ExtractionOptions? options = null,
            string overrideFileName = null
        ) =>
            WriteEntryToDirectory(
                entry,
                destinationDirectory,
                options,
                entry.WriteToFile,
                overrideFileName
            );

        public static void WriteToFile(
            IArchiveEntry entry,
            string destinationFileName,
            ExtractionOptions? options = null
        ) =>
            WriteEntryToFile(
                entry,
                destinationFileName,
                options,
                (x, fm) =>
                {
                    using var fs = File.Open(destinationFileName, fm);
                    entry.WriteTo(fs);
                }
            );

        public static void WriteEntryToFile(
            IEntry entry,
            string destinationFileName,
            ExtractionOptions? options,
            Action<string, FileMode> openAndWrite
        )
        {
            if (entry.LinkTarget != null)
            {
                if (options?.WriteSymbolicLink is null)
                {
                    throw new ExtractionException(
                        "Entry is a symbolic link but ExtractionOptions.WriteSymbolicLink delegate is null"
                    );
                }
                options.WriteSymbolicLink(destinationFileName, entry.LinkTarget);
            }
            else
            {
                FileMode fm = FileMode.Create;
                options ??= new ExtractionOptions() { Overwrite = true };

                if (!options.Overwrite)
                {
                    fm = FileMode.CreateNew;
                }

                openAndWrite(destinationFileName, fm);
                entry.PreserveExtractionOptions(destinationFileName, options);
            }
        }
    }
}

internal static class BetterEntryExtensions
{
    internal static void PreserveExtractionOptions(
        this IEntry entry,
        string destinationFileName,
        ExtractionOptions options
    )
    {
        if (options.PreserveFileTime || options.PreserveAttributes)
        {
            var nf = new FileInfo(destinationFileName);
            if (!nf.Exists)
            {
                return;
            }

            // update file time to original packed time
            if (options.PreserveFileTime)
            {
                if (entry.CreatedTime.HasValue)
                {
                    nf.CreationTime = entry.CreatedTime.Value;
                }

                if (entry.LastModifiedTime.HasValue)
                {
                    nf.LastWriteTime = entry.LastModifiedTime.Value;
                }

                if (entry.LastAccessedTime.HasValue)
                {
                    nf.LastAccessTime = entry.LastAccessedTime.Value;
                }
            }

            if (options.PreserveAttributes)
            {
                if (entry.Attrib.HasValue)
                {
                    nf.Attributes = (FileAttributes)
                        System.Enum.ToObject(typeof(FileAttributes), entry.Attrib.Value);
                }
            }
        }
    }
}
