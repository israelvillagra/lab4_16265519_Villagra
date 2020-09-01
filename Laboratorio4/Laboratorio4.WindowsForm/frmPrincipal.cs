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
using static Laboratorio4.Entities.Utiles.EnumeradoresUtiles;

namespace Laboratorio4.WindowsForm
{
    public partial class frmPrincipal : Form
    {
        public static RepositorioController repositorioController;
        public frmPrincipal(string p_Nombre, String p_Autor)
        {
            InitializeComponent();
            repositorioController = new RepositorioController(p_Nombre, p_Autor);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            new frmFilesInZonaDeTrabajo(ZonasDeTrabajoEnum.Workspace).ShowDialog();
            this.Visible = true;
        }
    }
}
