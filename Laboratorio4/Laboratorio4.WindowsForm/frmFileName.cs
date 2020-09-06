using Laboratorio4.Controller;
using Laboratorio4.Entities;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Laboratorio4.WindowsForm
{
    /// <summary>
    /// Formulario creado para pedir el nombre del archivo
    /// </summary>
    public partial class frmFileName : Form
    {
        /// Variables del formulario
        public ArchivoDeTextoPlano archivo;
        private ZonaDeTrabajo _zonaDeTrabajo;
        private frmFile frmFile;

        /// <summary>
        /// Constructor dle formulario, contiene la zona de trabajo donde será agregado el archivo de texto plano
        /// </summary>
        /// <param name="p_ZonaDeTrabajo"></param>
        public frmFileName(ZonaDeTrabajo p_ZonaDeTrabajo)
        {
            _zonaDeTrabajo = p_ZonaDeTrabajo;
            InitializeComponent();
        }

        /// <summary>
        /// Evento que inicializa el formulario donde se puede ingresar la información del objeto (Archivo de texto plano)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (_zonaDeTrabajo._ListaDeArchivos != null && _zonaDeTrabajo._ListaDeArchivos.Count > 0)
            {
                //comprueba que el archivo sea unico
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

            //Si el formulario no ha sido incializado, se obtiene el objeto que esta siendo modificado
            if (frmFile != null)
            {
                if (frmFile.ArchivoDeTextoPlano != null)
                {
                    RepositorioController.AgregarArchivoZonaDeTrabajo(_zonaDeTrabajo.NombreZonaDeTrabajo, frmFile.ArchivoDeTextoPlano);
                }

                this.Close();
            }
        }

        /// <summary>
        /// Evento del cierre del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
