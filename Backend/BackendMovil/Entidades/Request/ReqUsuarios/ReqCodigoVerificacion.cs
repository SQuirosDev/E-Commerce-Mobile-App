using BackendMovil.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class ReqCodigoVerificacion
    {
        public string  Correo { get; set; }
        public string CodigoVerificacion { get; set; }
    }
}
