using Laboratorio4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio4.Controller.Utils
{
    public class UtilsController
    {
        public static Boolean FileExists(List<ArchivoDeTextoPlano> p_ListaArchivos, String p_Nombre)
        {
            if (p_ListaArchivos == null)
            {
                return false;
            }

            return p_ListaArchivos.Exists(x=>x._Nombre.Equals(p_Nombre));
        }
    }
}
