using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class token
    {
        public string lexema;
        public string dato;
        public int fila;
        public int columna;

        public token(string lexema, string dato, int fila, int columna) {
            this.lexema = lexema;
            this.dato = dato;
            this.fila = fila;
            this.columna = columna;
        }

        public string getLexema() {
            return lexema;
        }

        public String getDato() {
            return dato;
        }

        public int getFila() {
            return fila;
        }

        public int getColumna() {
            return columna;
        }

    }
}
