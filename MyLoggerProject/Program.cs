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
            db.ExecCommand("CREATE TABLE IF NOT EXISTS log (id INT NOT NULL AUTO_INCREMENT, PRIMARY KEY(id), description VARCHAR(200), date DATETIME)");
            TxtManager manager = TxtManager.GetMyInstance();
            var lines = manager.ReadFromFile();

            foreach (var line in lines)
            {
                db.ExecCommand($"INSERT INTO log (description, date) VALUES ('{line}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
            }

            db.ExecCommand("SELECT * FROM log; SHOW tables; DESCRIBE log");
            db.DBCloseConnection();
            Console.ReadKey();
        }
    }
}
