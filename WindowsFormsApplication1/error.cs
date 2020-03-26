using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class error
    {
        public string caracter;
        public string descripcion;
        public int fila;
        public int columna;

        public error(string caracter, string descripcion, int fila, int columna) {
            this.caracter = caracter;
            this.descripcion = descripcion;
            this.fila = fila;
            this.columna = columna; 
        }

        public string getCaracter()
        {
            return caracter;
        }

        public String getDescripcion()
        {
            return descripcion;
        }

        public int getFila()
        {
            return fila;
        }

        public int getColumna()
        {
            return columna;
        }
    }
}
