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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNombreAutor.Text.Trim()) && !String.IsNullOrEmpty(txtNombreRepositorio.Text.Trim()))
            {
                this.Visible = false;
                new frmPrincipal(txtNombreRepositorio.Text.Trim(),txtNombreAutor.Text.Trim()).ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe ingresar el nombre del repositorio como el autor");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
