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
    public partial class frmFilesInZonaDeTrabajo : Form
    {
        ZonaDeTrabajo zonaDeTrabajo;
        ZonasDeTrabajoEnum ZonasDeTrabajoEnumerador;
        public frmFilesInZonaDeTrabajo(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum)
        {
            InitializeComponent();
            CargaListaArchivos(p_ZonasDeTrabajoEnum);
            ZonasDeTrabajoEnumerador = p_ZonasDeTrabajoEnum;
        }

        private void CargaListaArchivos(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum)
        {
            zonaDeTrabajo = frmPrincipal.repositorioController.obtenerZonaDeTrabajo(ZonasDeTrabajoEnum.Workspace);
            if (zonaDeTrabajo._ListaDeArchivos != null && zonaDeTrabajo._ListaDeArchivos.Count > 0)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            new frmFileName(zonaDeTrabajo).ShowDialog();
            this.Visible = true;
            CargaListaArchivos(ZonasDeTrabajoEnumerador);
        }
    }
}
