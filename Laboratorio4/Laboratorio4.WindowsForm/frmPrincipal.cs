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
using static Laboratorio4.Entities.Utiles.EnumeradoresUtiles;

namespace Laboratorio4.WindowsForm
{
    public partial class frmPrincipal : Form
    {

        public frmPrincipal(string p_Nombre, String p_Autor)
        {
            InitializeComponent();
            Form1.repositorioController = new RepositorioController(p_Nombre, p_Autor);
            this.Text = "Formulario Principal Repositorio : " + p_Nombre;
        }

        public frmPrincipal()
        {
            InitializeComponent();
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

        private void btnIndex_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            new frmIndexFiles(ZonasDeTrabajoEnum.Index).ShowDialog();
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                bool respuesta =false;
                string resultado = Form1.repositorioController.ExportRepository(fbd.SelectedPath, ref respuesta);
                if (respuesta)
                    MessageBox.Show("Se ha creado el archivo : "+ resultado);
                else
                    MessageBox.Show("Ha ocurrido un problema al crear el archivo");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            new frmLocalRepository(ZonasDeTrabajoEnum.LocalRepository).ShowDialog();
            this.Visible = true;
            //CargaListaArchivos
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            new frmRemoteRepository(ZonasDeTrabajoEnum.RemoteRepository).ShowDialog();
            this.Visible = true;
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            new frmGitStatus(Form1.repositorioController.GetRepository()).ShowDialog();
            this.Visible = true;
        }
    }
}
