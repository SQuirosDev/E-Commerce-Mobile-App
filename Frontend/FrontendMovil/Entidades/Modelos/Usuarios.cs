using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMovil.Entidades
{
    public class Usuarios : Personas
    {
        public int IdUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        public bool Estado { get; set; }
    }
} 