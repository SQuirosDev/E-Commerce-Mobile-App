using AccesoDatosMovil.AccesoDatos;
using BackendMovil.Entidades;
using BackendMovil.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Logicas
{
    public class LogHistorialVisitas
    {
        // Ingresa el ultimo producto visto, se llame en otro metodo del backend
        public ResIngresarHistorialVisita Ingresar(ReqIngresarHistorialVisita req)
        {
            ResIngresarHistorialVisita res = new ResIngresarHistorialVisita();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();

            try
            {
                #region Validacion 

                res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdProducto", "IdUsuario"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));

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
                            LinQ.SP_INGRESAR_HISTORIAL_VISITA(req.Id.IdUsuario, req.Id.IdProducto, ref idBD, ref errorIdBD, ref errorMensajeBD);
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
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.HistorialVisitaAgregada, "Se creo el historial de visita del ultimo producto visto correctamente"));
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
        // Obtiene el ultimo producto visto, se llame en otro metodo del backend
        public ResObtenerHistorialVisitas Obtener(ReqObtenerHistorialVisitas req)
        {
            ResObtenerHistorialVisitas res = new ResObtenerHistorialVisitas();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();

            try
            {
                #region Validacion

                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdUsuario"));

                #endregion Validacion

                if (req.Estado == (int)EnumEstadoSesion.Cerrada || res.ListaErrores.Any())
                {
                    res.Resultado = false;
                }
                else
                {
                    Factorias factoria = new Factorias();

                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            res.HistorialVisita = factoria.FactoriaHistorialVisita(LinQ.SP_OBTENER_HISTORIAL_VISITAS(req.Id.IdUsuario, ref idBD, ref errorIdBD, ref errorMensajeBD).ToList().First());
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
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.HistorialVisitaAgregada, "Se obtuvo el historial de visita del ultimo producto visto correctamente"));
                    }
                    else if (idBD == 0)
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Helpers().CrearError((int)EnumAciertos.HistorialVisitaAgregada, "No hay registros de un historial de productos vistos"));
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
