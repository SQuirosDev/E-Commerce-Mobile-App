using BackendMovil.Entidades;
using BackendMovil.Logicas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RestAPIMovil.Controllers
{
    public class FeedbacksController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/feedbacks/ingresar")]
        public ResIngresarFeedback Ingresar(ReqIngresarFeedback req)
        {
            return new LogFeedbacks().Ingresar(req);
        }
    }
}