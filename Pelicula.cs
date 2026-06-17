using System;
using System.Collections.Generic;

namespace contenido
{
    internal class Pelicula : Contenido
    {
        // Propiedades específicas que usa películas en TMDb (añadidas para que no tiren error)
        public string original_title { get; set; }
        public string release_date { get; set; }

        // Constructor vacío para el Serializer
        public Pelicula() : base() { }

        // Constructor completo que le pasa los datos generales al Padre (: base)
        public Pelicula(bool ad, int[] genID, int i, string origLang, string oview, float popul, float votAver, int votCount, string ttl)
            : base(ad, genID, i, origLang, oview, popul, votAver, votCount)
        {
            this.title = ttl;
        }

        public override void MostrarDatos()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=================== PELÍCULA ===================");
            Console.ResetColor();

            Console.Write($"Nombre de la película: {this.title} ({this.original_title})\nFecha de estreno: {this.release_date}\nClasificación: ");
            if (this.adult)
            {
                Console.WriteLine("Solo para adultos");
            }
            else
            {
                Console.WriteLine("Apta para todo público");
            }

            Console.Write($"Idioma original: {this.original_language}\nGéneros: ");
            if (this.genre_ids != null)
            {
                foreach (int idGenero in this.genre_ids)
                {
                    if (Program.ListaGeneros != null && Program.ListaGeneros.ContainsKey(idGenero))
                    {
                        Console.Write($"{Program.ListaGeneros[idGenero]} ");
                    }
                }
            }

            Console.WriteLine($"\nCalificación promedio: {this.vote_average}\nVotos totales: {this.vote_count}\nÍndice de popularidad: {this.popularity}\nSinopsis: {this.overview}");
            Console.WriteLine("------------------------------------------------------------------\n");
        }
    }
}
