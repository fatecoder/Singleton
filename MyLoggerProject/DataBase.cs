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
        private static DataBase mydbinstace;
        private static MySqlConnection connection;

        //Constructor de la clase, dentro se hace la instancia para la conexión a mysql
        private DataBase() 
        {
            //Instancia de conexión a mysql
            connection = new MySqlConnection("Server=localhost;Database=test;Port=3306;User Id=root;Password='';");
        }

        //Método para obtener una única instancia de la clase DataBase
        public static DataBase GetMyInstance()
        {
            //Comprobacion de la existencia de la instancia de la clase, si no ha sido instanciada, crea una nueva instancia
            if (mydbinstace == null)
            {
                mydbinstace = new DataBase();
            }
            //Retorna la instancia ya sea que se haya creado, o que ya exista
            return mydbinstace;
        }

        //Abre la conexión de mysql
        public void DBOpenConnection()
        {
            connection.Open();
        }

        //Función para ejecutar comandos de lectura únicamente (consultas)
        private void DBExecReadCommand(string mycommand)
        {
            //Se crea una instancia para ejecutar comandos con base en la conexión mysql
            MySqlCommand command = new MySqlCommand(mycommand, connection);
            //Se ejecuta el comando y se guarda en reader lo que se obtenga
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                //Se inicializa una variable string para guardar lo que se obtenga del reader
                string s = string.Empty;
                //Vamos leyendo todo lo que contiene en el reader con límite en la cantidad de columnas de la tabla
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    //Concantenamos a la variable string lo que obtenemos
                    s += $"{reader[i].ToString()}";
                    //Si la columna NO es la última que se lee, se concantena un guión medio, para separar los datos de cada tabla
                    if (i != reader.FieldCount - 1)
                    {
                        s += " - ";
                    }
                }
                //Se imprime cada línea de la tabla
                Console.WriteLine(s);
            }
            //Una vez terminado, se deshecha el comando
            command.Dispose();
        }

        //Función para ejecutar cualquier tipo de comando ya sean SELECT, UPDATE, SHOW, DELETE, etc
        //Recibe un string, haciendo posible la ejecución de varias consultas al mismo tiempo, separadas por punto y coma
        public void ExecCommand(string s)
        {
            //Se hace un split de los comandos introducidos
            var commandsarray = s.Split(';');
            foreach (var line in commandsarray)
            {
                try
                {
                    //Si alguno de los comandos es de tipo consulta (select, show, describe), entra en el if
                    if (line.ToLower().Contains("select") || line.ToLower().Contains("show") || line.ToLower().Contains("describe"))
                    {
                        //Y ejecuta nuestra función para realizar comandos de lectura únicamente
                        DBExecReadCommand(line);
                        Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - -");
                    }
                    else
                    {
                        //En caso de no se comando de lectura, se ejecuta esta función que se encargará de realizar la operación introducida
                        DBOtherCommand(line);
                    }
                }
                catch (Exception e)
                {
                    //En caso de algún error se notifica en la consola
                    Console.WriteLine($"There is a problem with your syntax. {e.ToString()}");
                }
            }

        }

        //Función para ejecutar todo tipos de comando; no se imprime nada en pantalla
        private void DBOtherCommand(string message)
        {
            //Se crea una instancia para ejecutar comandos con base en la conexión mysql
            MySqlCommand command = new MySqlCommand(message, connection);
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Una vez terminado, se deshecha el comando
            command.Dispose();
        }

        //Método para cerrar la conexión a la base de datos mysql
        public void DBCloseConnection()
        {
            connection.Close();
        }
    }
}
