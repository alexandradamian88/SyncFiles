using System;
using System.IO;
using Sync.Helper;

namespace Sync.Model
{
    class Folder
    {
        public string path { get; private set; }

        public FileCopy fileCopy;

        public Folder(string path)
        {
            this.path = path;
        }

        public void createIfNoneExist()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public FileInfo findFile(string name)
        {
            FileInfo file = new FileInfo(Path.Combine(path, name));
            return file;
        }

        public void sync(Folder destination, FileComparer comparer)
        {
            destination.createIfNoneExist();
            destination.refresh();
            DirectoryInfo folder = new DirectoryInfo(path);
            if ((folder.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
            {
                Console.WriteLine("folder {0} is hidden", folder.FullName);
                Log.trace("folder {0} is hidden", folder.FullName);
                using (StreamWriter w = File.AppendText("D:\\log.txt"))
                {

                    Log.LogFile(String.Format("folder {0} is hidden", folder.FullName), w);
                }
                return;
            }
            folder.Refresh();
            if (!folder.Exists)
            {
                Console.WriteLine("{0} does not exist", folder.FullName);
                using (StreamWriter w = File.AppendText("D:\\log.txt"))
                {

                    Log.LogFile(String.Format("{0} does not exist", folder.FullName), w);
                }
                return;
            }
            foreach (FileInfo file in folder.GetFiles())
            {
                if ((file.Attributes & FileAttributes.Hidden)== FileAttributes.Hidden)
                {
                    Log.trace("{0} is hidden", file.FullName);
                    using (StreamWriter w = File.AppendText("D:\\log.txt"))
                    {

                        Log.LogFile(String.Format("{0} is hidden", file.FullName), w);
                    }
                    continue;
                }
                FileInfo destFile = destination.findFile(file.Name);
                if (comparer.match(file, destFile))
                {
                    if (CurrentMode.TransferMode == Mode.Normal)
                    {
                        Log.trace("{0} ===> {1} === exist", file.FullName, destFile.FullName);

                    }
                    continue ;
                }
                File.Copy(file.FullName, destFile.FullName);
            }

            foreach (DirectoryInfo child in folder.GetDirectories())
            {
                string newPath = Path.Combine(destination.path, child.Name);
                Folder childDestination = new Folder(newPath);
                Folder childSource = new Folder(child.FullName);
                childSource.sync(childDestination, comparer);
                Console.WriteLine("{0} was copied successfully", child.FullName);
                Log.trace("{0} was copied successfully", child.FullName);
                using (StreamWriter w = File.AppendText("D:\\log.txt"))
                {

                    Log.LogFile(String.Format("{0} was copied successfully", child.FullName), w);
                }

            }
        }

        public void refresh()
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            folder.Refresh();
        }

        public bool exists()
        {
            return Directory.Exists(path);                
        }

    }
}
