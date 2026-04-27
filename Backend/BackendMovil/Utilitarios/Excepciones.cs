using AccesoDatosMovil.AccesoDatos;
using Algolia.Search.Exceptions;
using BackendMovil.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace BackendMovil.Utilitarios
{
    public class Excepciones
    {
        string mensajeFrontend = "Ha habido en error, intente mas tarde";

        public string GuardarExcepciones(bool resultado, int errorCode, string mensage, string ex, string exMessage)
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
                    LinQ.SP_INGRESAR_ERROR_BACKEND(errorCode.ToString(), mensage, exMessage, resultado, ref idBD, ref errorIdBD, ref errorMensajeBD);
                }

                return mensage;
            }
            catch (Exception excepcion)
            {
                return "Ha habido en error muy grave";
            }
        }

        // Excepciones
        public Errores ExcepcionLogica(Exception excepcion, bool resultado)
        {
            string resMensjae = this.GuardarExcepciones(resultado, (int)EnumExcepciones.ExcepcionLogica, mensajeFrontend, excepcion.ToString(), excepcion.Message);

            return new Helpers().CrearError((int)EnumExcepciones.ExcepcionLogica, resMensjae);
        }
        public Errores ExcepcionBaseDatos(SqlException excepcionSQL, bool resultado)
        {
            string resMensjae = this.GuardarExcepciones(resultado, (int)EnumExcepciones.ExcepcionBaseDatos, mensajeFrontend, excepcionSQL.ToString(), excepcionSQL.Message);

            return new Helpers().CrearError((int)EnumExcepciones.ExcepcionBaseDatos, resMensjae);
        }
        public Errores ExcepcionAlgolia(AlgoliaException exAlgolia, bool resultado)
        {
            string resMensjae = this.GuardarExcepciones(resultado, (int)EnumExcepciones.ExcepcionAlgolia, mensajeFrontend, exAlgolia.ToString(), exAlgolia.Message);

            return new Helpers().CrearError((int)EnumExcepciones.ExcepcionAlgolia, resMensjae);
        }
        public Errores ExcepcionMail(SmtpException exMail, bool resultado)
        {
            string resMensjae = this.GuardarExcepciones(resultado, (int)EnumExcepciones.ExcepcionMail, mensajeFrontend, exMail.ToString(), exMail.Message);

            return new Helpers().CrearError((int)EnumExcepciones.ExcepcionMail, resMensjae);
        }
        public Errores ExcepxionAPI(ReqExcepcionAPI req)
        {
            string resMensjae = this.GuardarExcepciones(req.resultado, (int)EnumExcepciones.ExcepcionAPI, mensajeFrontend, req.exAPI, req.exMensaje);

            return new Helpers().CrearError((int)EnumExcepciones.ExcepcionAPI, resMensjae);
        }
        public Errores Error(bool resultado, int errorIdBD, string errorMensageBD)
        {
            string resMensjae = this.GuardarExcepciones(resultado, errorIdBD, errorMensageBD, "", "");

            return new Helpers().CrearError((int)EnumExcepciones.Excepcion, resMensjae);
        }
    }
}