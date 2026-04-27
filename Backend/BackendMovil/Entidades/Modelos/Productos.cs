using BackendMovil.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class Productos
    {
        public int IdProducto { get; set; } //Autogenera, no se valida
        public int IdUsuario { get; set; } // no se valida, viene de la sesion
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioAlquiler { get; set; }
        public char TipoOperacion { get; set; }
        public int Stock { get; set; }
        public double CalificacionPromedio { get; set; } // No se valida
        public DateTime FechaPublicacion { get; set; }
        public string Categoria { get; set; } 
        public byte[] Imagen { get; set; }
    }
}