using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace SmartExtractor.Special_Types
{
    internal class UnityPackage
    {
        internal static (Dictionary<string, (IArchiveEntry, IArchiveEntry)>, (FileStream, string)?) GetUnityPackageEntries(IArchive archive)
        {
            var archtempTAR = archive.Entries.First();

            var tempfile = $"{Path.GetTempPath()}\\{Path.GetRandomFileName()}";
            
            var data = File.Create(tempfile);
            archtempTAR.WriteTo(data);
            data.Position = 0;
            
            var tempArchive = ArchiveFactory.Open(data); // open tar stream as archive
            
            var entryfiles = tempArchive.Entries.Where(entry => entry.IsDirectory).Select(a => tempArchive.Entries.Where(b => !b.IsDirectory && b.Key.StartsWith(a.Key)).ToArray()).ToArray();

            // parent path of entry | target path of file including filename from pathname file
            var entries = entryfiles.ToDictionary(
                files => files.First(f => Path.GetFileName(f.Key) is "pathname").GetFirstLine().Replace('/', Path.DirectorySeparatorChar), // full path
                files => (files.FirstOrDefault(f => Path.GetFileName(f.Key) is "asset"), files.First(f => Path.GetFileName(f.Key) is "asset.meta"))).OrderByDescending(a => a.Key).ToDictionary(a => a.Key, a => a.Value);
            
            //MessageBox.Show("Extracting " + entries.Count + " files.");

            return (entries, (data, tempfile));
        }
    }

    internal static class UnityPackageExt
    {
        internal static (List<(string, IArchiveEntry)>, (FileStream, string)?) GetEntries(this IArchive archive, string path)
        {
            if (Path.GetExtension(path).ToLower() == ".unitypackage")
            {
                var list = new List<(string, IArchiveEntry)>();

                var data = UnityPackage.GetUnityPackageEntries(archive);

                foreach (var entry in data.Item1)
                {
                    var target = entry.Key;
                    if (entry.Value.Item1 != null) // assume item2 is meta for this file
                    {
                        list.Add((target, entry.Value.Item1));
                    }
                    
                    list.Add(($"{target}.meta", entry.Value.Item2));
                }
                
                return (list, data.Item2);
            }

            return (archive.Entries.Select(o => (o.Key, o)).ToList(), null);
        }
        
        internal static string GetFirstLine(this IArchiveEntry entry)
        {
            var path = string.Empty;
            using (var sr = new StreamReader(entry.OpenEntryStream()))
            {
                path = sr.ReadLine();
            }

            return path;
        }

        internal static long GetCRC(this IArchiveEntry entry)
        {
            if (entry.Crc == 0)
            {
                // calculate
                var tempfile = $"{Path.GetTempPath()}\\{Path.GetRandomFileName()}";
            
                var data = File.Create(tempfile);
                entry.WriteTo(data);
                data.Position = 0;

                return Utilities.Utilities.CalculateCRC32(data);
            }
            
            return entry.Crc;
        }
    }
}
