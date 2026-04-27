using BackendMovil.Entidades;
using BackendMovil.Logicas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RestAPIMovil.Controllers
{
    public class UsuariosController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuarios/registrar")]
        public ResRegistrar Registrar(ReqRegistrar req)
        {
            return new LogUsuarios().Registrar(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuarios/login")]
        public ResLogin Login(ReqLogin req)
        {
            return new LogUsuarios().Login(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuarios/obtener")]
        public ResObtenerUsuarios Obtener(ReqObtenerUsuario req)
        {
            return new LogUsuarios().Obtener(req);
        }

        [System.Web.Http.HttpPost] // Update, Put?
        [System.Web.Http.Route("api/usuarios/actualizarContrasena")]
        public ResActualizarContrasena ActualizarContrasena(ReqActualizarContrasena req)
        {
            return new LogUsuarios().ActualizarContrasena(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuarios/CodigoVerificacion")]
        public ResCogidoVerificacion CodigoVerificacion(ReqCodigoVerificacion req)
        {
            return new LogUsuarios().CodigoVerificacion(req);
        }

        [System.Web.Http.HttpPost]// Update, Put?
        [System.Web.Http.Route("api/usuarios/CambiarRol")]
        public ResCambiarRol CambiarRol(ReqCambiarRol req)
        {
            return new LogUsuarios().CambiarRol(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/usuarios/MapaVendedores")]
        public ResMapaVendedores MapaVendedores(ReqMapaVendedores req)
        {
            return new LogUsuarios().MapaVendedores(req);
        }
    }
}