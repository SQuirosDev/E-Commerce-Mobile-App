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
    public class LogFeedbacks
    {
        // Ingresa solo un Feedback
        public ResIngresarFeedback Ingresar(ReqIngresarFeedback req)
        {
            ResIngresarFeedback res = new ResIngresarFeedback();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();

            try
            {
                #region Validacion

                res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdProducto", "IdUsuario"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req.Feedback, "Calificacion", "Comentario"));

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
                            LinQ.SP_INSERTAR_FEEDBACK(req.Id.IdProducto, req.Id.IdUsuario, req.Feedback.Calificacion, req.Feedback.Comentario, ref idBD, ref errorIdBD, ref errorMensajeBD);
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
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.ConversacionAgregada, "Se creo el feedback correctamente"));
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
        // Obtiene una Lista de Feedbacks (comentarios), este se llama en otro metodo del Backend
        public ResListarFeedbacks Listar(ReqListarFeedbacks req)
        {
            ResListarFeedbacks res = new ResListarFeedbacks();
            res.ListaErrores = new List<Errores>();
            res.ListaFeedbacks = new List<FeedBacks>();
            res.ListaAciertos = new List<Aciertos>();

            try
            {
                #region Validacion

                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdProducto"));

                #endregion Validacion

                if (req.Estado == (int)EnumEstadoSesion.Cerrada)
                {
                    res.Resultado = false;
                }
                else
                {
                    List<SP_OBTENER_FEEDBACKResult> listaFeedbacksTC = new List<SP_OBTENER_FEEDBACKResult>();

                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            listaFeedbacksTC = LinQ.SP_OBTENER_FEEDBACK(req.Id.IdProducto, ref idBD, ref errorIdBD, ref errorMensajeBD).ToList();
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
                        foreach (SP_OBTENER_FEEDBACKResult feedbackTC in listaFeedbacksTC)
                        {
                            res.ListaFeedbacks.Add(new Factorias().FactoriaFeedback(feedbackTC));
                        }

                        res.Resultado = true;
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.FeedbacksObtenidos, "Se obtuvo todas los feedbacks del producto correctamente"));
                    }
                    else if (idBD == 0)
                    {
                        res.Resultado = true;
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.FeedbacksObtenidos, "No hay comentario para el producto seleccionado"));
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