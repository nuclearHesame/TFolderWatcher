using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFolderWatcher
{
    using System;
    using System.IO;

    using System;
    using System.IO;

    class Program
    {
        static void Main()
        {
            string folderPath = @"D:\Music";
            string logFilePath = @"D:\Music\LogChanges.txt";

            FileSystemWatcher watcher = new FileSystemWatcher(folderPath);
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            //watcher.Changed += (sender, e) => LogChange($"File Changed: {e.FullPath}", logFilePath);
            watcher.Created += (sender, e) => LogChange($"File Created: {e.FullPath}", logFilePath);
            watcher.Deleted += (sender, e) => LogChange($"File Deleted: {e.FullPath}", logFilePath);
            watcher.Renamed += (sender, e) => LogChange($"File Renamed: {e.OldFullPath} to {e.FullPath}", logFilePath);

            Console.WriteLine("Press ENTER to exit.");

            // still runnig to press key
            while (true)
            {
                string userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                    break;
            }
        }

        private static void LogChange(string logMessage, string logFilePath)
        {
            // string logEntry = $"{DateTime.Now}: {logMessage}\n";
            File.AppendAllText(logFilePath, logMessage);
            Console.WriteLine(logMessage);
        }
    }


}
