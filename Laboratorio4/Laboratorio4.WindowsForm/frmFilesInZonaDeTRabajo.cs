using Laboratorio4.Controller;
using Laboratorio4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static Laboratorio4.Entities.Utiles.EnumeradoresUtiles;

namespace Laboratorio4.WindowsForm
{
    /// <summary>
    /// Formulario que contiene la lógica de la vista de la Zona de Trabajo WorkSpace
    /// </summary>
    public partial class frmFilesInZonaDeTrabajo : Form
    {
        //Variables globales del formulario
        ZonaDeTrabajo zonaDeTrabajo;
        ZonasDeTrabajoEnum ZonasDeTrabajoEnumerador;

        /// <summary>
        /// Contructor del formulario, donde se debe ingresar el tipo de zona de trabajo
        /// </summary>
        /// <param name="p_ZonasDeTrabajoEnum">Zona de trabajo con la cual realizá los cambios</param>
        public frmFilesInZonaDeTrabajo(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum)
        {
            InitializeComponent();
            CargaListaArchivos(p_ZonasDeTrabajoEnum);
            ZonasDeTrabajoEnumerador = p_ZonasDeTrabajoEnum;
        }

        /// <summary>
        /// Función que se encarga de listar de objetos (Archivos de texto plano) para ser listados
        /// </summary>
        /// <param name="p_ZonasDeTrabajoEnum">zona de trabajo de la cual obtnendrá los objetos</param>
        private void CargaListaArchivos(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum)
        {
            listView1.Items.Clear();
            //Obtiene los objetos de la zona de trabajo que es enviada como parametro
            zonaDeTrabajo = Form1.repositorioController.obtenerZonaDeTrabajo(ZonasDeTrabajoEnum.Workspace);
            if (zonaDeTrabajo._ListaDeArchivos != null && zonaDeTrabajo._ListaDeArchivos.Count > 0)
            {
               ///Llena la lista para ser desplegados por el usuario en con User Control
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

        /// <summary>
        /// Evento que contiene la lógica para crear un nuevo archivo en la Zona de Trabajo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            frmFileName fileNew = new frmFileName(zonaDeTrabajo);
            fileNew.ShowDialog();
            this.Visible = true;
            CargaListaArchivos(ZonasDeTrabajoEnumerador);
        }

        /// <summary>
        /// EVento que genera la copia de los archivos selecionados el Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActualiza_Click(object sender, EventArgs e)
        {
            if (listView1.CheckedItems.Count > 0)
            {
                List<ArchivoDeTextoPlano> listadoArchivo = new List<ArchivoDeTextoPlano>();

                foreach (ListViewItem itemList in listView1.CheckedItems)
                {
                    var archivo = zonaDeTrabajo._ListaDeArchivos.FirstOrDefault(x => x._Nombre == itemList.Text);
                    if (archivo != null)
                        listadoArchivo.Add(archivo);
                }

                int resultado = RepositorioController.AddFileToIndex(listadoArchivo);
                if (resultado > 0)
                    MessageBox.Show("Se han copiados los archivos de manera exitosa");
                else
                    MessageBox.Show("Ha ocurrido un problema al copiar el archivo");
            }
            else
            {
                MessageBox.Show("Debe seleccionar al menos un archivo");
            }
        }

        /// <summary>
        /// Evento que provoca la actualización del archvo seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var itemselect = this.listView1.FocusedItem.Text;
            if (zonaDeTrabajo._ListaDeArchivos!=null && zonaDeTrabajo._ListaDeArchivos.Count>0)
            {
                //Se obtiene el archivo seleccionado para ser actualizado.
                ArchivoDeTextoPlano archivo = zonaDeTrabajo._ListaDeArchivos.FirstOrDefault(x => x._Nombre == itemselect);
                //Se incializa el formulario con el archivo a actualizar
                frmFile fileNew = new frmFile(archivo);
                this.Visible = false;
                fileNew.ShowDialog();
                this.Visible = true;
                CargaListaArchivos(ZonasDeTrabajoEnumerador);
            }
        }
    }
}
