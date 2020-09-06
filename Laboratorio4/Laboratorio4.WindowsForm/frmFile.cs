using Laboratorio4.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratorio4.WindowsForm
{
    public partial class frmFile : Form
    {
        public ArchivoDeTextoPlano ArchivoDeTextoPlano;
        string _Nombre;
        public frmFile(string p_NombreArchivo)
        {
            InitializeComponent();
            this.Text = "Archivo : " + p_NombreArchivo;
            _Nombre = p_NombreArchivo;
        }

        public frmFile(ArchivoDeTextoPlano p_Archivo)
        {
            InitializeComponent();
            this.Text = "Archivo : " + p_Archivo._Nombre;
            _Nombre = p_Archivo._Nombre;
            rtbContenido.Text = p_Archivo._Contenido;
            ArchivoDeTextoPlano = p_Archivo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(rtbContenido.Text))
            {
                if (ArchivoDeTextoPlano == null)
                {
                    ArchivoDeTextoPlano = new ArchivoDeTextoPlano(_Nombre, rtbContenido.Text);
                    this.Close();
                }
                else
                {
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

        private void button1_Click(object sender, EventArgs e)
        {
            ArchivoDeTextoPlano = null;
            this.Close();
        }
    }
}
