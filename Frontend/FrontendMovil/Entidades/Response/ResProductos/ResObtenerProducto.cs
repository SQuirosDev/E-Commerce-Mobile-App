using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMovil.Entidades
{
    public class ResObtenerProducto : ResBase
    {
        public Productos Producto { get; set; }
        public List<FeedBacks> ListaFeedbacks { get; set; }
    }
}
