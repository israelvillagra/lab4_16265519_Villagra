﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio4.Entities
{
    /// <summary>
    /// Objeto que contiene la lista de archivos involucrados en el commit, con más información
    /// </summary>
    [Serializable]
    public class Commit
    {
        public String _Autor;
        public int _Identificador;
        public Boolean _Copiado;
        public List<ArchivoDeTextoPlano> _ListaDeArchivos;
        public DateTime _Fecha;
        public String _Mensaje;

        public Commit() { }
    }
}
