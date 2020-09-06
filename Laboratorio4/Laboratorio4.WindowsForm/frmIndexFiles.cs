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
    public partial class frmIndexFiles : Form
    {
        public frmIndexFiles()
        {
            InitializeComponent();
        }

        ZonaDeTrabajo zonaDeTrabajo;
        ZonasDeTrabajoEnum ZonasDeTrabajoEnumerador;
        public frmIndexFiles(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum)
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                if (listView1.CheckedItems.Count > 0)
                {
                    this.Visible = false;
                    frmCommit comit = new frmCommit();
                    comit.ShowDialog();
                    if (!String.IsNullOrWhiteSpace(comit.textBox1.Text))
                    {
                        List<ArchivoDeTextoPlano> listadoArchivo = new List<ArchivoDeTextoPlano>();

                        foreach (ListViewItem itemList in listView1.CheckedItems)
                        {
                            var archivo = zonaDeTrabajo._ListaDeArchivos.FirstOrDefault(x => x._Nombre == itemList.Text);
                            if (archivo != null)
                                listadoArchivo.Add(archivo);
                        }

                        int resultado = Form1.repositorioController.AddAllFilesToLocalRepository(comit.textBox1.Text, listadoArchivo);
                        if (resultado > 0)
                            MessageBox.Show("Se acaba de realizar el commit");
                        else
                            MessageBox.Show("Ha Ocurrido un problema al ejecutar la operación");

                        CargaListaArchivos(ZonasDeTrabajoEnumerador);
                    }
                }
                else
                {
                    MessageBox.Show("Debe tener al menos un archivo en Index");
                }
            }
            else
                MessageBox.Show("Debe tener al menos un archivo en Index");

            this.Visible = true;
        }
    }
}
