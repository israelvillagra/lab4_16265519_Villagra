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
                //List<ArchivoDeTextoPlanoModel> listaArchivos = new List<ArchivoDeTextoPlanoModel>();
                //var listadoOrdenado = zonaDeTrabajo._ListaCommit.OrderBy(x => x._Fecha);

                //foreach (Commit temp in listadoOrdenado)
                //{
                //    foreach (ArchivoDeTextoPlano item in temp._ListaDeArchivos)
                //    {
                //        var archivo = listaArchivos.FirstOrDefault(x => x._Nombre.Equals(item._Nombre));
                //        if (archivo != null)
                //        {
                //            listaArchivos.Remove(archivo);
                //            archivo = new ArchivoDeTextoPlanoModel(item, temp);
                //            listaArchivos.Add(archivo);
                //        }
                //        else
                //            listaArchivos.Add(new ArchivoDeTextoPlanoModel(item, temp));
                //    }
                //}

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

            if (zonaDeTrabajo._ListaCommit != null && zonaDeTrabajo._ListaCommit.Count > 0)
            {
                List<Commit> listaCommits = zonaDeTrabajo._ListaCommit.Where(x => !x._Copiado).ToList();
                if (listaCommits != null && listaCommits.Count > 0)
                {
                    int resulado = Form1.repositorioController.AddAllFilesToRemoteRepository();
                    if (resulado > 0)
                        MessageBox.Show("Se han copiado los archivos en Remote Repository");
                    else
                        MessageBox.Show("Ha ocurido un problema al copiar los archivos");
                }
            }
            CargaListaArchivos(ZonasDeTrabajoEnumerador);
        }
    }
}
