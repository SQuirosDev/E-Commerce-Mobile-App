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
    class LogUsuarios
    {
        public async Task<ResRegistrar> Registrarse(ReqRegistrar req)
        {
            ResRegistrar res = new ResRegistrar();
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
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/usuarios/registrar", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/usuarios/registrar", jsonContent);
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

                        res = JsonConvert.DeserializeObject<ResRegistrar>(responseContent);

                        return res;
                    }
                    else
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResRegistrar>(responseContent);

                        return res;
                    }
                }
                else
                {
                    res.Resultado = false;
                    res.ListaErrores.Add(new Errores {ErrorCode = -1, Message = "Falta informacion"});

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

        public async Task<ResLogin> Login(ReqLogin req)
        {
            ResLogin res = new ResLogin();
            res.Usuario = new Usuarios();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();

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
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/usuarios/login", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/usuarios/login", jsonContent);
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionAPI(ex, res.Resultado));
                    }

                    if (respuestaHttp.IsSuccessStatusCode)
                    {
                        var responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResLogin>(responseContent); 

                        if (res.Resultado)
                        {
                            new Helpers().CrearSesion(res.Usuario.IdUsuario, res.Usuario.Rol, res.Usuario.Estado, res.Usuario.Nombre, res.Usuario.Correo, res.Usuario.Telefono, res.Usuario.Longitud, res.Usuario.Latitud, 1, DateTime.Now);

                            return res;
                        }
                        else
                        {
                            return res;
                        }
                    }
                    else
                    {
                        var responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResLogin>(responseContent);

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

        public async Task<ResObtenerUsuarios> Obener(ReqObtenerUsuario req)
        {
            ResObtenerUsuarios res = new ResObtenerUsuarios();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaErrores = new List<Errores>();
            res.Usuario = new Usuarios();

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
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/usuarios/obtener", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/usuarios/obtener", jsonContent);
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

                        res = JsonConvert.DeserializeObject<ResObtenerUsuarios>(responseContent);

                        return res;
                    }
                    else
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResObtenerUsuarios>(responseContent);

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

        public async Task<ResActualizarContrasena> ActualizarContrasena(ReqActualizarContrasena req)
        {
            ResActualizarContrasena res = new ResActualizarContrasena();
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
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/usuarios/actualizarContrasena", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/usuarios/actualizarContrasena", jsonContent);
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionAPI(ex, res.Resultado));
                    }

                    if (respuestaHttp.IsSuccessStatusCode)
                    {
                        var responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResActualizarContrasena>(responseContent);

                        return res;
                    }
                    else
                    {
                        var responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResActualizarContrasena>(responseContent);

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

        public async Task<ResCogidoVerificacion> CodigoVerificacion(ReqCodigoVerificacion req)
        {
            ResCogidoVerificacion res = new ResCogidoVerificacion();
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
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/usuarios/CodigoVerificacion", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/usuarios/CodigoVerificacion", jsonContent);

                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionAPI(ex, res.Resultado));
                    }

                    if (respuestaHttp.IsSuccessStatusCode)
                    {
                        var responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResCogidoVerificacion>(responseContent);

                        return res;
                    }
                    else
                    {
                        var responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResCogidoVerificacion>(responseContent);

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

        public async Task<ResCambiarRol> CambiarRol(ReqCambiarRol req)
        {
            ResCambiarRol res = new ResCambiarRol();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaErrores = new List<Errores>();
            res.Usuario = new Usuarios();

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
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/usuarios/CambiarRol", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/usuarios/CambiarRol", jsonContent);
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

                        res = JsonConvert.DeserializeObject<ResCambiarRol>(responseContent);

                        new Helpers().CrearSesion(res.Usuario.IdUsuario, res.Usuario.Rol, res.Usuario.Estado, res.Usuario.Nombre, res.Usuario.Correo, res.Usuario.Telefono, res.Usuario.Longitud, res.Usuario.Latitud, 1, DateTime.Now);

                        return res;
                    }
                    else
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResCambiarRol>(responseContent);

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
