using Laboratorio4.Entities;
using System;
using System.Windows.Forms;

namespace Laboratorio4.WindowsForm
{
    /// <summary>
    /// Formulario que permite crear o modificar un objeto archivo de texto plano
    /// </summary>
    public partial class frmFile : Form
    {
        /// Objetos que serán utilizados
        public ArchivoDeTextoPlano ArchivoDeTextoPlano;
        string _Nombre;

        /// <summary>
        /// Contructor, contiene el nombre del archivo
        /// </summary>
        /// <param name="p_NombreArchivo"></param>
        public frmFile(string p_NombreArchivo)
        {
            InitializeComponent();
            this.Text = "Archivo : " + p_NombreArchivo;
            _Nombre = p_NombreArchivo;
        }

        /// <summary>
        /// Contructor del formulario, se diferencia que recibe ya un archivo, se utilizará para actualizar el archivo
        /// </summary>
        /// <param name="p_Archivo"></param>
        public frmFile(ArchivoDeTextoPlano p_Archivo)
        {
            InitializeComponent();
            this.Text = "Archivo : " + p_Archivo._Nombre;
            _Nombre = p_Archivo._Nombre;
            rtbContenido.Text = p_Archivo._Contenido;
            //copia el archivo de texto plano hacia la variable global
            ArchivoDeTextoPlano = p_Archivo;
        }

        /// <summary>
        /// Evento que confirma el cierre del formulario, cuando ya ingreso el contenido del archivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(rtbContenido.Text))
            {
                //Si el Objeto es nulo, es un nuevo objeto
                if (ArchivoDeTextoPlano == null)
                {
                    ArchivoDeTextoPlano = new ArchivoDeTextoPlano(_Nombre, rtbContenido.Text);
                    this.Close();
                }
                else
                {
                    //En el caso que que el objeto no sea nulo, se esta actualizando el objeto
                    if (!rtbContenido.Text.Equals(ArchivoDeTextoPlano._Contenido))
                    {
                        ArchivoDeTextoPlano._Contenido = rtbContenido.Text;
                        ArchivoDeTextoPlano._Version += 1;
                    }
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("El archivo debe tener un contenido");
            }
        }

        /// <summary>
        /// Botón que informa que no se desean realizar cambios en el objeto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ArchivoDeTextoPlano = null;
            this.Close();
        }
    }
}
