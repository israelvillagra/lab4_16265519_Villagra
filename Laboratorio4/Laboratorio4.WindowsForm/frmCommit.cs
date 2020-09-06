using System;
using System.Windows.Forms;

namespace Laboratorio4.WindowsForm
{
    /// <summary>
    /// formulario que solamente se debe ingresar el comentario que se incluirá en el commit
    /// </summary>
    public partial class frmCommit : Form
    {
        /// <summary>
        /// constructor del fomulario
        /// </summary>
        public frmCommit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento que confirma el ingreso de información del usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            textBox1.Text = String.Empty;
        }

        /// <summary>
        /// Eventoq ue cierra el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
