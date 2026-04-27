using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class ReqExcepcionAPI
    {
        public string exAPI { get; set; }
        public string exMensaje { get; set; }
        public bool resultado { get; set; }
    }
}