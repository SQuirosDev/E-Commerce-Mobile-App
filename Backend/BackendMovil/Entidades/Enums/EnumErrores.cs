using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public enum EnumErrores
    {
        // General
        RequestNulo = 1,
        AtributoVacio = 2,
        AtributoNoEncontrado = 3,

        // Usuario
        NombreFaltante = 4,
        CorreoFaltante = 5,
        CorreoInvalido = 6,
        PasswordFaltante = 7,
        PasswordInvalido = 8,
        TelefonoFaltante = 9,
        CodigoVerificacionErroneo = 10,
        CodigoVerificacionFaltante = 11,

        // FeedBack
        IdProductoFaltante = 12,
        IdClienteFaltante = 13,
        CalificacionFaltante = 14,
        ComentarioFaltante = 15,

        // Producto
        CategoriaFaltante = 16,
        IdUsuarioFaltante = 17,
        DescripcionFaltante = 18,
        PrecioFaltante = 19,
        PrecioAlquilerFaltante = 20,
        TipoOperacionFaltante = 21,
        StockFaltante = 22,
        PalabraClaveFaltante = 23,
        ImagenFaltante = 24,
        ProductoFaltante = 25,

        // Conversaciones
        IdUsuario1Faltante = 26,
        IdUsuario2Faltante = 27,
        EstadoSoporteFaltante = 28,

        // Mensajes
        IdConversacionFaltante = 29,
        MensajeFaltante = 30,
        EstadoLeidoFaltante = 31,

    }
}