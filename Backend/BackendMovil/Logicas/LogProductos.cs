using AccesoDatosMovil.AccesoDatos;
using BackendMovil.Entidades;
using BackendMovil.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algolia.Search.Models.Recommend;

namespace BackendMovil.Logicas
{
    public class LogProductos
    {
        // Ingresa solo un producto, lo ingresa a la BD y despues Algolia
        public async Task<ResIngresarProducto> Ingresar(ReqIngresarProducto req)
        {
            ResIngresarProducto res = new ResIngresarProducto();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();

            try
            {
                #region Validacion

                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdUsuario"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req.Producto, "Nombre", "Descripcion", "Precio", "PrecioAlquiler", "TipoOperacion", "Stock", "Categoria", "Imagen"));

                #endregion Validacion

                if (req.Estado == (int)EnumEstadoSesion.Cerrada || res.ListaErrores.Any())
                {
                    res.Resultado = false;
                }
                else
                {
                    Productos productoBD = new Productos();

                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            productoBD = new Factorias().FactoriaProductoAlgolia(LinQ.SP_INSERTAR_PRODUCTO(req.Id.IdUsuario, req.Producto.Nombre, req.Producto.Descripcion, req.Producto.Precio, req.Producto.PrecioAlquiler, req.Producto.TipoOperacion, req.Producto.Stock, req.Producto.Categoria, req.Producto.Imagen, null, true, ref idBD, ref errorIdBD, ref errorMensajeBD).ToList().FirstOrDefault());
                        }
                    }
                    catch (SqlException exSQL) //Elegante!! 
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionBaseDatos(exSQL, res.Resultado));
                    }

