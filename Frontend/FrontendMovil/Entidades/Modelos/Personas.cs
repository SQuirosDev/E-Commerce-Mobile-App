using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMovil.Entidades
{
    public class Personas
    {
        public int IdPersona { get; set; } // Autogenera, no se valida
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; } // Autogenera, no se valida
        public double Longitud { get; set; }
        public double Latitud { get; set; }
    }
} 