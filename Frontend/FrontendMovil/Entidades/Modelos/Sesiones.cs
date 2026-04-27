using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMovil.Entidades
{
    public static class Sesiones
    {
        public static Usuarios Usuario { get; set; }
        public static int Estado { get; set; }
        public static DateTime Inicio { get; set; }
    }
}