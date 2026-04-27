using AccesoDatosMovil.AccesoDatos;
using BackendMovil.Entidades;
using BackendMovil.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Logicas
{
    public class LogMensajes // SignalR
    {
        // Ingresa solo un mensaje
        public ResIngresarMensaje Ingresar(ReqIngresarMensaje req)
        {
            ResIngresarMensaje res = new ResIngresarMensaje();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();

            try
            {
                #region Validacion

                res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdConversacion", "IdUsuario"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req.Mensaje, "Mensaje"));

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
                            LinQ.SP_INGRESAR_MENSAJE(req.Id.IdConversacion, req.Id.IdUsuario, req.Mensaje.Mensaje, ref idBD, ref errorIdBD, ref errorMensajeBD);
                        }
                    }
                    catch (SqlException exSQL) //Elegante!! 
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionBaseDatos(exSQL, false));
                    }

                    // Validacion si todo esta bien desde la BD
                    if (idBD >= 1)
                    {
                        res.Resultado = true;
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.MensajeAgregado, "Se creo el mensaje correctamente"));
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
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(ex, false));
            }

            return res;
        }

        // Obtiene una lista de mensajes, este llama en otro metodo del backend
        public ResListarMensajes Listar(ReqListarMensajes req, int idConversacion)
        {
            ResListarMensajes res = new ResListarMensajes();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaMensajes = new List<Mensajes>();

            try
            {
                #region Validacion

                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "idConversacion"));

                #endregion Validacion

                if (req.Estado == (int)EnumEstadoSesion.Cerrada || res.ListaErrores.Any())
                {
                    res.Resultado = false;
                }
                else
                {
                    List<SP_LISTAR_MENSAJESResult> listaMensajesTC = new List<SP_LISTAR_MENSAJESResult>();

                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            listaMensajesTC = LinQ.SP_LISTAR_MENSAJES(idConversacion, ref idBD, ref errorIdBD, ref errorMensajeBD).ToList();
                        }
                    }
                    catch (SqlException exSQL) //Elegante!! 
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionBaseDatos(exSQL, false));
                    }

                    // Validacion si todo esta bien desde la BD
                    if (idBD >= 1)
                    {
                        foreach (SP_LISTAR_MENSAJESResult MensajeTC in listaMensajesTC)
                        {
                            res.ListaMensajes.Add(new Factorias().FactoriaMensaje(MensajeTC));
                        }

                        res.Resultado = true;
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.MensajesObtenidos, "Se obtuvo todas los mensajes de la conversacion correctamente"));
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
                res.ListaErrores.Add(new Excepciones().ExcepcionLogica(ex, false));
            }

            return res;
        }
    }
}