using FrontendMovil.Entidades;
using FrontendMovil.Utilitarios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMovil.Logicas
{
    public class LogProductos
    {
        public async Task<ResIngresarProducto> Ingresar(ReqIngresarProducto req)
        {
            ResIngresarProducto res = new ResIngresarProducto();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaErrores = new List<Errores>();

            try
            {
                if (req != null)
                {

                    StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    HttpResponseMessage respuestaHttp = new HttpResponseMessage();

                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/productos/ingresar", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/productos/ingresar", jsonContent);
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionAPI(ex, res.Resultado));
                    }

                    if (respuestaHttp.IsSuccessStatusCode)
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResIngresarProducto>(responseContent);

                        return res;
                    }
                    else
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResIngresarProducto>(responseContent);

                        return res;
                    }
                }
                else
                {
                    res.Resultado = false;
                    res.ListaErrores.Add(new Errores { ErrorCode = -1, Message = "Falta informacion" });

                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(ex, res.Resultado));

                return res;
            }
        }

        public async Task<ResListarProductos> Listar(ReqListarProductos req)
        {
            ResListarProductos res = new ResListarProductos();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaErrores = new List<Errores>();
            res.ListaProductos = new List<Productos>();

            try
            {
                if (req != null)
                {
                    StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    HttpResponseMessage respuestaHttp = new HttpResponseMessage();

                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/productos/listar", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/productos/listar", jsonContent);
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionAPI(ex, res.Resultado));
                    }

                    if (respuestaHttp.IsSuccessStatusCode)
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResListarProductos>(responseContent);

                        return res;
                    }
                    else
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResListarProductos>(responseContent);

                        return res;
                    }
                }
                else
                {
                    res.Resultado = false;
                    res.ListaErrores.Add(new Errores { ErrorCode = -1, Message = "Falta informacion" });

                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(ex, res.Resultado));

                return res;
            }
        }

        public async Task<ResListarProductosFiltrados> ListarFiltrados(ReqListarProductosFiltrados req)
        {
            ResListarProductosFiltrados res = new ResListarProductosFiltrados();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaErrores = new List<Errores>();
            res.ListaProductosFiltrados = new List<Productos>();

            try
            {
                if (req != null)
                {
                    StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    HttpResponseMessage respuestaHttp = new HttpResponseMessage();

                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/productos/listarFiltrado", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/productos/listarFiltrado", jsonContent);
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionAPI(ex, res.Resultado));
                    }

                    if (respuestaHttp.IsSuccessStatusCode)
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResListarProductosFiltrados>(responseContent);

                        return res;
                    }
                    else
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResListarProductosFiltrados>(responseContent);

                        return res;
                    }
                }
                else
                {
                    res.Resultado = false;
                    res.ListaErrores.Add(new Errores { ErrorCode = -1, Message = "Falta informacion" });

                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(ex, res.Resultado));

                return res;
            }
        }

        public async Task<ResListarProductosRecomendados> ListarRecomendados(ReqListarProductosRecomendados req)
        {
            ResListarProductosRecomendados res = new ResListarProductosRecomendados();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaErrores = new List<Errores>();
            res.ListaProductosRecomendados = new List<Productos>();

            try
            {
                if (req != null)
                {
                    StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    HttpResponseMessage respuestaHttp = new HttpResponseMessage();

                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/productos/ListarRecomendados", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/productos/ListarRecomendados", jsonContent);
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionAPI(ex, res.Resultado));
                    }

                    if (respuestaHttp.IsSuccessStatusCode)
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResListarProductosRecomendados>(responseContent);

                        return res;
                    }
                    else
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResListarProductosRecomendados>(responseContent);

                        return res;
                    }
                }
                else
                {
                    res.Resultado = false;
                    res.ListaErrores.Add(new Errores { ErrorCode = -1, Message = "Falta informacion" });

                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(ex, res.Resultado));

                return res;
            }
        }

        public async Task<ResObtenerProducto> Obtener(ReqObtenerProducto req)
        {
            ResObtenerProducto res = new ResObtenerProducto();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaErrores = new List<Errores>();
            res.Producto = new Productos();
            res.ListaFeedbacks = new List<FeedBacks>();

            try
            {
                if (req != null)
                {
                    StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    HttpResponseMessage respuestaHttp = new HttpResponseMessage();

                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/productos/Obtener", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/productos/Obtener", jsonContent);
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionAPI(ex, res.Resultado));
                    }

                    if (respuestaHttp.IsSuccessStatusCode)
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResObtenerProducto>(responseContent);

                        return res;
                    }
                    else
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResObtenerProducto>(responseContent);

                        return res;
                    }
                }
                else
                {
                    res.Resultado = false;
                    res.ListaErrores.Add(new Errores { ErrorCode = -1, Message = "Falta informacion" });

                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(ex, res.Resultado));

                return res;
            }
        } 

        public async Task<ResListarProductosBuscados> ListarBuscados (ReqListarProductosBuscados req)
        {
            ResListarProductosBuscados res = new ResListarProductosBuscados();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaErrores = new List<Errores>();
            res.ListaProductosBuscados = new List<Productos>();

            try
            {
                if (req != null)
                {
                    StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    HttpResponseMessage respuestaHttp = new HttpResponseMessage();

                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/productos/ListarBuscados", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/productos/ListarBuscados", jsonContent);
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionAPI(ex, res.Resultado));
                    }

                    if (respuestaHttp.IsSuccessStatusCode)
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResListarProductosBuscados>(responseContent);

                        return res;
                    }
                    else
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResListarProductosBuscados>(responseContent);

                        return res;
                    }
                }
                else
                {
                    res.Resultado = false;
                    res.ListaErrores.Add(new Errores { ErrorCode = -1, Message = "Falta informacion" });

                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(ex, res.Resultado));

                return res;
            }
        }
    }
}