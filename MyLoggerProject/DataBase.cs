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
            connection = new MySqlConnection("Server=localhost;Database=test;Port=3306;User Id=root;Password='';");
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

        private void DBExecReadCommand(string mycommand)
        {
            MySqlCommand command = new MySqlCommand(mycommand, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string s = string.Empty;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    s += $"{reader[i].ToString()}";
                    if (i != reader.FieldCount - 1)
                    {
                        s += " - ";
                    }
                }
                Console.WriteLine(s);
            }
            command.Dispose();
        }

        public void ExecCommand(string s)
        {
            var commandsarray = s.Split(';');
            foreach (var line in commandsarray)
            {
                try
                {
                    if (line.ToLower().Contains("select") || line.ToLower().Contains("show") || line.ToLower().Contains("describe"))
                    {
                        DBExecReadCommand(line);
                        Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - -");
                    }
                    else
                    {
                        DBOtherCommand(line);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"There is a problem with your syntax. {e.ToString()}");
                }
            }

        }

        private void DBOtherCommand(string message)
        {
            MySqlCommand command = new MySqlCommand(message, connection);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public void DBCloseConnection()
        {
            connection.Close();
        }
    }
}
