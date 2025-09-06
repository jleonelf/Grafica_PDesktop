using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Grafica_PDesktop
{
    class Serializador
    {

        public static void SerializarObjeto<T>(T objeto, string rutaArchivo)
        {
            try
            {
                // Indentado para que sea legible
                string json = JsonConvert.SerializeObject(objeto, Formatting.Indented);
                File.WriteAllText(rutaArchivo, json);
                Console.WriteLine($"Objeto serializado en: {rutaArchivo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al serializar: {ex.Message}");
            }
        }

        public static T DeserializarObjeto<T>(string rutaArchivo)
        {
            try
            {
                string json = File.ReadAllText(rutaArchivo);
                T objeto = JsonConvert.DeserializeObject<T>(json);
                Console.WriteLine($"Objeto deserializado desde: {rutaArchivo}");
                return objeto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al deserializar: {ex.Message}");
                return default(T);
            }
        }



    }
}
