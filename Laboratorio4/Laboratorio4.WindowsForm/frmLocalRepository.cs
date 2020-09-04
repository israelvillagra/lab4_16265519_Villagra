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
    public partial class frmLocalRepository : Form
    {
        ZonaDeTrabajo zonaDeTrabajo;
        ZonasDeTrabajoEnum ZonasDeTrabajoEnumerador;
        public frmLocalRepository(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum)
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
                    listView1.Items.Add(itemTXT);
                }
                btnPushFiles.Visible = true;
            }
        }
    }
}
