using BackendMovil.Entidades;
using BackendMovil.Logicas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RestAPIMovil.Controllers
{
    public class ConversacionesController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/conversaciones/ingresar")]
        public ResIngresarConversacion Ingresar(ReqIngresarConversacion req)
        {
            return new LogConversaciones().Ingresar(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/conversaciones/obtener")]
        public  ResObtenerConversacion Obtener(ReqObtenerConversacion req)
        {
            return new LogConversaciones().Obtener(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/conversaciones/listar")]
        public ResListarConversacion Listar(ReqListarConversaciones req)
        {
            return new LogConversaciones().Listar(req);
        }
    }
}