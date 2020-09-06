using Laboratorio4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static Laboratorio4.Entities.Utiles.EnumeradoresUtiles;

namespace Laboratorio4.WindowsForm
{
    /// <summary>
    /// Formulario que muestra del repositorio
    /// </summary>
    public partial class frmGitStatus : Form
    {
        /// <summary>
        /// Contructor que recibe el objeto Repositorio
        /// </summary>
        /// <param name="p_Repositorio"></param>
        public frmGitStatus(Repositorio p_Repositorio)
        {
            InitializeComponent();
            CompletarFormulario(p_Repositorio);
        }

        /// <summary>
        /// Función que se encarga de llamar a las otras funciones especificas para cada dato a desplegar
        /// </summary>
        /// <param name="p_Repositorio"></param>
        private void CompletarFormulario(Repositorio p_Repositorio)
        {
            this.lblAutor.Text = p_Repositorio._Autor;
            this.lblNombre.Text = p_Repositorio._Nombre;
            CargaListaArchivosWorkSpace(ZonasDeTrabajoEnum.Workspace, p_Repositorio);
            CargaListaArchivosIndex(ZonasDeTrabajoEnum.Index, p_Repositorio);
            CargaListaCommits(ZonasDeTrabajoEnum.LocalRepository, p_Repositorio);
            CargaListaRemote(ZonasDeTrabajoEnum.Workspace, ZonasDeTrabajoEnum.RemoteRepository, p_Repositorio);
        }

        /// <summary>
        /// Función que se encarga de listar la lista de archivos de texto plano para ser vializadas por el usuario
        /// </summary>
        /// <param name="p_ZonasDeTrabajoEnum"></param>
        /// <param name="p_Repositorio"></param>
        private void CargaListaArchivosWorkSpace(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum, Repositorio p_Repositorio)
        {
            listView1.Items.Clear();
            ///Obtiene la Zona de Trabajo para cargar los archivos
            ZonaDeTrabajo zonaDeTrabajo = p_Repositorio._ListaZonasDeTrabajo.FirstOrDefault(x=>x.NombreZonaDeTrabajo == p_ZonasDeTrabajoEnum);

            if (zonaDeTrabajo._ListaDeArchivos != null && zonaDeTrabajo._ListaDeArchivos.Count > 0)
            {
                ///Recorre la lista de archivos y llena el user control
                foreach (ArchivoDeTextoPlano item in zonaDeTrabajo._ListaDeArchivos)
                {
                    ListViewItem itemTXT = new ListViewItem(item._Nombre);
                    itemTXT.SubItems.Add(item._Version.ToString());
                    listView1.Items.Add(itemTXT);
                }
                //Informa la cantidad de archivos
                lblCantidadWorkspace.Text = zonaDeTrabajo._ListaDeArchivos.Count.ToString();
            }
        }

        /// <summary>
        /// Función que se encarga de listar la lista de archivos de texto plano para ser vializadas por el usuario
        /// </summary>
        /// <param name="p_ZonasDeTrabajoEnum"></param>
        /// <param name="p_Repositorio"></param>
        private void CargaListaArchivosIndex(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum, Repositorio p_Repositorio)
        {
            listView2.Items.Clear();
            ///Obtiene la Zona de Trabajo para cargar los archivos
            ZonaDeTrabajo zonaDeTrabajo = p_Repositorio._ListaZonasDeTrabajo.FirstOrDefault(x => x.NombreZonaDeTrabajo == p_ZonasDeTrabajoEnum);

            if (zonaDeTrabajo._ListaDeArchivos != null && zonaDeTrabajo._ListaDeArchivos.Count > 0)
            {
                ///Recorre la lista de archivos y llena el user control
                foreach (ArchivoDeTextoPlano item in zonaDeTrabajo._ListaDeArchivos)
                {
                    ListViewItem itemTXT = new ListViewItem(item._Nombre);
                    itemTXT.SubItems.Add(item._Version.ToString());
                    listView2.Items.Add(itemTXT);
                }
                //Informa la cantidad de archivos
                lblCantidadIndex.Text = zonaDeTrabajo._ListaDeArchivos.Count.ToString();
            }
        }

