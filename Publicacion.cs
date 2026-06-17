using System;
using System.Collections.Generic;
using System.Text;

namespace contenido
{

    internal class Publicacion
    {
        private Usuario _usuario_publicacion;
        private string _contenido_publicacion;
        private DateTime _fecha_publicacion;
        private string _texto_publicacion;

        public Publicacion()
        {
            this.usuario_publicacion = new Usuario();
            this.contenido_publicacion = string.Empty;
            this.fecha_publicacion = DateTime.Now;
            this.texto_publicacion = string.Empty;
        }

        public void Usuario(Usuario us, string con, string text)
        {
            this.usuario_publicacion = us;
            this.contenido_publicacion = con;
            this.fecha_publicacion = DateTime.Now;
            this.texto_publicacion = text;
        }



        public Usuario usuario_publicacion
        {
            get { return this._usuario_publicacion; }
            set { this._usuario_publicacion = value; }
        }

        public string contenido_publicacion
        {
            get { return this._contenido_publicacion; }
            set { this._contenido_publicacion = value; }
        }

        public DateTime fecha_publicacion
        {
            get { return this._fecha_publicacion; }
            set { this._fecha_publicacion = value; }
        }

        public string texto_publicacion
        {
            get { return this._contenido_publicacion; }
            set { this._contenido_publicacion = value; }
        }

        public void MostrarDatosPublicacion()
        {
            Console.WriteLine($"Nombre de usuario de la publicacion: {this.usuario_publicacion}\nContenido al que referencia: {this.contenido_publicacion}\nFecha de publicacion: {this.fecha_publicacion}\n{this.texto_publicacion}");

        }
    }
}
