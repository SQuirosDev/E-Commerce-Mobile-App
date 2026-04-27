using AccesoDatosMovil.AccesoDatos;
using BackendMovil.Entidades;
using BackendMovil.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BackendMovil.Logicas
{
    public class LogConversaciones // SignalR
    {
        // Ingresa solo una conversacion
        public ResIngresarConversacion Ingresar(ReqIngresarConversacion req)
        {
            ResIngresarConversacion res = new ResIngresarConversacion();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaErrores = new List<Errores>();

            try
            {
                #region Validacion

                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdUsuario"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req.Conversacion, "IdUsuario2", "EsSoporte"));

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
                            LinQ.SP_INGRESAR_CONVERSACION(req.Id.IdUsuario, req.Conversacion.IdUsuario2, req.Conversacion.EsSoporte, ref idBD, ref errorIdBD, ref errorMensajeBD);
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
                        res.Resultado = true;
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.ConversacionAgregada, "Se creo la conversacion correctamente"));
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
        // Obtiene una conversacion y su propio lista de mensajes
        public ResObtenerConversacion Obtener(ReqObtenerConversacion req)
        {
            ResObtenerConversacion res = new ResObtenerConversacion();
            res.ListaErrores = new List<Errores>();
            res.ListaMensajes = new List<Mensajes>();
            res.ListaAciertos = new List<Aciertos>();

            try
            {
                #region Validacion

                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdConversacion"));

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
                            res.Conversacion = new Factorias().FactoriaConversacion(LinQ.SP_OBTENER_CONVERSACION(req.Id.IdConversacion, ref idBD, ref errorIdBD, ref errorMensajeBD).ToList().First());
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
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.ConversacionYMensajesObtenidos, "Se obtuvo la conversacion correctamente"));

                        ResListarMensajes resListarMensajes = new ResListarMensajes();
                        resListarMensajes = new LogMensajes().Listar(new ReqListarMensajes(), res.Conversacion.IdConversacion);

                        if (resListarMensajes.Resultado)
                        {
                            res.ListaMensajes = resListarMensajes.ListaMensajes;

                            res.Resultado = true;
                            res.ListaAciertos.AddRange(resListarMensajes.ListaAciertos);
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaErrores.AddRange(resListarMensajes.ListaErrores);
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
        // Obtiene una lista de todas las conversaciones relacionadas con el Usuario
        public ResListarConversacion Listar(ReqListarConversaciones req)
        {
            ResListarConversacion res = new ResListarConversacion();
            res.ListaErrores = new List<Errores>();
            res.ListaConversaciones = new List<Conversaciones>();
            res.ListaAciertos = new List<Aciertos>();

            try
            {
                #region Validacion

                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdUsuario"));

                #endregion Validacion

                // No hay atributos a validar
                if (req.Estado == (int)EnumEstadoSesion.Cerrada || res.ListaErrores.Any())
                {
                    res.Resultado = false;
                }
                else
                {
                    List<SP_LISTAR_CONVERSACIONESResult> listaConversacionesTC = new List<SP_LISTAR_CONVERSACIONESResult>();

                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            listaConversacionesTC = LinQ.SP_LISTAR_CONVERSACIONES(req.Id.IdUsuario, ref idBD, ref errorIdBD, ref errorMensajeBD).ToList();
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
                        foreach ( SP_LISTAR_CONVERSACIONESResult conversacionTC in listaConversacionesTC)
                        {
                            res.ListaConversaciones.Add(new Factorias().FactoriaConversaciones(conversacionTC));
                        }

                        res.Resultado = true;
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.ConversacionesObtenidas, "Se obtuvo todas las conversaciones del usuario correctamente"));
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