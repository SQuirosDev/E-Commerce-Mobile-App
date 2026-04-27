using AccesoDatosMovil.AccesoDatos;
using Algolia.Search.Exceptions;
using Algolia.Search.Models.Recommend;
using BackendMovil.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackendMovil.Utilitarios
{
    public class Helpers
    {
        public string Hashing(string contrasena, string semilla1, string semilla2)
         {
            string preHashing = contrasena.Insert(contrasena.Length / 2, semilla2);
            preHashing = semilla1 + preHashing + semilla1;

            // Hashea
            byte[] textoBytes = Encoding.UTF8.GetBytes(preHashing);

            return Convert.ToBase64String(textoBytes);
        }
        
        public StringContent SerializarJson(object req)
        {
            return new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
        }
        
        public string GenerarCodigoVerificacion()
        {
            return new Random().Next(100000, 999999).ToString(); // Genera un número entre 100000 y 999999
        }

        public List<Productos> ExtraerInfoAlgolia(GetRecommendationsResponse response)
        {
            // Se intenta obtener la lista de "Hits" dentro del primer elemento de "Results".
            // "Hits" contiene la lista de productos recomendados.
            var hits = response?.Results?.FirstOrDefault()?.Hits;

            // Lista donde se almacenarán los productos transformados
            List<Productos> productosDeserializados = new List<Productos>();

            // Si hits no es null, se recorren sus elementos y se transforman en Productos
            if (hits != null)
            {
                foreach (var hit in hits)
                {
                    //productosDeserializados.Add(new Factorias().FactoriaProductosRecomendados(hit));
                }
            }

            return productosDeserializados;
        }

        public Errores CrearError(int TipoEnum, string Mensaje)
        {
            return new Errores { ErrorCode = TipoEnum, Message = Mensaje };
        }

        public Aciertos CrearAcierto(int TipoEnum, string Mensaje)
        {
            return new Aciertos { AciertoCode = TipoEnum, Message = Mensaje };
        }

        public (bool resultado, Errores error, Aciertos acierto) EnviarCorreoVerificacion(string destinatario, string CodigoVerificacion)
        {
            Errores error = new Errores();
            Aciertos acierto = new Aciertos();
            bool resultado = false;

            // Configuración del remitente
            string correoRemitente = ""; // Cambia esto a tu correo
            string contraseñaApp = ""; // Usa una contraseña de aplicación de Gmail

            // Crear el mensaje de correo
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(correoRemitente),
                Subject = "Código de Verificación",
                IsBodyHtml = true, // Habilitar HTML
                Body = GenerarHTMLCorreo(CodigoVerificacion) // Método que genera el contenido HTML
            };

            // Agregar destinatario
            mailMessage.To.Add(destinatario);

            try
            {
                // Configuración del servidor SMTP de Gmail
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential(correoRemitente, contraseñaApp);
                    smtpClient.EnableSsl = true;

                    // Enviar correo
                    smtpClient.Send(mailMessage);
                }
            }
            catch (SmtpException exMail)
            {
                resultado = false;
                error = new Excepciones().ExcepcionMail(exMail, resultado);
            }

            resultado = true;
            acierto = this.CrearAcierto((int)EnumAciertos.UsuarioAgregado, "Correo enviado correctamente.");

            return (resultado, error, acierto);
        }

        private string GenerarHTMLCorreo(string codigoVerificacion)
        {
            return @"
            <html>
                <head>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            padding: 20px;
                        }
                        .container {
                            max-width: 600px;
                            background: #ffffff;
                            padding: 20px;
                            border-radius: 10px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                            text-align: center;
                        }
                        h1 {
                            color: #333;
                        }
                        .code {
                            font-size: 24px;
                            font-weight: bold;
                            color: #007bff;
                            margin: 20px 0;
                        }
                        .footer {
                            font-size: 12px;
                            color: #666;
                            margin-top: 20px;
                        }
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h1>Verificación de Cuenta</h1>
                        <p>Tu código de verificación es:</p>
                        <div class='code'>" + codigoVerificacion + @"</div>
                        <p>Por favor, ingrésalo en la aplicación para verificar tu cuenta.</p>
                        <div class='footer'>Este es un mensaje automático. No respondas a este correo.</div>
                    </div>
                </body>
            </html>";
        }

        public (string semilla1, string semilla2) CrearSemillas()
        {
            // Semilla 1 = Fecha
            // Semilla 2 = Guid
            return (DateTime.UtcNow.ToString("HH:mm:ss.fff"), Guid.NewGuid().ToString());
        }
    }
}