using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMovil.Entidades
{
    public class FeedBacks
    {
        public int  IdFeedback { get; set; } // Autogenera, no se valida
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; } // No se valida, viene de la sesion
        public decimal Calificacion { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaFeedback { get; set; } // Autogenera, no se valida
    }
}