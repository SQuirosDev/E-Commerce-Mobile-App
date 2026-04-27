using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class ResObtenerConversacion : ResBase
    {
        public Conversaciones Conversacion { get; set; }
        public List<Mensajes> ListaMensajes { get; set; }
    }
}