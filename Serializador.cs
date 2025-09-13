using System;
using System.IO;
using Newtonsoft.Json;

namespace Grafica_PDesktop
{
    class Serializador
    {
        static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Include,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public static void SerializarObjeto<T>(T objeto, string rutaArchivo)
        {
            string json = JsonConvert.SerializeObject(objeto, Settings);
            File.WriteAllText(rutaArchivo, json);
            Console.WriteLine($"Objeto serializado en: {rutaArchivo}");
        }

        public static T DeserializarObjeto<T>(string rutaArchivo)
        {
            string json = File.ReadAllText(rutaArchivo);
            if (string.IsNullOrWhiteSpace(json))
            {
                Console.WriteLine("El JSON está vacío: " + rutaArchivo);
                return default(T);
            }
            var obj = JsonConvert.DeserializeObject<T>(json, Settings);
            Console.WriteLine($"Objeto deserializado desde: {rutaArchivo}");
            return obj;
        }
    }
}
