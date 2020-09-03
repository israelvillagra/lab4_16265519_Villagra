using Laboratorio4.Controller;
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
    public partial class frmFileName : Form
    {
        public ArchivoDeTextoPlano archivo;
        private ZonaDeTrabajo _zonaDeTrabajo;
        private frmFile frmFile;
        public frmFileName(ZonaDeTrabajo p_ZonaDeTrabajo)
        {
            _zonaDeTrabajo = p_ZonaDeTrabajo;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_zonaDeTrabajo._ListaDeArchivos != null && _zonaDeTrabajo._ListaDeArchivos.Count > 0)
            {
                ArchivoDeTextoPlano archivoDeTextoPlano = _zonaDeTrabajo._ListaDeArchivos.Where(x => x._Nombre == txtNombreArchivo.Text).FirstOrDefault();
                if (archivoDeTextoPlano == null)
                {
                    this.Visible = false;
                    frmFile = new frmFile(txtNombreArchivo.Text);
                    frmFile.ShowDialog();
                }
                else
                {
                    MessageBox.Show("El nombre del archivo ya se encuentra en la Zona de Trabajo");
                }
            }
            else
            {
                this.Visible = false;
                frmFile = new frmFile(txtNombreArchivo.Text);
                frmFile.ShowDialog();
            }

            if (frmFile != null)
            {
                if (frmFile.ArchivoDeTextoPlano != null)
                {
                    RepositorioController.AgregarArchivoZonaDeTrabajo(_zonaDeTrabajo.NombreZonaDeTrabajo, frmFile.ArchivoDeTextoPlano);
                }

                this.Close();
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
