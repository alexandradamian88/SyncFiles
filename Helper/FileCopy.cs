using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Sync.Helper
{
    class FileCopy
    {
        public void copy(FileInfo source, FileInfo destination)
        {
           
            using (StreamWriter w = File.AppendText("D:\\log.txt"))
            {

                Log.LogFile(source.FullName + " ===> " + destination.FullName + " === copy", w);
            }
            if (CurrentMode.TransferMode == Mode.Normal)
            {
                source.CopyTo(destination.FullName, true);
            }
        }
    }
}
