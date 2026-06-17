using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace contenido
{
    enum estado_usuario
    {
        privado,
        publico,
        inexistente
    }

    internal class Usuario
    {
        private string _nombre_Usuario;
        private string _clave_usuario;
        private DateTime _fechaNacimiento;
        private estado_usuario _estado;
        private List<Publicacion> _publicaciones_usuario;

      
        public Usuario()
        {
            this.Clave_usuario = string.Empty;
            this.nombre_usuario = string.Empty;
            this.estado = estado_usuario.inexistente;
            this.FechaNacimiento = DateTime.Now;
            this.publicaciones_usuario = new List<Publicacion>();
        }

        public Usuario(string nom, string clave, DateTime fechaNac, estado_usuario est)
        {
            this.Clave_usuario = clave;
            this.nombre_usuario = nom;
            this.FechaNacimiento = fechaNac;
            this.estado = est;
            this.publicaciones_usuario = new List<Publicacion>();
        }

        public string nombre_usuario
        {
            get { return this._nombre_Usuario; }
            set { this._nombre_Usuario = value; }
        }

        public string Clave_usuario
        {
            get { return this._clave_usuario; }
            set { this._clave_usuario = value; }
        }

        public DateTime FechaNacimiento
        {
            get { return this._fechaNacimiento; }
            set { this._fechaNacimiento = value; }
        }

        // Propiedad de solo lectura
        public int Edad
        {
            get
            { return DateTime.Now.Year - FechaNacimiento.Year; }
        }

        public estado_usuario estado
        {
            get { return this._estado; }
            set { this._estado = value; }
        }

        public List<Publicacion> publicaciones_usuario
        {
            get { return this._publicaciones_usuario; }
            set { this._publicaciones_usuario = value; }
        }


        public void MostrarDatosUsuario()
        {
            Console.WriteLine($"Nombre de usuario: {this.nombre_usuario}\nClave: {this.Clave_usuario}\nFecha de nacimiento: {this.FechaNacimiento}\nEdad: {this.Edad}\nEstado: {this.estado}\nPublicaciones: ");
            foreach (Publicacion pub in this.publicaciones_usuario)
            {
                pub.MostrarDatosPublicacion();
                Console.WriteLine();
            }
        }

        public void CambiarDatos()
        {
            //se cambiara proximamente
            Program.cadena = Utilidades.menu( new String[] { "Buscar pelicula","Buscar serie","Buscar usuario","Ver datos de usuario","Ver publicaciones","Adivinar pelicula","Cerrar secion" });
            switch (Program.cadena)
            {
                case "Buscar pelicula":
                {
                    //se espera codigo
                    break;
                }
                case "Buscar serie":
                {
                    //se espera codigo
                    break;
                }
                case "Buscar usuario":
                {   

                    //se espera codigo
                    break;
                }
                case "Ver publicaciones":
                {
                    //se espera codigo
                    break;
                }
                case "Adivinar pelicula":
                {
                        //se espera codigo
                        break;
                }
                case "Cerrar secion":
                {
                    //se espera codigo
                    break;
                }
            }
        }
    }
}
