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
            listView1.Items.Clear();
            zonaDeTrabajo = Form1.repositorioController.obtenerZonaDeTrabajo(ZonasDeTrabajoEnum.Workspace);
            if (zonaDeTrabajo._ListaDeArchivos != null && zonaDeTrabajo._ListaDeArchivos.Count > 0)
            {
               
                foreach (ArchivoDeTextoPlano item in zonaDeTrabajo._ListaDeArchivos)
                {
                    ListViewItem itemTXT = new ListViewItem(item._Nombre);
                    itemTXT.SubItems.Add(item._FechaModificacion.ToString());
                    itemTXT.SubItems.Add(item._Version.ToString());
                    listView1.Items.Add(itemTXT);
                }
                btnActualiza.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            frmFileName fileNew = new frmFileName(zonaDeTrabajo);
            fileNew.ShowDialog();
            this.Visible = true;
            CargaListaArchivos(ZonasDeTrabajoEnumerador);
        }


        private void btnActualiza_Click(object sender, EventArgs e)
        {
            if (listView1.CheckedItems.Count > 0)
            {
                List<ArchivoDeTextoPlano> listadoArchivo = new List<ArchivoDeTextoPlano>();

                foreach (ListViewItem itemList in listView1.CheckedItems)
                {
                   var archivo=  zonaDeTrabajo._ListaDeArchivos.FirstOrDefault(x => x._Nombre == itemList.Text);
                    if (archivo != null)
                        listadoArchivo.Add(archivo);
                }

                int resultado = RepositorioController.AddFileToIndex(listadoArchivo);
                if(resultado>0)
                    MessageBox.Show("Se han copiados los archivos de manera exitosa");
                else
                    MessageBox.Show("Ha ocurrido un problema al copiar el archivo");
            }
            else
            {
                MessageBox.Show("Debe seleccionar al menos un archivo");
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var itemselect = this.listView1.FocusedItem.Text;
            if (zonaDeTrabajo._ListaDeArchivos!=null && zonaDeTrabajo._ListaDeArchivos.Count>0)
            {
                ArchivoDeTextoPlano archivo = zonaDeTrabajo._ListaDeArchivos.FirstOrDefault(x => x._Nombre == itemselect);
                frmFile fileNew = new frmFile(archivo);
                this.Visible = false;
                fileNew.ShowDialog();
                this.Visible = true;
                CargaListaArchivos(ZonasDeTrabajoEnumerador);
            }
            
        }
    }
}
