using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio4.Entities
{
    [Serializable]
    public class ArchivoDeTextoPlanoModel: ArchivoDeTextoPlano
    {
        public String _FechaCommit;
        public int Identificador_Commit;

        public ArchivoDeTextoPlanoModel(ArchivoDeTextoPlano p_Archivo,Commit p_Commit)
        {
            this._Contenido = p_Archivo._Contenido;
            this._FechaCommit = p_Commit._Fecha.ToString();
            this._FechaModificacion = p_Archivo._FechaModificacion;
            this._Nombre = p_Archivo._Nombre;
            this._Version = p_Archivo._Version;
            this.Identificador_Commit = p_Commit._Identificador;
        }
    }
}
