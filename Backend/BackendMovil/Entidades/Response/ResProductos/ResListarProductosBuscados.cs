using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class ResListarProductosBuscados : ResBase
    {
        public List<Productos> ListaProductosBuscados { get; set; }
    }
}
