using BackendMovil.Entidades;
using BackendMovil.Logicas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RestAPIMovil.Controllers
{
    public class ProductosController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/productos/ingresar")]
        public async Task<ResIngresarProducto> Ingresar(ReqIngresarProducto req)
        {
            return await new LogProductos().Ingresar(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/productos/listar")]
        public ResListarProductos Listar(ReqListarProductos req)
        {
            return new LogProductos().Listar(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/productos/listarFiltrado")]
        public ResListarProductosFiltrados ListarFiltrado(ReqListarProductosFiltrados req)
        {
            return new LogProductos().ListarFiltrado(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/productos/ListarRecomendados")]
        public async Task<ResListarProductosRecomendados> ListarRecomendados(ReqListarProductosRecomendados req)
        {
            return await new LogProductos().ListarRecomendados(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/productos/Obtener")]
        public ResObtenerProducto Obtener(ReqObtenerProducto req)
        {
            return new LogProductos().Obtener(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/productos/ListarBuscados")]
        public ResListarProductosBuscados ListarBuscados(ReqListarProductosBuscados req)
        {
            return new LogProductos().ListarBuscados(req);
        }
    }
}