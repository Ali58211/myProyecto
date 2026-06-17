using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using contenido.Utilidades;
namespace contenido
{
    internal class Program
    {
        public static string cadena = "";
        public static bool continuar = true;
        public static string[] info;
        public static HttpClient client = new HttpClient();
        public static string apiKey = "f6ea4d5e46440ed50e6316844f6b6f6d";
        public static Dictionary<int, string> ListaGeneros = new Dictionary<int, string>();
        static async Task Main(string[] args)
        {
            try
            {
                Utilidades util = new Utilidades();
                await util.DescargarGeneros();
                util.menu_principal();
                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
