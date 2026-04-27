using BackendMovil.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class HistorialVisitas
    {
        public int IdHistorialVisita { get; set; } //Autogenera, no se valida
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public DateTime FechaVisita { get; set; } // Autogenera, no se valida
    }
}