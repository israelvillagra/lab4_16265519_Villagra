using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio4.Entities
{
    /// <summary>
    /// Objeto que contiene todos los objetos para ejecutar el programa
    /// </summary>
    [Serializable]
    public class Repositorio
    {
        public List<ZonaDeTrabajo> _ListaZonasDeTrabajo;
        public String _Nombre;
        public String _Autor;
        public Repositorio()
        { }
    }
}
