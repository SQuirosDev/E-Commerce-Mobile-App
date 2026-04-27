using BackendMovil.Entidades;
using BackendMovil.Logicas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RestAPIMovil.Controllers
{
    public class MensajesController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/mensajes/ingresar")]
        public ResIngresarMensaje Ingresar(ReqIngresarMensaje req)
        {
            return new LogMensajes().Ingresar(req);
        }
    }
}