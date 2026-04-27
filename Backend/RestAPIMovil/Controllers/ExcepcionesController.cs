using BackendMovil.Entidades;
using BackendMovil.Logicas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace RestAPIMovil.Controllers
{
    public class ExcepcionesController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/excepciones")]
        public ResExcepcionAPI Ingresar(ReqExcepcionAPI req)
        {
            return new LogExcepciones().Excepcion(req);
        }
    }
}