using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMovil.Entidades
{
    public class ReqCodigoVerificacion : ReqBase
    {
        public string  Correo { get; set; }
        public string CodigoVerificacion { get; set; }
    }
}
