using AccesoDatosMovil.AccesoDatos;
using Algolia.Search.Models.Recommend;
using BackendMovil.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BackendMovil.Utilitarios
{
    public class Factorias
    {
        // Feedbacks
        public FeedBacks FactoriaFeedback(SP_OBTENER_FEEDBACKResult feedback)
        {
            return new FeedBacks
            {
                IdFeedback = feedback.ID_FEEDBACK,
                IdProducto = feedback.ID_PRODUCTO,
                IdUsuario = feedback.ID_USUARIO,
                Calificacion = feedback.CALIFICACION,
                Comentario = feedback.COMENTARIO,
                FechaFeedback = (DateTime)feedback.FECHA_FEEDBACK,
            };
        }

        // Productos
        public Productos FactoriaProductos(SP_LISTAR_PRODUCTOResult producto)
        {
            return new Productos
            {
                IdProducto = producto.ID_PRODUCTO,
                IdUsuario = producto.ID_USUARIO,
                Nombre = producto.NOMBRE,
                Descripcion = producto.DESCRIPCION,
                Precio = producto.PRECIO,
                PrecioAlquiler = (decimal)producto.PRECIO_ALQUILER,
                TipoOperacion = (char)producto.TIPO_OPERACION,
                Stock = (int)producto.STOCK,
                CalificacionPromedio = (double)producto.CALIFICACION_PROMEDIO,
                FechaPublicacion = (DateTime)producto.FECHA_PUBLICACION,
                Categoria = producto.CATEGORIA,
                Imagen = producto.IMAGEN.ToArray()
            };
        }
        public Productos FactoriaProductosFiltrado(SP_LISTAR_PRODUCTOS_FILTRADOSResult producto)
        {
            return new Productos
            {
                IdProducto = producto.ID_PRODUCTO,
                IdUsuario = producto.ID_USUARIO,
                Nombre = producto.NOMBRE,
                Descripcion = producto.DESCRIPCION,
                Precio = producto.PRECIO,
                PrecioAlquiler = (decimal)producto.PRECIO_ALQUILER,
                TipoOperacion = (char)producto.TIPO_OPERACION,
                Stock = (int)producto.STOCK,
                CalificacionPromedio = (double)producto.CALIFICACION_PROMEDIO,
                FechaPublicacion = (DateTime)producto.FECHA_PUBLICACION,
                Categoria = producto.CATEGORIA,
                Imagen = producto.IMAGEN.ToArray()
            };
        }
        public Productos FactoriaProductoAlgolia(SP_INSERTAR_PRODUCTOResult producto)
        {
            return new Productos
            {
                IdProducto = producto.ID_PRODUCTO,
                IdUsuario = producto.ID_USUARIO,
                Nombre = producto.NOMBRE,
                Descripcion = producto.DESCRIPCION,
                Precio = producto.PRECIO,
                PrecioAlquiler = (decimal)producto.PRECIO_ALQUILER,
                TipoOperacion = (char)producto.TIPO_OPERACION,
                Stock = (int)producto.STOCK,
                CalificacionPromedio = (double)producto.CALIFICACION_PROMEDIO,
                FechaPublicacion = (DateTime)producto.FECHA_PUBLICACION,
                Categoria = producto.CATEGORIA,
                Imagen = producto.IMAGEN.ToArray()
            };
        }

        //Tira error, hasta que no este creado algolia no va a servir

        //public Productos FactoriaProductosRecomendados(RecommendationsHit hit)
        //{
        //    Productos productoRetornar = new Productos();

        //    // Falta el id usuario/vendedor
        //    productoRetornar.IdProducto = hit.IdProducto;
        //    //productoRetornar.IdUsuario = producto.IdUsuario
        //    productoRetornar.Nombre = hit.Nombre;
        //    productoRetornar.Descripcion = hit.Descripcion;
        //    productoRetornar.Precio = hit.Precio;
        //    productoRetornar.PrecioAlquiler = hit.PrecioAlquiler;
        //    productoRetornar.TipoOperacion = hit.TipoOperacion;
        //    productoRetornar.Stock = hit.Stock;

        //    return productoRetornar;
        //}

        public Productos FactoriaProducto(SP_OBTENER_PRODUCTOResult producto)
        {
            return new Productos
            {
                IdProducto = producto.ID_PRODUCTO,
                IdUsuario = producto.ID_USUARIO,
                Nombre = producto.NOMBRE,
                Descripcion = producto.DESCRIPCION,
                Precio = producto.PRECIO,
                PrecioAlquiler = (decimal)producto.PRECIO_ALQUILER,
                TipoOperacion = (char)producto.TIPO_OPERACION,
                Stock = (int)producto.STOCK,
                CalificacionPromedio = (double)producto.CALIFICACION_PROMEDIO,
                FechaPublicacion = (DateTime)producto.FECHA_PUBLICACION,
                Categoria = producto.CATEGORIA,
                Imagen = producto.IMAGEN.ToArray()
            };
        }
        public Productos FactoriaProductosBuscados(SP_PRODUCTOS_BUSCADOSResult producto)
        {
            return new Productos
            {
                IdProducto = producto.ID_PRODUCTO,
                IdUsuario = producto.ID_USUARIO,
                Nombre = producto.NOMBRE,
                Descripcion = producto.DESCRIPCION,
                Precio = producto.PRECIO,
                PrecioAlquiler = (decimal)producto.PRECIO_ALQUILER,
                TipoOperacion = (char)producto.TIPO_OPERACION,
                Stock = (int)producto.STOCK,
                CalificacionPromedio = (double)producto.CALIFICACION_PROMEDIO,
                FechaPublicacion = (DateTime)producto.FECHA_PUBLICACION,
                Categoria = producto.CATEGORIA,
                Imagen = producto.IMAGEN.ToArray()
            };
        }
        public Productos FactoriaProductosRandom(SP_LISTAR_PRODUCTOS_RANDOMResult producto)
        {
            return new Productos
            {
                IdProducto = producto.ID_PRODUCTO,
                IdUsuario = producto.ID_USUARIO,
                Nombre = producto.NOMBRE,
                Descripcion = producto.DESCRIPCION,
                Precio = producto.PRECIO,
                PrecioAlquiler = (decimal)producto.PRECIO_ALQUILER,
                TipoOperacion = (char)producto.TIPO_OPERACION,
                Stock = (int)producto.STOCK,
                CalificacionPromedio = (double)producto.CALIFICACION_PROMEDIO,
                FechaPublicacion = (DateTime)producto.FECHA_PUBLICACION,
                Categoria = producto.CATEGORIA,
                Imagen = producto.IMAGEN.ToArray()
            };
        }

        // Usuarios
        public Usuarios FactoriaUsuario(SP_OBTENER_USUARIOResult usuario)
        {
            return new Usuarios
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.NOMBRE,
                Correo = usuario.CORREO,
                Telefono = usuario.TELEFONO,
                Longitud = (double)usuario.LONGITUD,
                Latitud = (double)usuario.LATITUD,
                Rol = usuario.ROL,
                Estado = (bool)usuario.ESTADO
            };
        }
        public Usuarios FactoriaUsuarioLogueado(SP_LOGIN_USUARIOResult usuario)
        {
            return new Usuarios
            {
                IdUsuario = (int)usuario.ID_USUARIO,
                Nombre = usuario.NOMBRE,
                Correo = usuario.CORREO,
                Telefono = usuario.TELEFONO,
                Longitud = (double)usuario.LONGITUD,
                Latitud = (double)usuario.LATITUD,
                Rol = usuario.ROL,
                Estado = (bool)usuario.ESTADO,
                Semilla1 = usuario.SEMILLA_1,
                Semilla2 = usuario.SEMILLA_2,
                Contrasena = usuario.CONTRASENA,
            };
        }
        public Verificaciones FatoriaCodigoVerificacion(SP_OBTENER_VERIFICACIONResult verificacion)
        {
            Verificaciones verificacionRetornar = new Verificaciones();

            verificacionRetornar.IdVerificacion = verificacion.ID_VERIFICACION;
            verificacionRetornar.Correo = verificacion.CORREO;
            verificacionRetornar.CodigoVerificacion = verificacion.CODEVERIFICACION;
            verificacionRetornar.FechaRegistro = (DateTime)verificacion.FECHA_REGISTRO;

            return verificacionRetornar;
        }
        public Usuarios FatoriaVendedores(SP_MAPAResult vendedor)
        {
            return new Usuarios
            {
                IdUsuario = vendedor.ID_USUARIO,
                Nombre = vendedor.NOMBRE,
                Correo = vendedor.CORREO,
                Telefono = vendedor.TELEFONO,
                Longitud = (double)vendedor.LONGITUD,
                Latitud = (double)vendedor.LATITUD,
                Rol = vendedor.ROL,
                Estado = (bool)vendedor.ESTADO
            };
        }

        public Usuarios FactoriaUsuarioVendedor(SP_CAMBIAR_ROL_A_VENDEDORResult usuario)
        {
            return new Usuarios
            {
                IdUsuario = usuario.ID_USUARIO,
                Nombre = usuario.NOMBRE,
                Correo = usuario.CORREO,
                Telefono = usuario.TELEFONO,
                Longitud = (double)usuario.LONGITUD,
                Latitud = (double)usuario.LATITUD,
                Rol = usuario.ROL,
                Estado = (bool)usuario.ESTADO
            };
        }

        // Conversaciones
        public Conversaciones FactoriaConversacion(SP_OBTENER_CONVERSACIONResult conversacion)
        {
            return new Conversaciones
            {
                IdConversacion = conversacion.ID_CONVERSACION,
                IdUsuario1 = conversacion.ID_USUARIO1,
                IdUsuario2 = conversacion.ID_USUARIO2,
                FechaCreacion = (DateTime)conversacion.FECHA_CREACION,
                EsSoporte = (bool)conversacion.ES_SOPORTE
            };
        }
        public Conversaciones FactoriaConversaciones(SP_LISTAR_CONVERSACIONESResult conversacion)
        {
            return new Conversaciones
            {
                IdConversacion = conversacion.ID_CONVERSACION,
                IdUsuario1 = conversacion.ID_USUARIO1,
                IdUsuario2 = conversacion.ID_USUARIO2,
                FechaCreacion = (DateTime)conversacion.FECHA_CREACION,
                EsSoporte = (bool)conversacion.ES_SOPORTE
            };
        }
        
        // Mensajes
        public Mensajes FactoriaMensaje(SP_LISTAR_MENSAJESResult mensaje)
        {
            return new Mensajes
            {
                IdMensaje = mensaje.ID_MENSAJES,
                IdConversacion = mensaje.ID_CONVERSACION,
                IdUsuario = mensaje.ID_USUARIO,
                Mensaje = mensaje.MENSAJE,
                FechaCreacion = (DateTime)mensaje.FECHA_CREACION
            };
        }

        // Historial Visitas
        public HistorialVisitas FactoriaHistorialVisita(SP_OBTENER_HISTORIAL_VISITASResult historialVisita)
        {
            return new HistorialVisitas
            {
                IdHistorialVisita = historialVisita.ID_HISTORIAL_VISITAS,
                IdUsuario = historialVisita.ID_USUARIO,
                IdProducto = historialVisita.ID_PRODUCTO,
                FechaVisita = (DateTime)historialVisita.FECHA_VISTA
            };
        }

        // Request
        public ReqListarProductosRandom FactoriaReqListarProductoRandom(ReqListarProductosRecomendados recomendados)
        {
            return new ReqListarProductosRandom
            {
                // Copiar las propiedades comunes de ReqBase
                Estado = recomendados.Estado,
                Id = recomendados.Id
            };
        }

        public ReqObtenerHistorialVisitas FactoriaReqObtenerHistorialVisisita(ReqListarProductosRecomendados recomendados)
        {
            return new ReqObtenerHistorialVisitas
            {
                // Copiar las propiedades comunes de ReqBase
                Estado = recomendados.Estado,
                Id = recomendados.Id
            };
        }

        public ReqIngresarHistorialVisita FactoriaReqIngresarHistorialVisita(ReqObtenerProducto producto)
        {
            return new ReqIngresarHistorialVisita
            {
                // Copiar las propiedades comunes de ReqBase
                Estado = producto.Estado,
                Id = producto.Id
            };
        }

        public ReqListarFeedbacks FactoriaReqListarFeedback(ReqObtenerProducto producto)
        {
            return new ReqListarFeedbacks
            {
                // Copiar las propiedades comunes de ReqBase
                Estado = producto.Estado,
                Id = producto.Id
            };
        }
    }
}