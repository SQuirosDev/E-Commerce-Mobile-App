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
    public class LogMensajes
    {
        public async Task<ResIngresarMensaje> Ingresar(ReqIngresarMensaje req)
        {
            ResIngresarMensaje res = new ResIngresarMensaje();
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
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/mensajes/ingresar", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/mensajes/ingresar", jsonContent);
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

                        res = JsonConvert.DeserializeObject<ResIngresarMensaje>(responseContent);

                        return res;
                    }
                    else
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResIngresarMensaje>(responseContent);

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
