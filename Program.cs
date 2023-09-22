using System;
using System.IO;
using System.Text;
using Sync.Helper;
using Sync.Model;

namespace Sync
{
    class Program
    {
        private FileComparer comparer;

        static void Main(string[] args)
        {
            Program app = new Program();


            int offset = 0;

 
            while (args[offset].StartsWith("-"))
            {
                app.processOptions(args[offset]);
                offset++;
            }

            DirectoryInfo source = new DirectoryInfo(args[offset]);
            DirectoryInfo destination = new DirectoryInfo(args[offset + 1]);
            app.init();
            app.sync(source, destination);
        }

        public void init()
        {
            comparer = new FileSizeComparer();
        }

        public void sync(DirectoryInfo source, DirectoryInfo destination)
        {
            Folder folderSource = new Folder(source.FullName);
            Folder folderDestination = new Folder(destination.FullName);

            if (!folderSource.exists())
            {
                using (StreamWriter w = File.AppendText("D:\\log.txt"))
                {
                  
                    Log.LogFile(String.Format("source folder {0} does not exist", source.FullName), w);
                }
                // Log.log("source folder {0} does not exist");
                return;
            }
            folderSource.sync(folderDestination, comparer);
        }

        public void processOptions(string option)
        {
            if (option.ToLower() == "-d")
            {
                CurrentMode.TransferMode = Mode.DryRun;
            }
            else if (option.ToLower() == "-s")
            {
                CurrentMode.CompareMode = Mode.Ignore_File_Size;
            }
            else if (option.ToLower() == "-v")
            {
                CurrentMode.LogMode = Mode.Verbose;
            }
        }
    }
}
