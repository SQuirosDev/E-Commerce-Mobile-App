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
    class LogConversaciones
    {
        public async Task<ResIngresarConversacion> Ingresar(ReqIngresarConversacion req)
        {
            ResIngresarConversacion res = new ResIngresarConversacion();
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
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/conversaciones/ingresar\r\n", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/conversaciones/ingresar", jsonContent);

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

                        res = JsonConvert.DeserializeObject<ResIngresarConversacion>(responseContent);

                        return res;
                    }
                    else
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResIngresarConversacion>(responseContent);

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

        public async Task<ResObtenerConversacion> Obtener(ReqObtenerConversacion req)
        {
            ResObtenerConversacion res = new ResObtenerConversacion();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaErrores = new List<Errores>();
            res.Conversacion = new Conversaciones();
            res.ListaMensajes = new List<Mensajes>();

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
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/conversaciones/obtener", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/conversaciones/obtener", jsonContent);
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

                        res = JsonConvert.DeserializeObject<ResObtenerConversacion>(responseContent);

                        return res;
                    }
                    else
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResObtenerConversacion>(responseContent);

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

        public async Task<ResListarConversacion> Listar(ReqListarConversaciones req)
        {
            ResListarConversacion res = new ResListarConversacion();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaErrores = new List<Errores>();
            res.ListaConversaciones = new List<Conversaciones>();

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
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/conversaciones/listar", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/conversaciones/listar", jsonContent);
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

                        res = JsonConvert.DeserializeObject<ResListarConversacion>(responseContent);

                        return res;
                    }
                    else
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResListarConversacion>(responseContent);

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
