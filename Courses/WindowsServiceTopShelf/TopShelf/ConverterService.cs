using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf.Logging;

namespace TopShelfProject
{
    public class ConverterService
    {
        private FileSystemWatcher _watcher;
        private static readonly LogWriter _log = HostLogger.Get<ConverterService>();

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
            _log.InfoFormat("Starting conversion of '{0}'", e.FullPath);

            if(e.FullPath.Contains("bad_in"))
            {
                throw new NotSupportedException("Cannot Convert");
            }


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

        public bool Pause()
        {
            _watcher.EnableRaisingEvents = false;
            return true;
        }


        public bool Continue()
        {
            _watcher.EnableRaisingEvents = true;
            return true;
        }

        public void CustomCommand(int commandNumber)
        {
            _log.InfoFormat("Hey, I got the command number '{0}'", commandNumber);
        }
    }
}
