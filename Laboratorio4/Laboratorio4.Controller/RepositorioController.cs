using Laboratorio4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Laboratorio4.Entities.Utiles.EnumeradoresUtiles;

namespace Laboratorio4.Controller
{
    

    public class RepositorioController
    {
        private Repositorio RespitorioLaboratorio;

        public RepositorioController(String p_Nombre, String p_Autor)
        {
            gitInit(p_Nombre, p_Autor);
        }

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

    }
}
