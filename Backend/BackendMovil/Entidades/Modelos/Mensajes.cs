using BackendMovil.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class Mensajes
    {
        public int IdMensaje { get; set; } // Autogenera, no se valida
        public int IdConversacion { get; set; }
        public int IdUsuario { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaCreacion { get; set; } // Autogenera, no se valida
    }
}