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
    public partial class frmRemoteRepository : Form
    {
        ZonaDeTrabajo zonaDeTrabajo;
        ZonasDeTrabajoEnum ZonasDeTrabajoEnumerador;
        public frmRemoteRepository(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum)
        {
            InitializeComponent();
            CargaListaArchivos(p_ZonasDeTrabajoEnum);
            ZonasDeTrabajoEnumerador = p_ZonasDeTrabajoEnum;
        }

        private void CargaListaArchivos(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum)
        {
            listView1.Items.Clear();
            zonaDeTrabajo = Form1.repositorioController.obtenerZonaDeTrabajo(p_ZonasDeTrabajoEnum);
            if (zonaDeTrabajo._ListaDeArchivos != null && zonaDeTrabajo._ListaDeArchivos.Count > 0)
            {
                foreach (ArchivoDeTextoPlano item in zonaDeTrabajo._ListaDeArchivos)
                {
                    ListViewItem itemTXT = new ListViewItem(item._Nombre);
                    itemTXT.SubItems.Add(item._FechaModificacion.ToString());
                    itemTXT.SubItems.Add(item._Version.ToString());
                    listView1.Items.Add(itemTXT);
                }
                btnPushFiles.Visible = true;
            }
        }

        private void btnPushFiles_Click(object sender, EventArgs e)
        {
            zonaDeTrabajo = Form1.repositorioController.obtenerZonaDeTrabajo(ZonasDeTrabajoEnumerador);

            if (zonaDeTrabajo._ListaDeArchivos != null && zonaDeTrabajo._ListaDeArchivos.Count > 0)
            {

                int resulado = Form1.repositorioController.AddAllFilesToWorkSpave();
                if (resulado > 0)
                    MessageBox.Show("Se han copiado los archivos en Workspace");
                else
                    MessageBox.Show("Ha ocurido un problema al copiar los archivos");
            }
            else
            {
                MessageBox.Show("No contiene archivos");
            }
            CargaListaArchivos(ZonasDeTrabajoEnumerador);
        }
    }
}