        /// <summary>
        /// Función que se encarga de listar la lista de archivos de texto plano para ser vializadas por el usuario, especifica para listar los Commits
        /// </summary>
        /// <param name="p_ZonasDeTrabajoEnum"></param>
        /// <param name="p_Repositorio"></param>
        private void CargaListaCommits(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum, Repositorio p_Repositorio)
        {
            listView3.Items.Clear();
            ///Obtiene la Zona de Trabajo para cargar los archivos
            ZonaDeTrabajo zonaDeTrabajo = p_Repositorio._ListaZonasDeTrabajo.FirstOrDefault(x => x.NombreZonaDeTrabajo == p_ZonasDeTrabajoEnum);

            if (zonaDeTrabajo._ListaCommit != null && zonaDeTrabajo._ListaCommit.Count > 0)
            {
                ///Recorre la lista de Commits y llena el user control
                foreach (Commit item in zonaDeTrabajo._ListaCommit)
                {
                    ListViewItem itemTXT = new ListViewItem(item._Identificador.ToString());
                    itemTXT.SubItems.Add(item._Fecha.ToString());
                    itemTXT.SubItems.Add(item._ListaDeArchivos.Count.ToString());
                    listView3.Items.Add(itemTXT);
                }
                //Informa la cantidad de Commits
                lblCantidadCommits.Text = zonaDeTrabajo._ListaCommit.Count.ToString();
            }
        }

        /// <summary>
        /// Función que demuestra los archivos que se encuentran en el Remote Repository y en la Zona de Trabajo Workspace
        /// </summary>
        /// <param name="p_ZonasDeTrabajoEnumDesde"></param>
        /// <param name="p_ZonasDeTrabajoEnumHasta"></param>
        /// <param name="p_Repositorio"></param>
        private void CargaListaRemote(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnumDesde, ZonasDeTrabajoEnum p_ZonasDeTrabajoEnumHasta, Repositorio p_Repositorio)
        {
            ///Obtiene la lista de archivos en las zonas de trabajo correcpondientes
            ZonaDeTrabajo zonaDeTrabajo = p_Repositorio._ListaZonasDeTrabajo.FirstOrDefault(x => x.NombreZonaDeTrabajo == p_ZonasDeTrabajoEnumDesde);
            ZonaDeTrabajo zonaDeTrabajoHasta = p_Repositorio._ListaZonasDeTrabajo.FirstOrDefault(x => x.NombreZonaDeTrabajo == p_ZonasDeTrabajoEnumHasta);
            bool Iguales = false;
            List<ArchivoDeTextoPlano> listaArchivos = new List<ArchivoDeTextoPlano>();
            if (zonaDeTrabajoHasta._ListaDeArchivos != null
                && zonaDeTrabajo._ListaDeArchivos != null)
            {
                
                ListViewItem itemTXT = new ListViewItem();
                ///Comprueba que el ARchivo de Remote repository exista en la Zona de Trabajo workSpace
                foreach (ArchivoDeTextoPlano archivo in zonaDeTrabajoHasta._ListaDeArchivos)
                {
                    itemTXT = new ListViewItem(archivo._Nombre.ToString());
                    itemTXT.SubItems.Add(archivo._Version.ToString());
                    itemTXT.SubItems.Add(archivo._FechaModificacion.ToString());

                    var archivoWorkSpace = zonaDeTrabajo._ListaDeArchivos.FirstOrDefault(x => x._Nombre == archivo._Nombre);
                    if (archivoWorkSpace != null)
                    {
                        if (archivo._Version == archivoWorkSpace._Version)
                        {
                            itemTXT.SubItems.Add("Archivos Iguales");
                            if (!Iguales)
                                Iguales = true;
                        }
                        else
                        {
                            itemTXT.SubItems.Add("Diferentes Versiones");
                            Iguales = false;
                        }
                    }
                    else
                    {
                        itemTXT.SubItems.Add("Archivo No Existe en WorkSpace");
                        Iguales = false;
                    }

                    listView4.Items.Add(itemTXT);
                }

                ///Comprueba que el Archivo de WorkSpae exista en la Zona de Remote Repository
                foreach (ArchivoDeTextoPlano archivo in zonaDeTrabajo._ListaDeArchivos)
                {
                    itemTXT = new ListViewItem(archivo._Nombre.ToString());
                    itemTXT.SubItems.Add(archivo._Version.ToString());
                    itemTXT.SubItems.Add(archivo._FechaModificacion.ToString());

                    var archivoWorkSpace = zonaDeTrabajoHasta._ListaDeArchivos.FirstOrDefault(x => x._Nombre == archivo._Nombre);
                    if (archivoWorkSpace != null)
                    {
                        if (archivo._Version != archivoWorkSpace._Version)
                        {
                            itemTXT.SubItems.Add("Diferentes Versiones");
                        }
                    }
                    else
                    {
                        itemTXT.SubItems.Add("Archivo No Existe en Remote Repository");
                        listView4.Items.Add(itemTXT);
                        Iguales = false;
                    }
                }
            }
            else
            {
                Iguales = true;
            }

            lblIguales.Text = Iguales ? "Si" : "No";
        }
    }
}
