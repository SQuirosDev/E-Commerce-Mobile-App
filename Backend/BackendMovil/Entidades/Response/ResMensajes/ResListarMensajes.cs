using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class ResListarMensajes : ResBase
    {
        public List<Mensajes> ListaMensajes { get; set; }
    }
}