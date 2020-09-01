using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Laboratorio4.Entities.Utiles.EnumeradoresUtiles;

namespace Laboratorio4.Entities
{
    public class ZonaDeTrabajo
    {
        public List<ArchivoDeTextoPlano> _ListaDeArchivos;
        public List<Commit> _ListaCommit;
        public String Nombre;
        public ZonasDeTrabajoEnum NombreZonaDeTrabajo;
    }
}
