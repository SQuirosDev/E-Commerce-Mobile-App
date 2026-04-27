using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMovil.Entidades
{
    public class Conversaciones
    {
        public int IdConversacion { get; set; } //Autogenera, no se valida
        public int IdUsuario1 { get; set; } // No se valida, viene de la sesion
        public int IdUsuario2 { get; set; }
        public DateTime FechaCreacion { get; set; } //Autogenera, no se valida
        public bool EsSoporte { get; set; }
    }
}