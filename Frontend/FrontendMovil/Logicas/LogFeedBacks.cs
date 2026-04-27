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
    class LogFeedBacks
    {
        public async Task<ResIngresarFeedback > Ingresar(ReqIngresarFeedback req)
        {
            ResIngresarFeedback res = new ResIngresarFeedback();
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
                            //respuestaHttp = await httpClient.PostAsync("https://sharex.azurewebsites.net/api/feedbacks/ingresar", jsonContent);
                            respuestaHttp = await httpClient.PostAsync("https://localhost:44396/api/feedbacks/ingresar", jsonContent);
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

                        res = JsonConvert.DeserializeObject<ResIngresarFeedback>(responseContent);

                        return res;
                    }
                    else
                    {
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();

                        res = JsonConvert.DeserializeObject<ResIngresarFeedback>(responseContent);

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