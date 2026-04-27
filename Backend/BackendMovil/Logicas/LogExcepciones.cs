using BackendMovil.Entidades;
using BackendMovil.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Logicas
{
    public class LogExcepciones
    {
        public ResExcepcionAPI Excepcion(ReqExcepcionAPI req)
        {
            ResExcepcionAPI res = new ResExcepcionAPI();
            res.ListaErrores = new List<Errores>();

            res.Resultado = false;
            res.ListaErrores.Add(new Excepciones().ExcepxionAPI(req));

            return res;
        }
    }
}
