using BackendMovil.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class ReqListarProductosFiltrados : ReqBase
    {
        public char TipoOperacion { get; set; }
        public string PalabraClave { get; set; }
    }
}