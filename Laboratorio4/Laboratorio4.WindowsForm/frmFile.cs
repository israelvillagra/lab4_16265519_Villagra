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
                    ArchivoDeTextoPlano._Version = ArchivoDeTextoPlano._Version + 1;
                    ArchivoDeTextoPlano._Contenido = rtbContenido.Text;
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
