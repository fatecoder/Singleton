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

        //Constructor de la clase
        private Logger() { }

        //Instancia de la clase TxtManager para manejar el archivo txt
        private TxtManager manager = TxtManager.GetMyInstance();

        //Método para obtener una única instancia de la clase Logger
        public static Logger GetMyInstance()
        {
            //Comprobacion de la existencia de la instancia de la clase, si no ha sido instanciada, crea una nueva instancia
            if (myloginstance == null)
            {
                myloginstance = new Logger();
            }
            //Retorna la instancia ya sea que se haya creado, o que ya exista
            return myloginstance;
        }

        //Función para imprimir en consola logs de tipo info
        public void LogInfo(string message)
        {
            //Cambiamos el color de las letras
            Console.ForegroundColor = ConsoleColor.Cyan;
            //Imprimimos el mensaje
            Console.WriteLine(message);
            //Guardamos el mensaje en el txt
            manager.WriteInFile($"[INFO] - {message}");
            //Regresamos el color blanco a las letras
            Console.ForegroundColor = ConsoleColor.White;
        }

        //Función para imprimir en consola logs de tipo warning
        public void LogWarn(string message)
        {
            //Cambiamos el color de las letras
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Imprimimos el mensaje
            Console.WriteLine(message);
            //Guardamos el mensaje en el txt
            manager.WriteInFile($"[WARNING] - {message}");
            //Regresamos el color blanco a las letras
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void LogError(string message)
        {
            //Cambiamos el color de las letras
            Console.ForegroundColor = ConsoleColor.Red;
            //Imprimimos el mensaje
            Console.WriteLine(message);
            //Guardamos el mensaje en el txt
            manager.WriteInFile($"[ERROR] - {message}");
            //Regresamos el color blanco a las letras
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
