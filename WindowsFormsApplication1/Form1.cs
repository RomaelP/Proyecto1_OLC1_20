using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Collections;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        LinkedList<token> listaToken = new LinkedList<token>();
        LinkedList<error> listaErrores = new LinkedList<error>();

        /*              BOTON ABRIR                 */
        private void aBRIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "rich text box(*.er) | *.er";
            openFileDialog1.FileName = "";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.InitialDirectory = "Escritorio";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtEntrada.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                //MessageBox.Show("ARCHIVO ABIERTO", "OLC1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void gUARDARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtEntrada.Clear();
        }

        private void sALIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void gUARDARToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "rich text box(*.er) | *.er";
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.Title = "Save File";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtEntrada.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                MessageBox.Show("Archivo Guardado", "OLC1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(txtEntrada.Text.Equals("")))
            {
                string textoAnalizar = txtEntrada.Text;
                metodoAnalizar(textoAnalizar + " ");
                MessageBox.Show("ANALISIS REALIZADO", "OLC1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else
            {
                MessageBox.Show("INGRESE TEXTO EN EL CUADRO DE TEXTO", "OLC1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void generarArchivoToken() {            

            using (StreamWriter outputFile = new StreamWriter("C:\\Users\\romael\\Desktop\\token.xml")) {
                outputFile.WriteLine("<ListaTokens>");
                foreach (token tk in listaToken) {
                    outputFile.WriteLine(   "\t<Token>\n"
                                                + "\t\t<Nombre> " + tk.getLexema() + " </Nombre>\n"
                                                + "\t\t<Valor> " + tk.getDato() + " </Valor>\n"
                                                + "\t\t<Fila> " + tk.getFila() + " </Fila>\n"
                                                + "\t\t<Columna> " + tk.getColumna() + " </Columna>\n"
                                            + "\t</Token>");
                }            
                outputFile.WriteLine("</ListaTokens>");
            }
            MessageBox.Show("ARCHIVO DE TOKENS ESCRITO", "OLC1", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void generarArchivoErrores()
        {

            using (StreamWriter outputFile = new StreamWriter("C:\\Users\\romael\\Desktop\\errores.xml")) {
                outputFile.WriteLine("<ListaErrores>");
                foreach (error er in listaErrores) {
                    outputFile.WriteLine("\t<Error>\n"
                                                + "\t\t<Valor> " + er.getCaracter() + " </Valor>\n"
                                                + "\t\t<Descripcion> " + er.getDescripcion() + " </Descripcion>\n"
                                                + "\t\t<Fila> "+er.getFila()+" </Fila>\n"
                                                + "\t\t<Columna> "+er.getColumna()+" </Columna>"
                                          +"\t</Error>");
                }
                outputFile.WriteLine("</ListaErrores>");
            }
            
        }

        private void metodoAnalizar(string textoAnalizar)
        {
            int fila = 0;
            int columna = 0;
            int eInicial = 0;
            int eActual = 0;
            char actual;
            string token = "";
            string cadenaAux = "";

            for (eInicial = 0; eInicial < textoAnalizar.Length; eInicial++) {
                actual = textoAnalizar[eInicial];

                switch (eActual) {
                    case 0://                                           ESTADO 0
                        
                        switch (validarCaracter(actual)) {
                            case 3://EB
                                columna++;
                                eActual = 0;
                                break;

                            case 4://SL                                
                                fila++;
                                eActual = 0;
                                break;

                            case 5://{
                                columna++;
                                token tk = new token("llave abre", "" + actual, fila, columna);
                                listaToken.AddLast(tk);
                                eActual = 1;
                                break;

                            default:
                                rtbConsola.AppendText("Error caracter no reconocido "+actual+" se esperaba { \n");
                                error er = new error(actual+" ","se esperaba {",fila,columna);
                                listaErrores.AddLast(er);
                                eActual = -1;
                                break;
                        }
                        break;

                    case 1://                                           ESTADO 1
                        columna = 0;
                        switch (validarCaracter(actual)) {
                            case 3://EB
                                columna++;
                                eActual = 1;
                                break;

                            case 4://SL                                
                                fila++;
                                eActual = 1;
                                break;

                            case 6:// /
                                columna++;
                                eActual = 2;
                                break;

                            case 7:// <
                                columna++;
                                eActual = 4;
                                break;

                            case 2:// L
                                columna++;
                                eActual = 8;
                                token += actual;
                                break;

                            case 15://}                                
                                token tk = new token("llave cierra", "" + actual, fila, columna);
                                listaToken.AddLast(tk);
                                break;

                            default:
                                rtbConsola.AppendText("Error caracter no reconocido " + actual + " se esperaba L|<|/ \n");
                                error er = new error(actual + " ", "se esperaba L|<|/ ", fila, columna);
                                listaErrores.AddLast(er);
                                eActual = -1;
                                break;
                        }
                        break;

                    case 2://                                            ESTADO 2
                        switch (validarCaracter(actual)) {
                            case 6:// /
                                columna++;
                                eActual = 3;
                                break;

                            default:
                                rtbConsola.AppendText("Error caracter no reconocido " + actual + " se esperaba / \n");
                                error er = new error(actual + " ", "se esperaba /", fila, columna);
                                listaErrores.AddLast(er);
                                eActual = -1;
                                break;
                        }
                        break;

                    case 3://                                           ESTADO 3                                             
                        switch (validarCaracter(actual)) {
                            case 4://SL
                                fila++;
                                eActual = 1;
                                rtbConsola.AppendText("Comentario de una linea aceptado\n");
                                break;

                            default://CUALQUIER COSA
                                eActual = 3;
                                break;
                        }
                        break;

                    case 4://                                           ESTADO 4
                        switch (validarCaracter(actual)) {
                            case 9://!
                                columna++;
                                eActual = 5;
                                break;

                            default:
                                rtbConsola.AppendText("Error caracter no reconocido " + actual + " se esperaba ! \n");
                                error er = new error(actual + " ", "se esperaba !", fila, columna);
                                listaErrores.AddLast(er);
                                eActual = -1;
                                break;
                        }
                        break;

                    case 5://                                           ESTADO 5
                        switch(validarCaracter(actual)){
                            case 9://!
                                columna++;
                                eActual = 6;
                                break;

                            case 4://SL
                                fila++;
                                eActual = 5;
                                break;

                            default:
                                eActual = 5;
                                break;
                        }
                        break;

                    case 6://                                           ESTADO 6
                        switch (validarCaracter(actual)) {
                            case 8://>
                                columna++;
                                eActual = 1;
                                rtbConsola.AppendText("Comentario multilinea aceptado\n");
                                break;
                        }
                        break;

                    case 7://                                           ESTADO 7
                        break;

                    case 8://                                           ESTADO 8
                        switch (validarCaracter(actual)) {
                            case 1://N
                                token += actual;
                                columna++;
                                eActual = 8;
                                break;

                            case 2://L
                                token += actual;
                                columna++;
                                eActual = 8;
                                break;

                            case 18://_
                                token += actual;
                                columna++;
                                eActual = 8;
                                break;

                            case 3://EB
                                cadenaAux = token;
                                //Console.WriteLine("Guardar token ID: "+cadenaAux);
                                token += actual;
                                eActual = 8;                                
                                break;

                            case 11://:
                                token += actual;
                                cadenaAux = token;                                
                                token tk = new token("ID", cadenaAux, fila, columna);
                                listaToken.AddLast(tk);
                                                                
                                token tk1 = new token("dos puntos", "" + actual, fila, columna);
                                listaToken.AddLast(tk1);

                                columna++;
                                eActual = 9;
                                break;

                            case 10://-
                                token += actual;
                                columna++;
                                token tk2 = new token("ID", "" + cadenaAux, fila, columna);
                                listaToken.AddLast(tk2);
                                
                                token tk3 = new token("guion", "" + actual, fila, columna);
                                listaToken.AddLast(tk3);                                
                                cadenaAux = "";
                                eActual = 20;
                                break;

                            default:
                                rtbConsola.AppendText("Error caracter no reconocido " + actual + " se esperaba L|N|_|: \n");
                                error er = new error(actual + " ", "se esperaba L|N|_|:", fila, columna);
                                listaErrores.AddLast(er);
                                eActual = -1;
                                break;
                        }                       
                        break;

                    case 9://                                           ESTADO 9
                        switch (validarCaracter(actual)) {
                            case 2://L
                                cadenaAux = "";
                                token += actual;
                                cadenaAux += actual;
                                columna++;
                                eActual = 10;
                                break;

                            case 16://"
                                cadenaAux = "";
                                token += actual;                                
                                columna++;                                
                                token tk = new token("comillas", "" + actual, fila, columna);
                                listaToken.AddLast(tk);
                                eActual = 18;
                                break;

                            case 3://EB                                                                
                                token += actual;
                                columna++;
                                eActual = 9;
                                break;

                            default:
                                rtbConsola.AppendText("Error caracter no reconocido " + actual + " se esperaba L|\" \n");
                                error er = new error(actual + " ", "se esperaba L|\"", fila, columna);
                                listaErrores.AddLast(er);
                                eActual = -1;
                                break;
                        }
                        break;

                    case 10://                                              ESTADO 10
                        switch (validarCaracter(actual)) {
                            case 1://N
                                token += actual;
                                cadenaAux += actual;
                                columna++;
                                eActual = 10;
                                break;

                            case 2://L
                                token += actual;
                                cadenaAux += actual;
                                columna++;
                                eActual = 10;
                                break;

                            case 18://_
                                token += actual;
                                cadenaAux += actual;
                                columna++;
                                eActual = 10;
                                break;

                            case 3://EB               
                                columna++;                                                                                 
                                token += actual;
                                eActual = 10;
                                break;

                            case 10://-
                                token += actual;
                                columna++;
                                token tk0 = new token("ID", cadenaAux, fila, columna);
                                listaToken.AddLast(tk0);

                                token tk1 = new token("guion", "" + actual, fila, columna);
                                listaToken.AddLast(tk1);
                                eActual = 11;
                                break;

                            default:                                
                                rtbConsola.AppendText("Error caracter no reconocido " + actual + " se esperaba L|N|_|- \n");
                                error er = new error(actual + " ", "se esperaba L|N|_|-", fila, columna);
                                listaErrores.AddLast(er);
                                eActual = -1;
                                break;
                        }
                        break;

                    case 11://                                              ESTADO 11
                        switch (validarCaracter(actual)) {
                            case 8://>
                                token += actual;
                                columna++;                                
                                token tk = new token("mayor que", "" + actual, fila, columna);
                                listaToken.AddLast(tk);                               
                                eActual = 12;
                                break;

                            default:                                
                                rtbConsola.AppendText("Error caracter no reconocido " + actual + " se esperaba > \n");
                                error er = new error(actual + " ", "se esperaba >", fila, columna);
                                listaErrores.AddLast(er);
                                break;
                        }
                        break;

                    case 12://                                              ESTADO 12
                        switch (validarCaracter(actual)) {
                            case 1://N
                                token += actual;
                                columna++;                                
                                token tk = new token("NUMERO", "" + actual, fila, columna);
                                listaToken.AddLast(tk);
                                columna++;
                                eActual = 13;
                                break;

                            case 2://L
                                token += actual;
                                columna++;
                                
                                token tk1 = new token("ID", "" + actual, fila, columna);
                                listaToken.AddLast(tk1);                                
                                eActual = 13;                                
                                break;

                                //aqui idear como hacer lo del ascii de caracteres
                        }
                        break;

                    case 13://                                              ESTADO 13
                        switch (validarCaracter(actual)) {
                            case 13://~
                                token += actual;                                
                                token tk = new token("guion especial", "" + actual, fila, columna);
                                listaToken.AddLast(tk);
                                columna++;
                                eActual = 14;
                                break;

                            case 14://,
                                token += actual;                                
                                token tk1 = new token("coma", "" + actual, fila, columna);
                                listaToken.AddLast(tk1);
                                columna++;
                                eActual = 16;
                                break;

                            default:
                                rtbConsola.AppendText("Error caracter no reconocido " + actual + " se esperaba ~|, \n");
                                error er = new error(actual + " ", "se esperaba ~|,", fila, columna);
                                listaErrores.AddLast(er);
                                eActual = -1;
                                break;
                        }
                        break;

                    case 14://                                              ESTADO 14
                        switch (validarCaracter(actual)) {
                            case 1://N
                                token += actual;
                                columna++;                                
                                token tk = new token("ID", "" + actual, fila, columna);
                                listaToken.AddLast(tk);                                
                                eActual = 15;
                                break;

                            case 2://L
                                token += actual;                                
                                token tk1 = new token("LETRA", "" + actual, fila, columna);
                                listaToken.AddLast(tk1);
                                columna++;
                                eActual = 15;
                                break;

                                //aqui idear como hacer lo del ascii de caracteres
                        }
                        break;

                    case 15://                                              ESTADO 15
                        switch (validarCaracter(actual)) {
                            case 12://;
                                token += actual;
                                columna++;                               
                                token tk = new token("punto y coma", "" + actual, fila, columna);
                                listaToken.AddLast(tk);
                                Console.WriteLine("\tConjunto: "+token);
                                token = "";
                                cadenaAux = "";
                                eActual = 1;
                                break;

                            default:
                                rtbConsola.AppendText("Error caracter no reconocido " + actual + " se esperaba ; \n");
                                error er = new error(actual + " ", "se esperaba ;", fila, columna);
                                listaErrores.AddLast(er);
                                eActual = -1;
                                break;
                        }
                        break;

                    case 16://                                                      ESTADO 16
                        switch (validarCaracter(actual)) {
                            case 1://N
                                token += actual;
                                columna++;                                
                                token tk = new token("NUMERO", "" + actual, fila, columna);
                                listaToken.AddLast(tk);                                
                                eActual = 17;
                                break;

                            case 2:
                                token += actual;
                                columna++;
                                
                                token tk1 = new token("LETRA", "" + actual, fila, columna);
                                listaToken.AddLast(tk1);                                
                                eActual = 17;
                                break;

                            default:                               
                                rtbConsola.AppendText("Error caracter no reconocido " + actual + " se esperaba L|N \n");
                                error er = new error(actual + " ", "se esperaba L|N", fila, columna);
                                listaErrores.AddLast(er);
                                eActual = -1;
                                break;
                        }
                        break;

                    case 17://                                                      ESTADO 17
                        switch (validarCaracter(actual)) {
                            case 14://,
                                token += actual;
                                columna++;
                                
                                token tk = new token("coma", "" + actual, fila, columna);
                                listaToken.AddLast(tk);
                                columna++;
                                eActual = 16;
                                break;

                            case 12://;
                                token += actual;
                                columna++;
                                
                                token tk1 = new token("punto y coma", "" + actual, fila, columna);
                                listaToken.AddLast(tk1);
                                eActual = 1;
                                Console.WriteLine("\tConjunto: " + token);
                                token = "";
                                cadenaAux = "";
                                break;

                            default:
                                rtbConsola.AppendText("Error caracter no reconocido " + actual + " se esperaba ,|; \n");
                                error er = new error(actual + " ", "se esperaba ,|;", fila, columna);
                                listaErrores.AddLast(er);
                                eActual = -1;
                                break;
                        }
                        break;

                    case 18://                                  ESTADO 18
                        //cadenaAux = "";
                        switch (validarCaracter(actual)) {
                            case 16://"
                                token += actual;
                                columna++;

                                token tk = new token("CADENA", "" + cadenaAux, fila, columna);
                                listaToken.AddLast(tk);

                                token tk1 = new token("comillas", "" + actual, fila, columna);
                                listaToken.AddLast(tk1);
                                eActual = 19;
                                break;

                            default:
                                token += actual;
                                cadenaAux += actual;
                                columna++;
                                eActual = 18;
                                break;
                        }
                        break;

                    case 19://                                  ESTADO 19
                        switch (validarCaracter(actual)) {
                            case 3://EB
                                token += actual;
                                columna++;
                                eActual = 19;
                                break;

                            case 12://;
                                token += actual;
                                columna++;

                                token tk = new token("punto y coma", "" + actual, fila, columna);
                                listaToken.AddLast(tk);
                                Console.WriteLine("\tExpresion: " + token );
                                eActual = 1;
                                cadenaAux = "";
                                token = "";
                                break;
                        }
                        break;

                    case 20:
                        switch (validarCaracter(actual)) {
                            case 8://>
                                token += actual;
                                columna++;
                                token tk = new token("mayor que", "" + actual, fila, columna);
                                listaToken.AddLast(tk);
                                
                                eActual = 21;
                                break;

                            default:
                                rtbConsola.AppendText("Error caracter no reconocido " + actual + " se esperaba > \n");
                                error er = new error(actual + " ", "se esperaba >", fila, columna);
                                listaErrores.AddLast(er);
                                eActual = -1;
                                break;
                        }
                        break;

                    case 21://                                      ESTADO 21
                        switch (validarCaracter(actual)) {
                            case 3://EB
                                token += actual;
                                columna++;
                                eActual = 21;
                                break;

                            case 17://OP
                                token += actual;
                                columna++;                                
                                token tk = new token("operador", "" + actual, fila, columna);
                                listaToken.AddLast(tk);                                
                                eActual = 21;
                                break;

                            case 5://{
                                token += actual;
                                
                                token tk1 = new token("llave abre", "" + actual, fila, columna);
                                listaToken.AddLast(tk1);
                                columna++;
                                eActual = 22;
                                break;

                            case 16://"
                                token += actual;
                                columna++;
                                token tk2 = new token("comillas", "" + actual, fila, columna);
                                listaToken.AddLast(tk2);
                                
                                eActual = 23;
                                break;

                            case 12://;
                                token += actual;
                                columna++;
                                
                                token tk3 = new token("punto y coma", "" + actual, fila, columna);
                                listaToken.AddLast(tk3);
                                Console.WriteLine("\tER: " + token);                            
                                eActual = 1;
                                token = "";
                                break;

                            default:
                                rtbConsola.AppendText("Error caracter no reconocido " + actual + " se esperaba OP|{|\"|; \n");
                                error er = new error(actual + " ", "se esperaba OP|{|\"|;", fila, columna);
                                listaErrores.AddLast(er);
                                eActual = -1;
                                break;
                        }
                        break;

                    case 22:        //                              ESTADO 22
                        switch (validarCaracter(actual)) {
                            case 1://N
                                token += actual;
                                cadenaAux += actual;
                                columna++;
                                eActual = 22;
                                break;

                            case 2://L
                                //cadenaAux = "";
                                token += actual;
                                cadenaAux += actual;
                                columna++;
                                eActual = 22;
                                break;

                            case 18://_
                                token += actual;
                                cadenaAux += actual;
                                columna++;
                                eActual = 22;
                                break;

                            case 3://EB                                
                                //Console.WriteLine("Guardar token ID: " + cadenaAux);
                                token += actual;
                                eActual = 22;
                                break;

                            case 15://}
                                token += actual;
                                //cadenaAux = token;
                                columna++;
                                
                                token tk = new token("ID", "" + cadenaAux, fila, columna);
                                listaToken.AddLast(tk);
                                
                                token tk1 = new token("llaves cierra", "" + actual, fila, columna);
                                listaToken.AddLast(tk1);
                                
                                eActual = 21;
                                cadenaAux = "";
                                break;
                        }
                        break;

                    case 23:
                        switch (validarCaracter(actual)) {
                            case 16://"
                                token += actual;
                                columna++;
                                
                                token tk = new token("comillas", "" + actual, fila, columna);
                                listaToken.AddLast(tk);
                                
                                token tk1 = new token("CADENA", "" + cadenaAux, fila, columna);
                                listaToken.AddLast(tk1);
                                eActual = 21;
                                cadenaAux = "";
                                break;

                            default:
                                token += actual;
                                cadenaAux += actual;
                                columna++;
                                eActual = 23;
                                break;
                        }
                        break;

                    case -1://Estado de error
                        switch (validarCaracter(actual)) {
                            case 4://SL
                                fila++;
                                eActual = 1;
                                break;

                            case 12://;
                                eActual = 1;
                                break;

                            default:
                                eActual = -1;
                                break;
                        }
                        break;
                    default:
                        rtbConsola.AppendText("Error caracter no reconocido \n");
                        error er1 = new error(actual + " ","caracter no reconocido", fila, columna);
                        listaErrores.AddLast(er1);
                        eActual = -1;
                        break;

                }
            }
        }

        private int validarCaracter(char caracter) {
            if (Char.IsNumber(caracter))
                return 1;
            else if (Char.IsLetter(caracter))
                return 2;
            else if (caracter.Equals(' ') || caracter.Equals('\t'))
                return 3;
            else if (caracter.Equals('\n') || caracter.Equals('\r'))
                return 4;
            else if (caracter.Equals('{'))
                return 5;
            else if (caracter.Equals('/'))
                return 6;
            else if (caracter.Equals('<'))
                return 7;
            else if (caracter.Equals('>'))
                return 8;
            else if (caracter.Equals('!'))
                return 9;
            else if (caracter.Equals('-'))
                return 10;
            else if (caracter.Equals(':'))
                return 11;
            else if (caracter.Equals(';'))
                return 12;
            else if (caracter.Equals('~'))
                return 13;
            else if (caracter.Equals(','))
                return 14;
            else if (caracter.Equals('}'))
                return 15;
            else if (caracter.Equals('\"'))
                return 16;
            else if (caracter.Equals('+') || caracter.Equals('?') || caracter.Equals('*') || caracter.Equals('.') || caracter.Equals('|'))
                return 17;
            else if (caracter.Equals('_')) 
                return 18;
            else
            return -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void aRCHIVODETOKENSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listaToken != null)
            {
                generarArchivoToken();
            }
        }

        private void aRCHIVODEERRORESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listaErrores != null)
            {
                generarArchivoErrores();
            }
        }

        
    }
}

/*

 */