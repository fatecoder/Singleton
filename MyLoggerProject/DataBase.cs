using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLoggerProject
{
    class DataBase
    {
        public static DataBase mydbinstace;
        private static MySqlConnection connection;

        private DataBase() 
        {
            connection = new MySqlConnection("Server=localhost;Database=test;Port=3306;User Id=root;Password=''");
        }

        public static DataBase GetMyInstance()
        {
            if (mydbinstace == null)
            {
                mydbinstace = new DataBase();
            }
            return mydbinstace;
        }

        public void DBOpenConnection()
        {
            connection.Open();
        }

        public void DBExecReadCommand(string mycommand)
        {
            MySqlCommand command = new MySqlCommand(mycommand, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["description"]);
            }
            command.Dispose();
        }

        public void DBInsert(string message)
        {
            MySqlCommand command = new MySqlCommand($"INSERT INTO log (description, date) VALUES ('{message}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')", connection);
            command.ExecuteNonQuery();
        }

        public void DBCloseConnection()
        {
            connection.Close();
        }
    }
}
