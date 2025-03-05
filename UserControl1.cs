using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Remoting.Messaging;

namespace ValidateTextBox
{

    //Declaración del enumerado 
    public enum eTipo
    {
        NUMERICO, TEXTUAL
    }



    public partial class UserControl1 : UserControl
    {
        public int index = 0;
        public Color color = Color.Red;
        public bool auxTextual = false;

        char[] letras = new char[53];
        public UserControl1()
        {
            InitializeComponent();
            //  this.Size = new Size(txtInterno.Height + 20, );
            this.Height = txtInterno.Height + 20;
            txtInterno.Width = this.Width - 20;


            //Bucle de iniciación de las letras (mayusculas, minusculas y un espacio)--53 caracteres
            for (int i = 0; i < 26; i++)// 26 porque hago una vuelta del abecedario
            {

                letras[i] = (char)('A' + i);
                //Darle a depurar para visualizar las letars 
                Debug.Write(letras[i]);
                letras[i + 26] = (char)('a' + i);
            }
            letras[52] = ' ';


        }

        //Definicion de nuevas propiedades de mi nuevo componente

        private String texto;
        [Category("Appearence")]
        [Description("Accede al campo text del TextBox interno del componente")]

        public String Texto
        {
            get
            {
                return txtInterno.Text;

            }
            set
            {

                if (Tipo == eTipo.NUMERICO)
                {
                    String aux = value;


                    if (aux != "")
                    {



                        bool flag = comprobarNumIndex(aux, 0);



                        Debug.WriteLine(flag + " bandera");
                        if (flag) { color = Color.Green; }
                        if (!flag) { color = Color.Red; /*isRecursivo = true;*/ }
                    }



                }
                else if (Tipo == eTipo.TEXTUAL)
                {
                    String auxtext = value;

                    auxTextual = comprobarTextual(auxtext, 0);

                    if (auxTextual) { color = Color.Green; }
                    if (!auxTextual) { color = Color.Red; }


                }
                /* txtInterno.Text = value; //se *//*refiere a esto? */
                txtInterno.Text = value;
                Invalidate();
                //Refresh();


            }
        }


        //Propiedad Multiline
        private bool multilinea;

        [Category("Appearence")]
        [Description("Establece la propiedad multiline del text box interno del componente")]
        public bool Multilinea
        {
            get { return txtInterno.Multiline; }
            set
            {
                txtInterno.Multiline = value;


            }
        }

        //Propiedad de tipo eTipo
        private eTipo tipo;
        [Category("Appearence")]
        [Description("Determina si es de tipo NUMÉRICO o TEXTUAL")]

        public eTipo Tipo
        {
            get
            {
                return tipo;
            }
            set
            {

                tipo = value;
            }


        }


        //Definición del evento y forma de agregarlo 
        [Category("Mis eventos")]
        [Description("Permite el acceso TextChange del textBox interno del componente.")]
        public event EventHandler CambiaTexto;

        //Función asociada a mi evento

        public void OnCambiaTexto()
        {
            //Si mi evento no es nulo se invoca
            CambiaTexto?.Invoke(this, EventArgs.Empty);
        }




        // HERENCIA Y SOBRECARGA de UserControl que a suvez hereda de Control que tiene el método
        // SizeChange 

        // Al sobreescribir el método se usa OnXX ,SIENDO XX el evento a sobreescribir de la clase padre

        // Método sobreescrito de la clase UserControl>Control por lo que no necesita enlazarlo desde las propiedades
        // de mi componente

        //Si existe el método se recomienda sobreescribirlo
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Height = txtInterno.Height + 20;
            txtInterno.Width = this.Width - 20;
            Debug.WriteLine("aaaaaaaaaaaaaaaaaaa");
        }

        private void txtInterno_TextChanged(object sender, EventArgs e)
        {
            Texto = txtInterno.Text;
            Debug.WriteLine("Hago el set del texto a mi componente");
        }





        public bool comprobarNumIndex(String valor, int index)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };




            if (valor.Length - 1 >= index)
            {

                for (int i = 0; i < nums.Length; i++)
                {
                    try
                    {
                        int valoraux = Int32.Parse(valor[index].ToString());
                        if (valoraux == (nums[i]))
                        {
                            Debug.WriteLine(valor[index] + " valor index");


                            if (valor.Length - 1 == index)
                            {
                                //color = Color.Green;
                                return true;
                            }
                            else
                            {
                                //Devuelvo el resultado de la funcion que es true o false
                                return comprobarNumIndex(valor, index + 1);//compruebo el siguiente numero

                            }
                        }
                    }
                    catch (FormatException fw)
                    {

                        return false;
                    }
                    catch (ArgumentException arg)
                    {

                        return false;
                    }
                    catch (OverflowException overflow)
                    {
                        return false;
                    }



                }

            }



            return false;

        }

        public bool comprobarTextual(String cad, int index)//index 0
        {
            if (cad != "")
            {

                int longitudCad = cad.Length;

                foreach (var item in cad)
                {
                    auxTextual = letras.Contains(item);//si no lo contiene

                    if (!auxTextual)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Graphics g = e.Graphics;
            // Color color = Tipo == eTipo.NUMERICO ? Tipo == eTipo.TEXTUAL ? Color.Green : Color.Red: Color.Red;
            e.Graphics.DrawRectangle(new Pen(color, 2), new Rectangle(5, 5, this.Width - 5, this.Height - 5));
            // Refresh();
        }

    }
}
