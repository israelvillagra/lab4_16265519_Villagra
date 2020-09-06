using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio4.Entities
{
    /// <summary>
    /// Objeto que emula la creación de un archivo
    /// </summary>
    [Serializable]
    public class ArchivoDeTextoPlano
    {
        /// <summary>
        /// Se dejan publicos los valores para ser manejados
        /// </summary>
        public String _Nombre;
        public int _Version;
        public DateTime _FechaModificacion;
        public String _Contenido;

        public ArchivoDeTextoPlano()
        { }

        /// <summary>
        /// Constructor de Archivo de Texto
        /// </summary>
        /// <param name="p_Nombre">Nombre del Archivo</param>
        /// <param name="p_Contenido">Contenido del Archivo</param>
        public ArchivoDeTextoPlano(String p_Nombre, String p_Contenido)
        {
            _Nombre = p_Nombre;
            _Contenido = p_Contenido;
            _Version = 1;
            _FechaModificacion = DateTime.Now;
        }

    }
}
