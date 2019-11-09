using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLoggerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = Logger.GetMyInstance();
            logger.LogInfo("LOG INFORMATION");
            logger.LogWarn("LOG WARNING");
            logger.LogError("LOG ERROR");

            DataBase db = DataBase.GetMyInstance();
            db.DBOpenConnection();            

            TxtManager manager = TxtManager.GetMyInstance();
            var lines = manager.ReadFromFile();

            foreach (var line in lines)
            {
                db.DBInsert(line);
            }

            db.DBExecReadCommand("SELECT * FROM log");
            db.DBCloseConnection();
            Console.ReadKey();
        }
    }
}
