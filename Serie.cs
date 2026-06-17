using System;
using System.Collections.Generic;

namespace contenido
{
    internal class Serie : Contenido
    {
        // Propiedades específicas que usa series en TMDb
        public string original_name { get; set; }
        public string first_air_date { get; set; }

        // Constructor vacío para el Serializer
        public Serie() : base() { }

        // Constructor completo que le pasa los datos generales al Padre (: base)
        public Serie(bool ad, int[] genID, int i, string origLang, string oview, float popul, float votAver, int votCount, string nm)
            : base(ad, genID, i, origLang, oview, popul, votAver, votCount)
        {
            this.name = nm;
        }

        public override void MostrarDatos()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===================== SERIE =====================");
            Console.ResetColor();

            Console.Write($"Nombre de la serie: {this.name} ({this.original_name})\nFecha de estreno del primer episodio: {this.first_air_date}\nClasificación: ");
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
