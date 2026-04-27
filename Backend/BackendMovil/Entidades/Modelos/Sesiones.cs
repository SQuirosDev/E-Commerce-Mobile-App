using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class Sesiones
    {
        public Usuarios Usuario { get; set; }
        public int Estado { get; set; }
        public DateTime Inicio { get; set; }
    }
}