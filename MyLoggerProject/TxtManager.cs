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
        
        //Ruta en donde se guardará el archivo
        private string path = @"C:\Users\lenovo\Desktop\filelogs.txt";
        
        //Constructor de la clase
        private TxtManager() { }

        //Método para obtener una única instancia de la clase TxtManager
        public static TxtManager GetMyInstance()
        {
            //Comprobacion de la existencia de la instancia de la clase, si no ha sido instanciada, crea una nueva instancia
            if (mytminstance == null)
            {
                mytminstance = new TxtManager();
            }
            //Retorna la instancia ya sea que se haya creado, o que ya exista
            return mytminstance;
        }

        public void OpenFile()
        {

        }

        public void CloseFile()
        { 
        }

        //Función para escribir en un archivo
        public void WriteInFile(string message)
        {
            //Añadimos el mensaje para el archivo en la ruta específica
            StreamWriter stream = File.AppendText(path);
            //Escribimos en el archivo
            stream.WriteLine(message);
            //Cerramos la conexión del StreamWriter
            stream.Close();
        }

        //Método para obtener todo el contenido de un archivo línea por línea, retorna un array de las líneas obtenidas
        public string[] ReadFromFile()
        {
            //Leemos todas las lineas y las guardamos en un array
            string[] lines = File.ReadAllLines(path);
            //Retornamos el array
            return lines;
        }
    }
}
