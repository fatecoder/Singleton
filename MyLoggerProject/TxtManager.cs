using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyLoggerProject
{
    class TxtManager
    {
        private static TxtManager mytminstance;
        private string path = @"C:\Users\lenovo\Desktop\filelogs.txt";
        private TxtManager() { }

        public static TxtManager GetMyInstance()
        {
            if (mytminstance == null)
            {
                mytminstance = new TxtManager();
            }
            return mytminstance;
        }

        public void OpenFile()
        {

        }

        public void CloseFile()
        { 
        }

        public void WriteInFile(string message)
        {
            StreamWriter stream = File.AppendText(path);
            stream.WriteLine(message);
            stream.Close();
        }

        public string[] ReadFromFile()
        {
            string[] lines = File.ReadAllLines(path);
            return lines;
        }
    }
}
