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
    public class LogUsuarios
    {
        // Ingresa solo una persona
        public ResRegistrar Registrar(ReqRegistrar req)
        {
            ResRegistrar res = new ResRegistrar();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();

            try
            {
                #region Validacion

                    res.ListaErrores.AddRange(new Validaciones().Validar(req.Usuario, "Nombre", "Correo", "Telefono", "Contrasena", "Longitud", "Latitud"));
                    res.ListaErrores.AddRange(new Validaciones().ValidarCorreo(req.Usuario, "Correo"));
                    res.ListaErrores.AddRange(new Validaciones().ValidarContrasena(req.Usuario, "Contrasena"));

                #endregion Validacion

                if (res.ListaErrores.Any())
                {
                    res.Resultado = false;
                }
                else
                {   
                    Helpers helper = new Helpers();

                    (req.Usuario.Semilla1, req.Usuario.Semilla2) = helper.CrearSemillas();

                    req.Usuario.Contrasena = helper.Hashing(req.Usuario.Contrasena, req.Usuario.Semilla1, req.Usuario.Semilla2);

                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            LinQ.SP_REGISTRAR_USUARIO(req.Usuario.Nombre, req.Usuario.Correo, req.Usuario.Telefono, "Cliente", req.Usuario.Contrasena, req.Usuario.Semilla1, req.Usuario.Semilla2, req.Usuario.Longitud, req.Usuario.Latitud, false, ref idBD, ref errorIdBD, ref errorMensajeBD);
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
                        res.ListaAciertos.Add(helper.CrearAcierto((int)EnumAciertos.UsuarioAgregado, "El usuario fue creado correctamente"));

                        string CodigoVerificacion = helper.GenerarCodigoVerificacion();

                        // Outputs de la BD
                        int? idBD2 = 0;
                        int? errorIdBD2 = 0;
                        string errorMensajeBD2 = "";

                        //Entrada a la BD
                        try
                        {
                            using (ConexionDataContext LinQ = new ConexionDataContext())
                            {
                                LinQ.SP_INSERTA_VERIFICACION(req.Usuario.Correo, CodigoVerificacion, ref idBD2, ref errorIdBD2, ref errorMensajeBD2);
                            }
                        }
                        catch (SqlException exSQL) //Elegante!! 
                        {
                            res.Resultado = false;
                            res.ListaErrores.Add(new Excepciones().ExcepcionBaseDatos(exSQL, res.Resultado));
                        }

                        if (idBD2 >= 1)
                        {
                            Errores error = new Errores();
                            Aciertos acierto = new Aciertos();

                            (res.Resultado, error, acierto) = helper.EnviarCorreoVerificacion(req.Usuario.Correo, CodigoVerificacion);

                            // Valida si el correo se envio a no
                            if (res.Resultado)
                            {
                                res.ListaAciertos.Add(acierto);
                            }
                            else
                            {
                                res.ListaErrores.Add(error);
                            }
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaErrores.Add(new Excepciones().Error(res.Resultado, (int)errorIdBD2, errorMensajeBD2));
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
        // Ingresa correo y contrasena
        public ResLogin Login (ReqLogin req)
        {
            ResLogin res = new ResLogin();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();

            try
            {
                #region Validacion

                    res.ListaErrores.AddRange(new Validaciones().Validar(req, "Correo", "Contrasena"));
                    res.ListaErrores.AddRange(new Validaciones().ValidarCorreo(req, "Correo"));
                    res.ListaErrores.AddRange(new Validaciones().ValidarContrasena(req, "Contrasena"));

                #endregion Validacion

                if (res.ListaErrores.Any())
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
                            res.Usuario = new Factorias().FactoriaUsuarioLogueado(LinQ.SP_LOGIN_USUARIO(req.Correo, ref idBD, ref errorIdBD, ref errorMensajeBD).ToList().First());
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
                        if (req.Correo == res.Usuario.Correo)
                        {
                            req.Contrasena = new Helpers().Hashing(req.Contrasena, res.Usuario.Semilla1, res.Usuario.Semilla2);

                            if (req.Contrasena == res.Usuario.Contrasena)
                            {
                                res.Resultado = true;
                                res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.UsuarioLogueado, "Se logueo el usuario correctamente"));
                            }
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
        // Obtiene solo una persona/usuario/cliente 
        public ResObtenerUsuarios Obtener (ReqObtenerUsuario req)
        {
            ResObtenerUsuarios res = new ResObtenerUsuarios();
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
                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            res.Usuario = new Factorias().FactoriaUsuario(LinQ.SP_OBTENER_USUARIO(req.Id.IdUsuario, ref idBD, ref errorIdBD, ref errorMensajeBD).ToList().First());
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
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.UsuarioObtenido, "Se obtuvo el usuario correctamente"));
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
        // Ingresa correo y contrasena
        public ResActualizarContrasena ActualizarContrasena (ReqActualizarContrasena req)
        {
            ResActualizarContrasena res = new ResActualizarContrasena();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();

            try
            {
                #region Validacion

                    res.ListaErrores.AddRange(new Validaciones().Validar(req, "Correo", "Contrasena"));
                    res.ListaErrores.AddRange(new Validaciones().ValidarCorreo(req, "Correo"));
                    res.ListaErrores.AddRange(new Validaciones().ValidarContrasena(req, "Contrasena"));

                #endregion Validacion

                if (res.ListaErrores.Any())
                {
                    res.Resultado = false;
                }
                else
                {
                    Helpers helper = new Helpers();

                    (string semilla1, string semilla2) = helper.CrearSemillas();

                    req.Contrasena = helper.Hashing(req.Contrasena, semilla1, semilla2);

                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            LinQ.ACTUALIZAR_CONTRASENA(req.Correo, req.Contrasena, semilla1, semilla2, ref idBD, ref errorIdBD, ref errorMensajeBD);
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
                        res.ListaAciertos.Add(helper.CrearAcierto((int)EnumAciertos.ActualizarContrasenaCorrecto, "Se cambio la contrasena correctamente"));
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
        // Valida el codigo de verificacion
        public ResCogidoVerificacion CodigoVerificacion(ReqCodigoVerificacion req) 
        {
            ResCogidoVerificacion res = new ResCogidoVerificacion();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();

            try
            {
                #region Validacion

                    res.ListaErrores.AddRange(new Validaciones().Validar(req, "Correo", "CodigoVerificacion"));

                #endregion Validacion

                if (res.ListaErrores.Any())
                {
                    res.Resultado = false;
                }
                else
                {
                    string CodigoVerificacionBD = "";

                    // Outputs de la BD
                    int? idBD1 = 0;
                    int? errorIdBD1 = 0;
                    string errorMensajeBD1 = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            CodigoVerificacionBD = new Factorias().FatoriaCodigoVerificacion(LinQ.SP_OBTENER_VERIFICACION(req.Correo, ref idBD1, ref errorIdBD1, ref errorMensajeBD1).ToList().First()).CodigoVerificacion;
                        }
                    }
                    catch (SqlException exSQL) //Elegante!! 
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().ExcepcionBaseDatos(exSQL, res.Resultado));
                    }

                    // Validacion si todo esta bien desde la BD
                    if (idBD1 >= 1)
                    {
                        if (req.CodigoVerificacion == CodigoVerificacionBD)
                        {
                            // Outputs de la BD
                            int? idBD2 = 0;
                            int? errorIdBD2 = 0;
                            string errorMensajeBD2 = "";

                            // Entrada a la BD
                            try
                            {
                                using (ConexionDataContext LinQ = new ConexionDataContext())
                                {
                                    LinQ.SP_VERIFICACION_COMPLETADA(idBD1, req.Correo, true, ref idBD2, ref errorIdBD2, ref errorMensajeBD2);
                                }
                            }
                            catch (SqlException exSQL)
                            {
                                res.Resultado = false;
                                res.ListaErrores.Add(new Excepciones().ExcepcionBaseDatos(exSQL, res.Resultado));
                            }

                            if (idBD2 >= 1)
                            {
                                res.Resultado = true;
                                res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.CodigoVerificacionCorrecto, "El codigo de verificacion es correcto"));
                            }
                            else
                            {
                                res.Resultado = false;
                                res.ListaErrores.Add(new Excepciones().Error(res.Resultado, (int)errorIdBD2, errorMensajeBD2));
                            }
                        }
                        else
                        {
                            res.Resultado = false;
                            res.ListaErrores.Add(new Excepciones().Error(res.Resultado, (int)EnumErrores.CodigoVerificacionErroneo, "Codigo de verificacion incorrecto"));
                        }
                    }
                    else
                    {
                        res.Resultado = false;
                        res.ListaErrores.Add(new Excepciones().Error(res.Resultado, (int)errorIdBD1, errorMensajeBD1));
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
        // Cambiar el rol del usuario
        public ResCambiarRol CambiarRol(ReqCambiarRol req)
        {
            ResCambiarRol res = new ResCambiarRol();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();
            res.Usuario = new Usuarios();

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
                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            res.Usuario = new Factorias().FactoriaUsuarioVendedor(LinQ.SP_CAMBIAR_ROL_A_VENDEDOR(req.Id.IdUsuario, ref idBD, ref errorIdBD, ref errorMensajeBD).ToList().First());
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
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.CambiarRolCorrecto, "Se cambio el rol a vendedor correctamente"));
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
        //Mostrar vendedores en el mapa
        public ResMapaVendedores MapaVendedores(ReqMapaVendedores req)
        {
            ResMapaVendedores res = new ResMapaVendedores();
            res.ListaVendedores = new List<Usuarios>();
            res.ListaErrores = new List<Errores>();
            res.ListaAciertos = new List<Aciertos>();

            try
            {
                #region Validacion

                    res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
                    res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdUsuario"));
                res.ListaErrores.AddRange(new Validaciones().Validar(req, "Radio"));

                #endregion Validacion

                if (req.Estado == (int)EnumEstadoSesion.Cerrada || res.ListaErrores.Any())
                {
                    res.Resultado = false;
                }
                else
                {
                    List<SP_MAPAResult> ListaVendedoresTC = new List<SP_MAPAResult>();

                    // Outputs de la BD
                    int? idBD = 0;
                    int? errorIdBD = 0;
                    string errorMensajeBD = "";

                    // Entrada a la BD
                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            ListaVendedoresTC = LinQ.SP_MAPA(req.Id.IdUsuario,req.Radio, ref idBD, ref errorIdBD, ref errorMensajeBD).ToList();
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
                        foreach (SP_MAPAResult vendedorTC in ListaVendedoresTC)
                        {
                            res.ListaVendedores.Add(new Factorias().FatoriaVendedores(vendedorTC));
                        }

                        res.Resultado = true;
                        res.ListaAciertos.Add(new Helpers().CrearAcierto((int)EnumAciertos.CambiarRolCorrecto, "Se ha obtenido la lista de vendedores correctamente"));
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
    }
}