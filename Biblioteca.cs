using System;
using System.Collections.Generic;

namespace contenido
{
    internal class Biblioteca
    {
        // Atributos privados encapsulados (Pauta N° 3)
        private List<Usuario> _usuarios;
        private List<Publicacion> _publicaciones;

        // Constructor: Inicializa las colecciones en memoria RAM (Pauta N° 4)
        public Biblioteca()
        {
            this._usuarios = new List<Usuario>();
            this._publicaciones = new List<Publicacion>();
        }

        // Propiedades públicas con métodos de acceso obligatorios para el Serializer
        public List<Usuario> usuarios
        {
            get { return this._usuarios; }
            set { this._usuarios = value; }
        }

        public List<Publicacion> publicaciones
        {
            get { return this._publicaciones; }
            set { this._publicaciones = value; }
        }

        // Método de instancia para añadir elementos de forma controlada
        public bool agregar_usuario(Usuario us)
        {
            if (us == null)
            {
                Console.WriteLine("El usuario es nulo.");
                return false;
            }

            // Verifica si ya existe un usuario con el mismo nombre
            foreach (Usuario u in this._usuarios)
            {
                if (u.nombre_usuario == us.nombre_usuario)
                {
                    Console.WriteLine($"El usuario '{us.nombre_usuario}' ya existe.");
                    return false;
                }
            }

            this._usuarios.Add(us);
            Console.WriteLine($"Usuario '{us.nombre_usuario}' agregado correctamente.");
            return true;
        }
        public bool eliminar_usuario(Usuario us)
        {
            if (us == null)
            {
                Console.WriteLine($"El usuario '{us.nombre_usuario}' es nulo.");
                return false;
            }

            if (this._usuarios.Remove(us))
            {
                Console.WriteLine($"Usuario '{us.nombre_usuario}' eliminado correctamente.");
                return true;
            }
            else
            {
                Console.WriteLine("El usuario no existe en la biblioteca.");
                return false;
            }

        }
        // Método de instancia para añadir elementos de forma controlada
        public void agregar_publicacion(Publicacion pu)
        {
            if (pu != null)
            {
                this.publicaciones.Add(pu);
            }
        }
    }
}
