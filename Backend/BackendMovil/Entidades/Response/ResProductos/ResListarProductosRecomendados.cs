using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class ResListarProductosRecomendados : ResBase
    {
        public List<Productos> ListaProductosRecomendados { get; set; }
    }
}
