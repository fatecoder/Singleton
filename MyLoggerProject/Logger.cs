using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLoggerProject
{
    class Logger
    {
        private static Logger myloginstance;
        private Logger() { }

        private TxtManager manager = TxtManager.GetMyInstance();

        public static Logger GetMyInstance()
        {
            if (myloginstance == null)
            {
                myloginstance = new Logger();
            }
            return myloginstance;
        }

        public void LogInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            manager.WriteInFile($"[INFO] - {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void LogWarn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            manager.WriteInFile($"[WARNING] - {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            manager.WriteInFile($"[ERROR] - {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
