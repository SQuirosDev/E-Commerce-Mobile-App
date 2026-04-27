using FrontendMovil.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMovil.Utilitarios
{
    public class Excepciones
    {
        string mensajeFrontend = "Ha habido en error en el sistema, intente mas tarde";

        public async void GuardarExcepciones(ReqExcepcionAPI req)
        {
            StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    await httpClient.PostAsync("https://localhost:44396/api/excepciones", jsonContent);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"{req.resultado} \n{(int)EnumExcepciones.ExcepcionAPI} \n{mensajeFrontend} \n{ex} \n{ex.Message}");
            }
        }

        //Generar Error
        public Errores CrearError(int TipoEnum, string Mensaje)
        {
            Errores error = new Errores();

            error.ErrorCode = TipoEnum;
            error.Message = Mensaje;

            return error;
        }

        // Excepciones
        public Errores ExcepcionAPI(HttpRequestException exAPI, bool resultado)
        {
            ReqExcepcionAPI req = new ReqExcepcionAPI();
            req.exAPI = exAPI.ToString();
            req.exMensaje = exAPI.Message;
            req.resultado = resultado;

            this.GuardarExcepciones(req);

            return this.CrearError((int)EnumExcepciones.ExcepcionAPI, mensajeFrontend);
        }
        public Errores ExcepcionLogica(Exception ex, bool resultado)
        {
            ReqExcepcionAPI req = new ReqExcepcionAPI();
            req.exAPI = ex.ToString();
            req.exMensaje = ex.Message;
            req.resultado = resultado;

            this.GuardarExcepciones(req);

            return this.CrearError((int)EnumExcepciones.ExcepcionAPI, mensajeFrontend);
        }
    }
}
