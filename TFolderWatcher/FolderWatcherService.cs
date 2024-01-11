using System;
using System.IO;
using System.ServiceProcess;

namespace FolderWatcherService
{
    public partial class FolderWatcherService : ServiceBase
    {
        private string folderPath = @"C:\Path\To\Your\Folder";
        private string logFilePath = @"C:\Path\To\Your\Log\File.txt";

        private FileSystemWatcher watcher;


        protected override void OnStart(string[] args)
        {
            InitializeWatcher();
        }

        protected override void OnStop()
        {
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();
        }

        private void InitializeWatcher()
        {
            watcher = new FileSystemWatcher(folderPath);
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            watcher.Changed += OnFileChanged;
            watcher.Created += OnFileCreated;
            watcher.Deleted += OnFileDeleted;
            watcher.Renamed += OnFileRenamed;
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            LogChange($"File Changed: {e.FullPath}");
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            LogChange($"File Created: {e.FullPath}");
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            LogChange($"File Deleted: {e.FullPath}");
        }

        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            LogChange($"File Renamed: {e.OldFullPath} to {e.FullPath}");
        }

        private void LogChange(string logMessage)
        {
            string logEntry = $"{DateTime.Now}: {logMessage}\n";
            File.AppendAllText(logFilePath, logEntry);
        }
    }
}
