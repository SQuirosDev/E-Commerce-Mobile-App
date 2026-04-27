using BackendMovil.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackendMovil.Utilitarios
{
    public class Validaciones
    {
        #region Documentacion
        // <summary>
        // Valida que los atributos especificados de un objeto no sean nulos o vacíos.
        // </summary>
        // <typeparam name="T">El tipo del objeto a validar.</typeparam>
        // <param name="request">Instancia del objeto a validar.</param>
        // <param name="atributosValidar">Lista de nombres de los atributos a validar.</param>
        // <returns>Lista de errores encontrados.</returns>
        #endregion Documentacion
        public List<Errores> Validar<T>(T request, params string[] atributosValidar)
        {
            List<Errores> ListaErrores = new List<Errores>();

            // Request Nulo
            if (request == null)
            {
                ListaErrores.Add(new Errores
                {
                    ErrorCode = (int)EnumErrores.RequestNulo,
                    Message = "El request está vacío."
                });

                return ListaErrores;
            }

            // Obtener las propiedades de la clase
            var propiedades = typeof(T).GetProperties();

            // Iterar solo sobre los atributos que se deben validar
            foreach (var atributoNombre in atributosValidar)
            {
                var propiedad = propiedades.FirstOrDefault(p => p.Name == atributoNombre);

                if (propiedad != null)
                {
                    var valor = propiedad.GetValue(request);

                    if (valor == null || string.IsNullOrEmpty(valor.ToString()) || string.IsNullOrWhiteSpace(valor.ToString()))
                    {
                        ListaErrores.Add(new Errores
                        {
                            ErrorCode = (int)EnumErrores.AtributoVacio,
                            Message = $"El dato de {atributoNombre} no puede estar vacío."
                        });
                    }
                }
                else
                {
                    ListaErrores.Add(new Errores
                    {
                        ErrorCode = (int)EnumErrores.AtributoNoEncontrado,
                        Message = $"El dato de {atributoNombre} no existe en {typeof(T).Name}."
                    });
                }
            }

            return ListaErrores;
        }
        public List<Errores> ValidarCorreo<T>(T request, params string[] atributosValidar)
        {
            List<Errores> ListaErrores = new List<Errores>();

            // Request Nulo
            if (request == null)
            {
                ListaErrores.Add(new Errores
                {
                    ErrorCode = (int)EnumErrores.RequestNulo,
                    Message = "El request está vacío."
                });

                return ListaErrores;
            }  

            var propiedades = typeof(T).GetProperties();

            string patronCorreo = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"; // Expresión regular para correos

            foreach (var atributoNombre in atributosValidar)
            {
                var propiedad = propiedades.FirstOrDefault(p => p.Name == atributoNombre);

                if (propiedad != null)
                {
                    var valor = propiedad.GetValue(request)?.ToString();

                    if (string.IsNullOrEmpty(valor) || !Regex.IsMatch(valor, patronCorreo))
                    {
                        ListaErrores.Add(new Errores
                        {
                            ErrorCode = (int)EnumErrores.CorreoInvalido,
                            Message = $"El dato de {atributoNombre} no tiene un formato de correo válido."
                        });
                    }
                }
                else
                {
                    ListaErrores.Add(new Errores
                    {
                        ErrorCode = (int)EnumErrores.AtributoNoEncontrado,
                        Message = $"El dato de {atributoNombre} no existe en {typeof(T).Name}."
                    });
                }
            }

            return ListaErrores;
        }
        public List<Errores> ValidarContrasena<T>(T request, params string[] atributosValidar)
        {
            List<Errores> ListaErrores = new List<Errores>();

            // Request Nulo
            if (request == null)
            {
                ListaErrores.Add(new Errores
                {
                    ErrorCode = (int)EnumErrores.RequestNulo,
                    Message = "El request está vacío."
                });

                return ListaErrores;
            }

            var propiedades = typeof(T).GetProperties();

            string patronContrasena = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$";

            #region Documentacion
                /*
                La contraseña debe tener al menos:
                - Una mayúscula
                - Una minúscula
                - Un número
                - Un carácter especial
                - Mínimo 8 caracteres de longitud
                */
            #endregion Documentacion

            foreach (var atributoNombre in atributosValidar)
            {
                var propiedad = propiedades.FirstOrDefault(p => p.Name == atributoNombre);

                if (propiedad != null)
                {
                    var valor = propiedad.GetValue(request)?.ToString();

                    if (string.IsNullOrEmpty(valor) || !Regex.IsMatch(valor, patronContrasena))
                    {
                        ListaErrores.Add(new Errores
                        {
                            ErrorCode = (int)EnumErrores.PasswordInvalido,
                            Message = $"El dato de {atributoNombre} no es una contraseña segura."
                        });
                    }
                }
                else
                {
                    ListaErrores.Add(new Errores
                    {
                        ErrorCode = (int)EnumErrores.AtributoNoEncontrado,
                        Message = $"El dato de {atributoNombre} no existe en {typeof(T).Name}."
                    });
                }
            }

            return ListaErrores;
        }
    }
}