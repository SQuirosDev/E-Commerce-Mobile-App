using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class ResBase
    {
        public bool Resultado { get; set; }
        public List<Aciertos> ListaAciertos { get; set; }
        public List<Errores> ListaErrores { get; set; }
    }
}