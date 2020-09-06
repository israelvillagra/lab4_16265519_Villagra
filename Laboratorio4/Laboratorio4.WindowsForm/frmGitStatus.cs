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
    public partial class frmGitStatus : Form
    {
        public frmGitStatus(Repositorio p_Repositorio)
        {
            InitializeComponent();
            CompletarFormulario(p_Repositorio);
        }

        private void CompletarFormulario(Repositorio p_Repositorio)
        {
            this.lblAutor.Text = p_Repositorio._Autor;
            this.lblNombre.Text = p_Repositorio._Nombre;
            CargaListaArchivosWorkSpace(ZonasDeTrabajoEnum.Workspace, p_Repositorio);
            CargaListaArchivosIndex(ZonasDeTrabajoEnum.Index, p_Repositorio);
            CargaListaCommits(ZonasDeTrabajoEnum.LocalRepository, p_Repositorio);
            CargaListaRemote(ZonasDeTrabajoEnum.Workspace, ZonasDeTrabajoEnum.RemoteRepository, p_Repositorio);
        }

        private void CargaListaArchivosWorkSpace(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum, Repositorio p_Repositorio)
        {
            listView1.Items.Clear();
            ZonaDeTrabajo zonaDeTrabajo = p_Repositorio._ListaZonasDeTrabajo.FirstOrDefault(x=>x.NombreZonaDeTrabajo == p_ZonasDeTrabajoEnum);

            if (zonaDeTrabajo._ListaDeArchivos != null && zonaDeTrabajo._ListaDeArchivos.Count > 0)
            {
                foreach (ArchivoDeTextoPlano item in zonaDeTrabajo._ListaDeArchivos)
                {
                    ListViewItem itemTXT = new ListViewItem(item._Nombre);
                    itemTXT.SubItems.Add(item._Version.ToString());
                    listView1.Items.Add(itemTXT);
                }
                lblCantidadWorkspace.Text = zonaDeTrabajo._ListaDeArchivos.Count.ToString();
            }
        }

        private void CargaListaArchivosIndex(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum, Repositorio p_Repositorio)
        {
            listView2.Items.Clear();
            ZonaDeTrabajo zonaDeTrabajo = p_Repositorio._ListaZonasDeTrabajo.FirstOrDefault(x => x.NombreZonaDeTrabajo == p_ZonasDeTrabajoEnum);

            if (zonaDeTrabajo._ListaDeArchivos != null && zonaDeTrabajo._ListaDeArchivos.Count > 0)
            {
                foreach (ArchivoDeTextoPlano item in zonaDeTrabajo._ListaDeArchivos)
                {
                    ListViewItem itemTXT = new ListViewItem(item._Nombre);
                    itemTXT.SubItems.Add(item._Version.ToString());
                    listView2.Items.Add(itemTXT);
                }
                lblCantidadIndex.Text = zonaDeTrabajo._ListaDeArchivos.Count.ToString();
            }
        }

        private void CargaListaCommits(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum, Repositorio p_Repositorio)
        {
            listView3.Items.Clear();
            ZonaDeTrabajo zonaDeTrabajo = p_Repositorio._ListaZonasDeTrabajo.FirstOrDefault(x => x.NombreZonaDeTrabajo == p_ZonasDeTrabajoEnum);

            if (zonaDeTrabajo._ListaCommit != null && zonaDeTrabajo._ListaCommit.Count > 0)
            {
                foreach (Commit item in zonaDeTrabajo._ListaCommit)
                {
                    ListViewItem itemTXT = new ListViewItem(item._Identificador.ToString());
                    itemTXT.SubItems.Add(item._Fecha.ToString());
                    itemTXT.SubItems.Add(item._ListaDeArchivos.Count.ToString());
                    listView3.Items.Add(itemTXT);
                }
                lblCantidadCommits.Text = zonaDeTrabajo._ListaCommit.Count.ToString();
            }
        }

        private void CargaListaRemote(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnumDesde, ZonasDeTrabajoEnum p_ZonasDeTrabajoEnumHasta, Repositorio p_Repositorio)
        {
            ZonaDeTrabajo zonaDeTrabajo = p_Repositorio._ListaZonasDeTrabajo.FirstOrDefault(x => x.NombreZonaDeTrabajo == p_ZonasDeTrabajoEnumDesde);
            ZonaDeTrabajo zonaDeTrabajoHasta = p_Repositorio._ListaZonasDeTrabajo.FirstOrDefault(x => x.NombreZonaDeTrabajo == p_ZonasDeTrabajoEnumHasta);
            List<ArchivoDeTextoPlano> listaArchivos = new List<ArchivoDeTextoPlano>();
            if (zonaDeTrabajoHasta._ListaDeArchivos != null
                && zonaDeTrabajo._ListaDeArchivos != null
                && zonaDeTrabajo._ListaDeArchivos.Count > 0
                && zonaDeTrabajoHasta._ListaDeArchivos.Count > 0)
            {
                bool Iguales = false;
                ListViewItem itemTXT = new ListViewItem();
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
                        itemTXT.SubItems.Add("Archivo No Existe");
                        Iguales = false;
                    }

                    listView4.Items.Add(itemTXT);
                }

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
                        itemTXT.SubItems.Add("Archivo No Existe");
                        listView4.Items.Add(itemTXT);
                        Iguales = false;
                    }

                    
                }
            }
        }
    }
}
