using System;
using System.Collections.Generic;
using System.Text;

namespace contenido
{
    public class Genero
    {
        private int _id; 
        private string _name; 

        public Genero()
        {
            this.id = 0;
            this.name = string.Empty;  
        }

        public int id 
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string name 
        {
            get { return this._name; }
            set { this._name = value; }
        }
    }
}
