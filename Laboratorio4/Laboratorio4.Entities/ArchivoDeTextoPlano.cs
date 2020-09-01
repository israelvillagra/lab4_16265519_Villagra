using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio4.Entities
{
    public class ArchivoDeTextoPlano
    {
        public String _Nombre;
        private int _Version;
        private DateTime _FechaModificacion;
        private String _Contenido;

        public ArchivoDeTextoPlano(String p_Nombre, String p_Contenido)
        {
            _Nombre = p_Nombre;
            _Contenido = p_Contenido;
            _Version = 1;
            _FechaModificacion = DateTime.Now;
        }
    }
}
