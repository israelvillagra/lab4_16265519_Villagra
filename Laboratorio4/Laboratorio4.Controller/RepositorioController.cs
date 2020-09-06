using Laboratorio4.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using static Laboratorio4.Entities.Utiles.EnumeradoresUtiles;

namespace Laboratorio4.Controller
{
    /// <summary>
    /// Controlador que contiene la mayor parte de la lógica del GitHub
    /// </summary>
    public class RepositorioController
    {
        /// <summary>
        /// Se crean los parametros, este repositorio queda como global.
        /// </summary>
        private static Repositorio RespitorioLaboratorio;

        /// <summary>
        /// Constructor declarado pero no implementado
        /// </summary>
        public RepositorioController() { }

        /// <summary>
        /// Función que exporta todo el repositorio unitario creado a un archivo XML
        /// </summary>
        /// <param name="p_PathFile">Ruta donde se Guardara el Archivo</param>
        /// <param name="resultado">Resultado de la creación del archivo 1:Exitoso, 0: Problemas</param>
        /// <returns>Mensaje para el usuario</returns>
        public String ExportRepository(string p_PathFile, ref bool resultado)
        {
            try
            {
                //por tema de pruebas, se genera la exportación de todo el repositorio en un archivo XML
                // para poder ser después importada, para acelerar el desarrollo
                XmlSerializer xs = new XmlSerializer(typeof(Repositorio));
                string fileName = p_PathFile + "\\" + RespitorioLaboratorio._Nombre + ".xml";
                //El nombre del archivo quedará con el nombre del repositorio
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

        /// <summary>
        /// Función que por temas de prueba de ingresa la importación de un repositorio exportado
        /// </summary>
        /// <param name="p_PathFile">Ruta del repositorio que fue exportado</param>
        /// <param name="p_Mensaje">Mensaje informando al usuario el estado de la impotacion</param>
        /// <returns>true: Exitoso; false: Fallido</returns>
        public static bool ImportRepository(string p_PathFile, ref String p_Mensaje)
        {
            try
            {
                ///Se utiliza la importación de un repositorio que ha sido exportado en un archivo XML
                using (var stream = new FileStream(p_PathFile, FileMode.Open, FileAccess.Read))
                {
                    stream.Position = 0;
                    var serializer = new XmlSerializer(typeof(Repositorio));
                    //se Desserializa el objeto y se reemplaza en local.
                    RespitorioLaboratorio = serializer.Deserialize(stream) as Repositorio;
                }
                p_Mensaje = "Se ha restaurado el repositorio " + RespitorioLaboratorio._Nombre;
                return true;
            }
            catch (Exception ex)
            {
                p_Mensaje = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// método publico pero no estatico que devuelve el repositorio global
        /// </summary>
        /// <returns></returns>
        public Repositorio GetRepository()
        {
            ///Reporta el repositorio completo para poder ser manejado como se necesite, para entrega de informción
            return RespitorioLaboratorio;
        }

        /// <summary>
        /// Inicializacion de de la Función GitInit, dando origen al parametro global dentro del controlador 'RespitorioLaboratorio'
        /// </summary>
        /// <param name="p_Nombre">Nombre del Creador</param>
        /// <param name="p_Autor">Autor del Repositorio</param>
        public RepositorioController(String p_Nombre, String p_Autor)
        {
            //Se llama a la función GitInit para inicializar el repositorio
            gitInit(p_Nombre, p_Autor);
        }

        /// <summary>
        /// Función interna donde se aplica el GetInir, iniciando el Objeto repositorio
        /// </summary>
        /// <param name="p_Nombre">Nombre del Repositorio</param>
        /// <param name="p_Autor">Autor del Repositorio</param>
        /// <returns>1: Exitoso; 0: Fallido</returns>
        public int gitInit(String p_Nombre, String p_Autor)
        {
            ///Se inicializan todos los objetos
            RespitorioLaboratorio = new Repositorio();

            //Se valida que contenga los valores minimos para inicializar el repositorio
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

        /// <summary>
        /// Obtiene la Zona de trabajo filtrado por nombre del enumerador
        /// </summary>
        /// <param name="p_ZonasDeTrabajoEnum">Enumerador con el nombre de la Zona de Trabajo</param>
        /// <returns>Zona de Trabajo</returns>
        public ZonaDeTrabajo obtenerZonaDeTrabajo(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum)
        {
            //Se realiza una busqueda rápida de la zona de trabajo
            return RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == p_ZonasDeTrabajoEnum).FirstOrDefault();
        }

        /// <summary>
        /// Función estatica que copia un Objeto Archivo de Texto a la Zona de trabajo que se indica
        /// </summary>
        /// <param name="p_ZonasDeTrabajoEnum">Enumerador indicando la Zona de Trabajo</param>
        /// <param name="p_Archivo">Objeto Archivo de Texto a incorporar</param>
        /// <returns>true:Exitoso </returns>
        public static bool AgregarArchivoZonaDeTrabajo(ZonasDeTrabajoEnum p_ZonasDeTrabajoEnum, ArchivoDeTextoPlano p_Archivo)
        {
            ZonaDeTrabajo zona = RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == p_ZonasDeTrabajoEnum).FirstOrDefault();
            if (zona._ListaDeArchivos == null)
                zona._ListaDeArchivos = new List<ArchivoDeTextoPlano>();

            zona._ListaDeArchivos.Add(p_Archivo);
            return true;
        }

        /// <summary>
        /// Función que se encarga de copiar la lista de Archivos a la Zona de Trabajo Index
        /// </summary>
        /// <param name="p_ListaArchivos">Lista de Objetos ArchivoDeTextoPlano que deben ser copiados</param>
        /// <returns>1:EXitoso; 0: Fallido</returns>
        public static int AddFileToIndex(List<ArchivoDeTextoPlano> p_ListaArchivos)
        {
            int resultado = 0;
            try
            {
                //Se busca la Zona de trabajo
                ZonaDeTrabajo zona = RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == ZonasDeTrabajoEnum.Index).FirstOrDefault();

                //Si no ha sido inicializada se crea la instancia
                if (zona._ListaDeArchivos == null)
                    zona._ListaDeArchivos = new List<ArchivoDeTextoPlano>();

                //Se comienza la copia del archivo
                foreach (ArchivoDeTextoPlano archivo in p_ListaArchivos)
                {
                    var archivoTemp = zona._ListaDeArchivos.FirstOrDefault(x => x._Nombre == archivo._Nombre);
                    //En el caso que el archivo exista lo reemplaza
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

        /// <summary>
        /// Función que copia la lista de objetos que se ingresar, a la Zona de Trabajo Local Repository, creando un Commit
        /// </summary>
        /// <param name="p_Mensaje">Mensaje de descripción del Commit</param>
        /// <param name="p_ListaArchivos">Opcional, Lista de Objetos que se deben incluin en el commit</param>
        /// <returns></returns>
        public int AddAllFilesToLocalRepository(String p_Mensaje, List<ArchivoDeTextoPlano> p_ListaArchivos = null)
        {
            int resultado = 0;
            //Obtiene las zona de trabajo de donde se deben copias hacia a la cual de deben copiar
            ZonaDeTrabajo zonaDesde = RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == ZonasDeTrabajoEnum.Index).FirstOrDefault();
            ZonaDeTrabajo zonaHasta = RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == ZonasDeTrabajoEnum.LocalRepository).FirstOrDefault();

            if (zonaHasta._ListaDeArchivos == null)
                zonaHasta._ListaDeArchivos = new List<ArchivoDeTextoPlano>();

            if (zonaHasta._ListaCommit == null)
                zonaHasta._ListaCommit = new List<Commit>();

            //Se instancia objeto commit
            int idComit = 1;
            if (zonaHasta._ListaCommit.Count > 0)
                idComit = zonaHasta._ListaCommit.Max(x => x._Identificador) + 1;
            Commit commit = new Commit();
            commit._Fecha = DateTime.Now;
            commit._Identificador = idComit;
            commit._Mensaje = p_Mensaje;
            commit._Copiado = false;
            commit._ListaDeArchivos = new List<ArchivoDeTextoPlano>();

            //se instancia el objeto si es nulo
            if (p_ListaArchivos == null)
                p_ListaArchivos = zonaDesde._ListaDeArchivos;


            foreach (ArchivoDeTextoPlano p in p_ListaArchivos)
            {
                //si el objeto existe se reemplaza
                var archivo = zonaHasta._ListaDeArchivos.FirstOrDefault(x => x._Nombre == p._Nombre);
                if (archivo != null && !String.IsNullOrEmpty(archivo._Contenido))
                    archivo = p;
                else
                    zonaHasta._ListaDeArchivos.Add(p);

                //Se agrega a la lista de objetos que estan relacionados con el commit
                commit._ListaDeArchivos.Add(p);

                resultado += 1;
            }

            zonaHasta._ListaCommit.Add(commit);

            return resultado;
        }

        /// <summary>
        /// Función que copia la lista de archivos de la Zona de Trabajo Local Repository a Remote Repository
        /// </summary>
        /// <returns>1: Exitoso; 0: Fallido</returns>
        public int AddAllFilesToRemoteRepository()
        {
            //Se obtiene la Zona de Trabajo de donde se copia, hacia la cual se deben copiar los archivos.
            ZonaDeTrabajo zonaDesde = RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == ZonasDeTrabajoEnum.LocalRepository).FirstOrDefault();
            ZonaDeTrabajo zonaHasta = RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == ZonasDeTrabajoEnum.RemoteRepository).FirstOrDefault();
            if (zonaHasta._ListaDeArchivos == null)
                zonaHasta._ListaDeArchivos = new List<ArchivoDeTextoPlano>();

            int respuesta = 0;
            if (zonaDesde._ListaCommit != null)
            {
                //Se comeinza a copiar la lista de objetos que provienen del commit, hacia la nueva zona de trabajo
                foreach (Commit p in zonaDesde._ListaCommit)
                {
                    if (!p._Copiado)
                    {
                        foreach (ArchivoDeTextoPlano p1 in p._ListaDeArchivos)
                        {
                            var archivo = zonaHasta._ListaDeArchivos.FirstOrDefault(x => x._Nombre == p1._Nombre);
                            if (archivo != null && !String.IsNullOrEmpty(archivo._Contenido))
                            {
                                //Si el objeti ya se encuentra se eliina y luego de copia
                                zonaHasta._ListaDeArchivos.Remove(archivo);
                                archivo = p1;
                                zonaHasta._ListaDeArchivos.Add(p1);
                            }
                            else
                                zonaHasta._ListaDeArchivos.Add(p1);
                            respuesta += 1;
                        }
                    }
                    //El commit queda como ya copiado, para posteriormente copiar sólo los que no se encuentran con el valor verdadero (true)
                    p._Copiado = true;
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Función que Copia todos los Archivos de la Zona de Trabajo Remote Repository hacia la Zona de
        /// trabajo WorkSpace, siendo todos reemplazados.
        /// </summary>
        /// <returns>1: Exitoso; 0: Fallido</returns>
        public int AddAllFilesToWorkSpave()
        {
            int resultado = 0;
            //Busca toda la lista de objetos desde la zona de inicio a la zona donde se copiarán
            ZonaDeTrabajo zonaDesde = RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == ZonasDeTrabajoEnum.RemoteRepository).FirstOrDefault();
            ZonaDeTrabajo zonaHasta = RespitorioLaboratorio._ListaZonasDeTrabajo.Where(x => x.NombreZonaDeTrabajo == ZonasDeTrabajoEnum.Workspace).FirstOrDefault();

            foreach (ArchivoDeTextoPlano p in zonaDesde._ListaDeArchivos)
            {
                //Se busca si contiene el archivo en la zona de trabajo donde se copiará, en el caso que existe lo elimina
                var archivo = zonaHasta._ListaDeArchivos.FirstOrDefault(x => x._Nombre.Equals(p._Nombre));
                if (archivo != null)
                    zonaHasta._ListaDeArchivos.Remove(archivo);

                //agrega el objeto a la lista
                zonaHasta._ListaDeArchivos.Add(p);
                resultado += 1;
            }
            return resultado;
        }
    }
}
