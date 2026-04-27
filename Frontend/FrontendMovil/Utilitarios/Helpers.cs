using FrontendMovil.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace FrontendMovil.Utilitarios
{
    public class Helpers
    {
        public void CrearSesion(int idUsuario, string rol, bool estadoUsuario, string nombre, string correo, string telefono, double longitud, double latitud, int estadoSesion, DateTime fecha)
        {
            Sesiones.Usuario = new Usuarios
            {
                IdUsuario = idUsuario,
                Rol = rol,
                Estado = estadoUsuario,
                Nombre = nombre,
                Correo = correo,
                Telefono = telefono,
                Longitud = longitud,
                Latitud = latitud
            };
            Sesiones.Estado = estadoSesion;
            Sesiones.Inicio = fecha;
        }

        public List<Productos> ListaLimitada(List<Productos> lista)
        {
            List<Productos> ListaProductos = new List<Productos>();

            if (lista == null || lista.Count() == 0)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < 5 && i <= lista.Count() - 1; i++)
                {
                    ListaProductos.Add(lista[i]);
                }

                return ListaProductos;
            }
        }

        public List<Productos> ListaLimitada2(List<Productos> lista)
        {
            List<Productos> ListaProductos = new List<Productos>();

            if (lista == null || lista.Count() == 0)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < Math.Floor(lista.Count() * 0.2); i++)
                {
                    ListaProductos.Add(lista[i]);
                }

                return ListaProductos;
            }
        }

        public async Task<byte[]> ConvertirABytesAsync(FileResult file)
        {
            if (file == null)
            {
                return null;
            }

            try
            {
                using var stream = await file.OpenReadAsync();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error convirtiendo imagen: {ex.Message}");
                return null;
            }
        }
    }
}
