using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sync.Helper
{
    class Log
    {

        public static void LogFile(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }
        public static void trace(string message, params object[] objects)
        {
            if (CurrentMode.LogMode == Mode.Verbose)
                Console.WriteLine(message, objects);
        }
    }
}
