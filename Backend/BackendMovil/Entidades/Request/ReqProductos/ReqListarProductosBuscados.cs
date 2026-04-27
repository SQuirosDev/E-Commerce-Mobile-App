using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class ReqListarProductosBuscados : ReqBase
    {
        public string Busqueda { get; set; }
    }
}
