using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShelfProject
{
    public class ConverterService
    {
        private FileSystemWatcher _watcher;

        public bool Start()
        {
            _watcher = new FileSystemWatcher(@"c:\temp\a", "*_in.txt");

            _watcher.Created += FileCreated;

            _watcher.IncludeSubdirectories = false;

            _watcher.EnableRaisingEvents = true;

            return true;
        }

        private void FileCreated(object sender, FileSystemEventArgs e)
        {
            string content = File.ReadAllText(e.FullPath);

            string upperContent = content.ToUpperInvariant();

            var dir = Path.GetDirectoryName(e.FullPath);

            var convertedFileName = Path.GetFileName(e.FullPath) + ".converted";

            var convertedPath = Path.Combine(dir, convertedFileName);

            File.WriteAllText(convertedPath, upperContent);
        }

        public bool Stop()
        {
            _watcher.Dispose();

            return true;
        }
    }
}