                    // Validacion si todo esta bien desde la BD
                    if (idBD >= 1)
                    {
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int) EnumAciertos.ProductoAgregado, "Se guardo el producto correctamente"));

                        Aciertos acierto = new Aciertos();
                        Errores error = new Errores();

                        (res.Resultado, acierto, error) = await new LogAlgolia().IngresarProductos(productoBD);

                        if (res.Resultado)
                        {
                            res.ListaAciertos.Add(acierto);
                        }
                        else
                        {
                            res.ListaErrores.Add(error);
                        }
                    }
                    else
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().Error(res.Resultado, (int)errorIdBD, errorMensajeBD));
                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(ex, res.Resultado));
            }

            return res;
        }
        // Obtiene una lista de todos los productos
        public ResListarProductos Listar(ReqListarProductos req)
        {
            ResListarProductos res = new ResListarProductos();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaProductos = new List<Productos>();

            try
            {
                #region Validacion

                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));

                #endregion Validacion

                if (req.Estado == (int)EnumEstadoSesion.Cerrada || res.ListaErrores.Any())
                {
                    res.Resultado = false;
                }
                else
                {
                    List<SP_LISTAR_PRODUCTOResult> listaProdustosTC = new List<SP_LISTAR_PRODUCTOResult>();

                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            listaProdustosTC = LinQ.SP_LISTAR_PRODUCTO(ref idBD, ref errorIdBD, ref errorMensajeBD).ToList();
                        }
                    }
                    catch (SqlException exSQL) //Elegante!! 
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionBaseDatos(exSQL, res.Resultado));
                    }

                    // Validacion si todo esta bien desde la BD
                    if (idBD >= 1)
                    {
                        foreach (SP_LISTAR_PRODUCTOResult productoTC in listaProdustosTC)
                        {
                            res.ListaProductos.Add(new Factorias().FactoriaProductos(productoTC));
                        }

                        res.Resultado = true;
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.ProductosObtenidos, "Se obtuvo todas las productos correctamente"));
                    }
                    else
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().Error(res.Resultado, (int)errorIdBD, errorMensajeBD));
                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(ex, res.Resultado));
            }

            return res;
        }
        //Obtiene una lista de productos filtrados
        public ResListarProductosFiltrados ListarFiltrado(ReqListarProductosFiltrados req)
        {
            ResListarProductosFiltrados res = new ResListarProductosFiltrados();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaProductosFiltrados = new List<Productos>();

            try
            {
                #region Validacion

                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req, "TipoOperacion", "PalabraClave"));

                #endregion Validacion

                if (req.Estado == (int)EnumEstadoSesion.Cerrada || res.ListaErrores.Any())
                {
                    res.Resultado = false;
                }
                else
                {
                    List<SP_LISTAR_PRODUCTOS_FILTRADOSResult> listaProdustosTC = new List<SP_LISTAR_PRODUCTOS_FILTRADOSResult>();

                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            listaProdustosTC = LinQ.SP_LISTAR_PRODUCTOS_FILTRADOS(req.TipoOperacion, req.PalabraClave, ref idBD, ref errorIdBD, ref errorMensajeBD).ToList();
                        }
                    }
                    catch (SqlException exSQL) //Elegante!! 
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionBaseDatos(exSQL, res.Resultado));
                    }

                    if (idBD >= 1)
                    {
                        foreach (SP_LISTAR_PRODUCTOS_FILTRADOSResult productoTC in listaProdustosTC)
                        {
                            res.ListaProductosFiltrados.Add(new Factorias().FactoriaProductosFiltrado(productoTC));
                        }

                        res.Resultado = true;
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.ProductosFiltradosObtenidos, "Se obtuvo todas las productos filtrados correctamente"));
                    }
                    else
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().Error(res.Resultado, (int)errorIdBD, errorMensajeBD));
                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(ex, res.Resultado));
            }

            return res;
        }
        // Obtiene una lista de productos recomendados desde Algolia
        public async Task<ResListarProductosRecomendados> ListarRecomendados(ReqListarProductosRecomendados req)
        {
            ResListarProductosRecomendados res = new ResListarProductosRecomendados();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaProductosRecomendados = new List<Productos>();

            try
            {
                #region Validacion

                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdUsuario"));

                #endregion Validacion

                if (req.Estado == (int)EnumEstadoSesion.Cerrada || res.ListaErrores.Any())
                {
                    res.Resultado = false;
                }
                else
                {
                    // Obtiene el ultimo producto visto
                    ResObtenerHistorialVisitas resObtenerHistorialVisita = new ResObtenerHistorialVisitas();

                    resObtenerHistorialVisita = new LogHistorialVisitas().Obtener(new Factorias().FactoriaReqObtenerHistorialVisisita(req));

                    if (resObtenerHistorialVisita.Resultado)
                    {
                        res.ListaAciertos.AddRange(resObtenerHistorialVisita.ListaAciertos);

                        Aciertos acierto = new Aciertos();
                        Errores error = new Errores();
                        GetRecommendationsResponse resAlgolia = new GetRecommendationsResponse();

                        (res.Resultado, acierto, error, resAlgolia) = await new LogAlgolia().ListarProductos(resObtenerHistorialVisita.HistorialVisita);

                        // Validacion si todo esta bien desde Algolia
                        if (res.Resultado)
                        {
                            res.ListaProductosRecomendados.AddRange(new Helpers().ExtraerInfoAlgolia(resAlgolia));
                            res.ListaAciertos.Add(acierto);
                        }
                        else
                        {
                            ResListarProductosRandom resListarProductosRandom = new ResListarProductosRandom();
                            resListarProductosRandom = this.ListarRandom(new Factorias().FactoriaReqListarProductoRandom(req));

                            if (resListarProductosRandom.Resultado)
                            {
                                res.Resultado = true;
                                res.ListaProductosRecomendados.AddRange(resListarProductosRandom.ListaProductosRandom);
                                res.ListaAciertos.AddRange(resListarProductosRandom.ListaAciertos);
                            }
                            else
                            {
                                res.Resultado = false;
                                res.ListaErrores.AddRange(resListarProductosRandom.ListaErrores);
                            }
                        }
                    }
                    else
                    {
                        res.Resultado = false;
                        res.ListaErrores.AddRange(resObtenerHistorialVisita.ListaErrores);
                    }
                }
            }
            catch (Exception excepcion)
            {
                res.Resultado = false;
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(excepcion, false));
            }

            return res;
        }
        // Obtiene solo un producto y solo respectivos comentarios (Feedback)
        public ResObtenerProducto Obtener(ReqObtenerProducto req)
        {
            ResObtenerProducto res = new ResObtenerProducto();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaFeedbacks = new List<FeedBacks>();

            try
            {
                #region Validacion

                res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdProducto"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));

                #endregion Validacion

                if (req.Estado == (int)EnumEstadoSesion.Cerrada || res.ListaErrores.Any())
                {
                    res.Resultado = false;
                }
                else
                {
                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            res.Producto = new Factorias().FactoriaProducto(LinQ.SP_OBTENER_PRODUCTO(req.Id.IdProducto, ref idBD, ref errorIdBD, ref errorMensajeBD).ToList().First());
                        }
                    }
                    catch (SqlException exSQL) //Elegante!! 
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionBaseDatos(exSQL, res.Resultado));
                    }

                    // Validacion si se obtuvo el producto
                    if (idBD >= 1)
                    {
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.ProductoObtenido, "Se obtuvo el producto correctamente"));

                        ResListarFeedbacks resListarFeedback = new ResListarFeedbacks();
                        resListarFeedback = new LogFeedbacks().Listar(new Factorias().FactoriaReqListarFeedback(req));

                        // Validacion si se obtuvo la lista de feedbacks
                        if (resListarFeedback.Resultado)
                        {
                            res.ListaFeedbacks = resListarFeedback.ListaFeedbacks;
                            res.ListaAciertos.AddRange(resListarFeedback.ListaAciertos);

                            ResIngresarHistorialVisita resIngresarHistorialVisita = new ResIngresarHistorialVisita();
                            resIngresarHistorialVisita = new LogHistorialVisitas().Ingresar(new Factorias().FactoriaReqIngresarHistorialVisita(req));

                            // Validacion si se agrego el producto al historial de visitas
                            if (resIngresarHistorialVisita.Resultado)
                            {
                                res.Resultado = true;
                                res.ListaAciertos.AddRange(resIngresarHistorialVisita.ListaAciertos);
                            }
                            else
                            {
                                res.Resultado = false;
                                res.ListaErrores.AddRange(resIngresarHistorialVisita.ListaErrores);
                            }
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaErrores.AddRange(resListarFeedback.ListaErrores);
                        }
                    }
                    else
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().Error(res.Resultado, (int)errorIdBD, errorMensajeBD));
                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(ex, res.Resultado));
            }

            return res;
        }
        // Obtiene una lista de los productos buscados
        public ResListarProductosBuscados ListarBuscados(ReqListarProductosBuscados req)
        {
            ResListarProductosBuscados res = new ResListarProductosBuscados();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaProductosBuscados = new List<Productos>();

            try
            {
                #region Validaciones

                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Busqueda"));

                #endregion Validaciones

                if (req.Estado == (int)EnumEstadoSesion.Cerrada || res.ListaErrores.Any())
                {
                    res.Resultado = false;
                }
                else
                {
                    List<SP_PRODUCTOS_BUSCADOSResult> listaProdustosBuscadosTC = new List<SP_PRODUCTOS_BUSCADOSResult>();

                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            listaProdustosBuscadosTC = LinQ.SP_PRODUCTOS_BUSCADOS(req.Busqueda, ref idBD, ref errorIdBD, ref errorMensajeBD).ToList();
                        }
                    }
                    catch (SqlException exSQL) //Elegante!! 
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionBaseDatos(exSQL, res.Resultado));
                    }

                    // Validacion si todo esta bien desde la BD
                    if (idBD >= 1)
                    {
                        foreach (SP_PRODUCTOS_BUSCADOSResult productoTC in listaProdustosBuscadosTC)
                        {
                            res.ListaProductosBuscados.Add(new Factorias().FactoriaProductosBuscados(productoTC));
                        }

                        res.Resultado = true;
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.ProductosObtenidos, "Se obtuvo todas las productos buscados correctamente"));
                    }
                    else
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().Error(res.Resultado, (int)errorIdBD, errorMensajeBD));
                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(ex, res.Resultado));
            }

            return res;
        }
        // Obtiene una lista random de todos los productos
        public ResListarProductosRandom ListarRandom(ReqListarProductosRandom req)
        {
            ResListarProductosRandom res = new ResListarProductosRandom();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaProductosRandom = new List<Productos>();

            try
            {
                #region Validacion

                    res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));

                #endregion Validacion

                if (req.Estado == (int)EnumEstadoSesion.Cerrada || res.ListaErrores.Any())
                {
                    res.Resultado = false;
                }
                else
                {
                    List<SP_LISTAR_PRODUCTOS_RANDOMResult> listaProdustosTC = new List<SP_LISTAR_PRODUCTOS_RANDOMResult>();

                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            listaProdustosTC = LinQ.SP_LISTAR_PRODUCTOS_RANDOM(ref idBD, ref errorIdBD, ref errorMensajeBD).ToList();
                        }
                    }
                    catch (SqlException exSQL) //Elegante!! 
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionBaseDatos(exSQL, res.Resultado));
                    }

                    // Validacion si todo esta bien desde la BD
                    if (idBD >= 1)
                    {
                        foreach (SP_LISTAR_PRODUCTOS_RANDOMResult productoTC in listaProdustosTC)
                        {
                            res.ListaProductosRandom.Add(new Factorias().FactoriaProductosRandom(productoTC));
                        }

                        res.Resultado = true;
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.ProductosObtenidos, "Se obtuvo todas las productos correctamente"));
                    }
                    else
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().Error(res.Resultado, (int)errorIdBD, errorMensajeBD));
                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(ex, res.Resultado));
            }

            return res;
        }
    }
} 