using System;
using System.Collections.Generic;

namespace contenido
{
    internal class TotalContenidos
    {
        public int page { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }

        // CORREGIDO: Ahora la lista es directamente de tu clase base Contenido
        public List<Contenido> results { get; set; } = new List<Contenido>();

        public void MostrarDatos(int indicePagina, Usuario usuario_activo)
        {
            // Validamos que realmente hayan llegado películas o series
            if (results == null || results.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No se encontraron resultados para mostrar en esta página.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            // PASO CLAVE: Si 'title' viene vacío, es una serie, entonces usamos 'name'
            string tituloFinal = !string.IsNullOrEmpty(this.results[indicePagina].title) ? this.results[indicePagina].title : this.results[indicePagina].name;

            // Si no hay sinopsis, ponemos un texto por defecto para que no quede en blanco
            string sinopsisFinal = !string.IsNullOrEmpty(this.results[indicePagina].overview)
                ? this.results[indicePagina].overview
                : "Sin sinopsis disponible.";

            // Imprimimos el título destacado en amarillo
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"• Título: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(tituloFinal);

            // Imprimimos la sinopsis resumida
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"  Sinopsis: {sinopsisFinal}");
            Console.WriteLine("------------------------------------------------------------------");
            Console.ResetColor();
        }
    }
}
