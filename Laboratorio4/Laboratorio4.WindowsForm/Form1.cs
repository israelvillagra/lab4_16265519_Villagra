using Laboratorio4.Controller;
using Laboratorio4.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Laboratorio4.WindowsForm
{
    public partial class Form1 : Form
    {
        public static RepositorioController repositorioController;
        public Form1()
        {
            repositorioController = new RepositorioController();
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

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                FileName = "*.xml",
                Filter = "XML Files (*.xml)|*.xml",
                Title = "ImportarRepositorio"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string mensaje = String.Empty;
                    var filePath = openFileDialog1.FileName;
                    bool respuesta = RepositorioController.ImportRepository(filePath, ref mensaje);
                    MessageBox.Show(mensaje);
                    if (respuesta)
                    {
                        this.Visible = false;
                        new frmPrincipal().ShowDialog();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }

        }
    }
}
