using Laboratorio4.Controller;
using System;
using System.Windows.Forms;

namespace Laboratorio4.WindowsForm
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Se crea el objeto estatico para ser manejdado desde los otros formularios
        /// </summary>
        public static RepositorioController repositorioController;

        /// <summary>
        /// Inicialización del Objeto
        /// </summary>
        public Form1()
        {
            //Inicializa el Repositorio controller, quien contiene la lógica del GitHub
            repositorioController = new RepositorioController();
            InitializeComponent();
        }

        /// <summary>
        /// Evento relacionado con el botón para la creación del repositorio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNombreAutor.Text.Trim()) && !String.IsNullOrEmpty(txtNombreRepositorio.Text.Trim()))
            {
                this.Visible = false;
                ///Al iniciar este formulario se entrega la información del nuevo repositorio
                new frmPrincipal(txtNombreRepositorio.Text.Trim(),txtNombreAutor.Text.Trim()).ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe ingresar el nombre del repositorio como el autor");
            }
        }

        /// <summary>
        /// Función que maneja el evento de presionar el botón que contiene l lógica del cierre del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Evento generado por el usuario que desea importar un repositorio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //Filta los elementos que puede seleccionar, que seben ser xml
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
                    ///Realiza el trabajo de enviar al controlador el nombre del archivo para ser deserializado
                    string mensaje = String.Empty;
                    var filePath = openFileDialog1.FileName;
                    bool respuesta = RepositorioController.ImportRepository(filePath, ref mensaje);
                    MessageBox.Show(mensaje);
                    if (respuesta)
                    {
                        this.Visible = false;
                        //Mantiene el formulario principal mientras se trabaje con el sistema
                        new frmPrincipal().ShowDialog();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error.\n: {ex.Message}\n\n" +
                    $"Detalle:\n\n{ex.StackTrace}");
                }
            }
        }
    }
}
