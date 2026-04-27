using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class ResListarProductos : ResBase
    {
        public List<Productos> ListaProductos { get; set; }
    }
}