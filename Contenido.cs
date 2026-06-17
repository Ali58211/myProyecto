using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contenido
{
    internal class Contenido
    {
        private bool _adult; //indica si la serie o pelicula es para adultos
        private int[] _genre_ids; //lista de IDs de géneros
        private int _id; //ID único de la serie o pelicula en TMDb
        private string _original_language; //idioma original
        private string _overview; //sinopsis
        private float _popularity; //popularidad calculada por TMDb
        private float _vote_average; //promedio de puntuación
        private int _vote_count; //cantidad de votos recibidos

        // Nuevos atributos privados para respaldar las propiedades de la API
        private string _title;
        private string _original_title;
        private string _release_date;
        private string _name;
        private string _original_name;
        private string _first_air_date;


        public Contenido()
        {
            // Inicializamos los atributos privados directamente
            this._adult = false;
            this._genre_ids = new int[] { };
            this._id = 0;
            this._original_language = string.Empty;
            this._overview = string.Empty;
            this._popularity = 0;
            this._vote_average = 0;
            this._vote_count = 0;

            // Inicializamos los campos nuevos de la API
            this._title = string.Empty;
            this._original_title = string.Empty;
            this._release_date = string.Empty;
            this._name = string.Empty;
            this._original_name = string.Empty;
            this._first_air_date = string.Empty;
        }

        public Contenido(bool ad, int[] genID, int i, string origLang, string oview, float popul, float votAver, int votCount)
        {
            // Asignamos directamente a las variables privadas de respaldo (_nombre)
            // Esto evita cualquier conflicto de nombres con las propiedades o parámetros
            this._adult = ad;
            this._genre_ids = genID;
            this._id = i;
            this._original_language = origLang;
            this._overview = oview;
            this._popularity = popul;
            this._vote_average = votAver;
            this._vote_count = votCount;
        }

        public bool adult
        {
            get { return this._adult; }
            set { this._adult = value; }
        }

        public int[] genre_ids
        {
            get { return this._genre_ids; }
            set { this._genre_ids = value; }
        }

        public int id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string original_language
        {
            get { return this._original_language; }
            set { this._original_language = value; }
        }

        public string overview
        {
            get { return this._overview; }
            set { this._overview = value; }
        }

        public float popularity
        {
            get { return this._popularity; }
            set { this._popularity = value; }
        }

        public float vote_average
        {
            get { return this._vote_average; }
            set { this._vote_average = value; }
        }

        public int vote_count
        {
            get { return this._vote_count; }
            set { this._vote_count = value; }
        }

        // --- Propiedades expandidas específicas para Películas ---
        public string title
        {
            get { return this._title; }
            set { this._title = value; }
        }

        public string original_title
        {
            get { return this._original_title; }
            set { this._original_title = value; }
        }

        public string release_date
        {
            get { return this._release_date; }
            set { this._release_date = value; }
        }

        // --- Propiedades expandidas específicas para Series ---
        public string name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public string original_name
        {
            get { return this._original_name; }
            set { this._original_name = value; }
        }

        public string first_air_date
        {
            get { return this._first_air_date; }
            set { this._first_air_date = value; }
        }


        public virtual void MostrarDatos()
        {
            Console.WriteLine("Datos generales del contenido.");
        }
    }
}
