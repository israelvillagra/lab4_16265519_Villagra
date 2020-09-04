﻿using Laboratorio4.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static Laboratorio4.Entities.Utiles.EnumeradoresUtiles;

namespace Laboratorio4.Controller
{
    

    public class RepositorioController
    {
        private static Repositorio RespitorioLaboratorio;

        public String ExportRepository(string p_PathFile, ref bool resultado)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(Repositorio));
                string fileName = p_PathFile + "\\"+ RespitorioLaboratorio._Nombre + ".xml";
                TextWriter txtWriter = new StreamWriter(fileName);
                xs.Serialize(txtWriter, RespitorioLaboratorio);
                txtWriter.Close();
                resultado = true;
                return fileName;
            }
            catch (Exception ex)
            {
                resultado = false;
                return String.Empty;
            }
        }

        public static bool ImportRepository(string p_PathFile, ref String p_Mensaje)
        {
            try
            {
                using (var stream = new FileStream(p_PathFile, FileMode.Open, FileAccess.Read))
                {
                    stream.Position = 0;
                    var serializer = new XmlSerializer(typeof(Repositorio));
                    RespitorioLaboratorio = serializer.Deserialize(stream) as Repositorio;
                }
                p_Mensaje = "Se ha restaurado el repositorio "+ RespitorioLaboratorio._Nombre;
                return true;
            }
            catch (Exception ex)
            {
                p_Mensaje = ex.Message;
                return false;
            }
        }


        public RepositorioController(String p_Nombre, String p_Autor)
        {
            gitInit(p_Nombre, p_Autor);
        }

        public RepositorioController() { }

        public int gitInit(String p_Nombre, String p_Autor)
        {
            RespitorioLaboratorio = new Repositorio();

            if (!p_Nombre.Equals("") && !p_Autor.Equals(""))
            {
                RespitorioLaboratorio._Nombre = p_Nombre;
                RespitorioLaboratorio._Autor = p_Autor;

                RespitorioLaboratorio._ListaZonasDeTrabajo = new List<ZonaDeTrabajo>();

                ZonaDeTrabajo zonaDeTrabajo = new ZonaDeTrabajo();
                zonaDeTrabajo.NombreZonaDeTrabajo = ZonasDeTrabajoEnum.Workspace;

                RespitorioLaboratorio._ListaZonasDeTrabajo.Add(zonaDeTrabajo);

                zonaDeTrabajo = new ZonaDeTrabajo();
                zonaDeTrabajo.NombreZonaDeTrabajo = ZonasDeTrabajoEnum.Index;
                RespitorioLaboratorio._ListaZonasDeTrabajo.Add(zonaDeTrabajo);

                zonaDeTrabajo = new ZonaDeTrabajo();
                zonaDeTrabajo.NombreZonaDeTrabajo = ZonasDeTrabajoEnum.LocalRepository;
                RespitorioLaboratorio._ListaZonasDeTrabajo.Add(zonaDeTrabajo);

                zonaDeTrabajo = new ZonaDeTrabajo();
                zonaDeTrabajo.NombreZonaDeTrabajo = ZonasDeTrabajoEnum.RemoteRepository;
                RespitorioLaboratorio._ListaZonasDeTrabajo.Add(zonaDeTrabajo);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public ZonaDeTrabajo obtenerZonaDeTrabajo(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum)
        {
            return RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == p_ZonasDeTrabajoEnum).FirstOrDefault();
        }

        public static bool AgregarArchivoZonaDeTrabajo(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum, ArchivoDeTextoPlano p_Archivo)
        {
            ZonaDeTrabajo zona =  RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == p_ZonasDeTrabajoEnum).FirstOrDefault();
            if (zona._ListaDeArchivos == null)
                zona._ListaDeArchivos = new List<ArchivoDeTextoPlano>();

            zona._ListaDeArchivos.Add(p_Archivo);
            return true;
        }

        public static int AddFileToIndex(List<ArchivoDeTextoPlano> p_ListaArchivos)
        {
            int resultado = 0;
            try
            {
                ZonaDeTrabajo zona = RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == ZonasDeTrabajoEnum.Index).FirstOrDefault();

                if (zona._ListaDeArchivos == null)
                    zona._ListaDeArchivos = new List<ArchivoDeTextoPlano>();

                foreach (ArchivoDeTextoPlano archivo in p_ListaArchivos)
                {
                    var archivoTemp = zona._ListaDeArchivos.FirstOrDefault(x => x._Nombre == archivo._Nombre);

                    if (archivoTemp != null)
                        archivoTemp = archivo;
                    else
                        zona._ListaDeArchivos.Add(archivo);

                    resultado++;
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return resultado;
        }

        public int AddAllFilesToLocalRepository(String p_Mensaje)
        {
            int resultado = 0;
            ZonaDeTrabajo zonaDesde = RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == ZonasDeTrabajoEnum.Index).FirstOrDefault();
            ZonaDeTrabajo zonaHasta = RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == ZonasDeTrabajoEnum.LocalRepository).FirstOrDefault();

            if (zonaHasta._ListaDeArchivos == null)
                zonaHasta._ListaDeArchivos = new List<ArchivoDeTextoPlano>();

            if (zonaHasta._ListaCommit == null)
                zonaHasta._ListaCommit = new List<Commit>();

            //Se instancia objeto commit
            Commit commit = new Commit();
            commit._Fecha = DateTime.Now;
            commit._Mensaje = p_Mensaje;
            commit._Copiado = false;
            commit._ListaDeArchivos = new List<ArchivoDeTextoPlano>();

            foreach (ArchivoDeTextoPlano p in zonaDesde._ListaDeArchivos)
            {
                var archivo = zonaHasta._ListaDeArchivos.FirstOrDefault(x => x._Nombre == p._Nombre);
                if (archivo != null && !String.IsNullOrEmpty(archivo._Contenido))
                    archivo = p;
                else
                    zonaHasta._ListaDeArchivos.Add(p);

                commit._ListaDeArchivos.Add(p);

                resultado += 1;
            }

            zonaDesde._ListaDeArchivos.Clear();
            zonaHasta._ListaCommit.Add(commit);

            return resultado;
        }

        public int AddAllFilesToRemoteRepository()
        {
            ZonaDeTrabajo zonaDesde = RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == ZonasDeTrabajoEnum.LocalRepository).FirstOrDefault();
            ZonaDeTrabajo zonaHasta = RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == ZonasDeTrabajoEnum.RemoteRepository).FirstOrDefault();
            if (zonaHasta._ListaDeArchivos == null)
            {
                zonaHasta._ListaDeArchivos = new List<ArchivoDeTextoPlano>();
            }

            int respuesta = 0;
            if (zonaDesde._ListaCommit != null)
            {
                foreach (Commit p in zonaDesde._ListaCommit)
                {
                    if (!p._Copiado)
                    {
                        foreach (ArchivoDeTextoPlano p1 in p._ListaDeArchivos)
                        {
                            zonaHasta._ListaDeArchivos.Add(p1);
                            respuesta += 1;
                        }
                    }
                }
            }
            return respuesta;
        }
    }
}
