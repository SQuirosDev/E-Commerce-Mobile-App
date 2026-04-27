using Algolia.Search.Clients;
using Algolia.Search.Exceptions;
using Algolia.Search.Models.Recommend;
using Algolia.Search.Models.Search;
using BackendMovil.Entidades;
using BackendMovil.Utilitarios;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Logicas
{
    public class LogAlgolia
    {
        // Ingresa un producto al indice de algolia
        public async Task<(bool resultado, Aciertos acierto, Errores error)> IngresarProductos(Productos producto)
        {
            bool resultado = false;
            Aciertos acierto = new Aciertos();
            Errores error = new Errores();

            try
            {
                SearchClient client = new SearchClient("applicationId", "apiKey");
                Helpers helper = new Helpers();

                // Entrada a Algolia
                try
                {
                    SaveObjectResponse saveResp = await client.SaveObjectAsync("Productos", helper.SerializarJson(producto));

                    resultado = true;
                    acierto = helper.CrearAcierto((int)EnumAciertos.ProductoAgregado, "Se volvio a guardar el producto correctamente");
                }
                catch (AlgoliaException exAlgolia)
                {
                    resultado = false;
                    error = new Excepciones().ExcepcionAlgolia(exAlgolia, resultado);
                }
            }
            catch (Exception ex)
            {
                resultado = true; // 4/2/2026 ya no tenemos algolia por eso no sirve
                error = new Excepciones().ExcepcionLogica(ex, resultado);
            }

            return (resultado, acierto, error);
        }
        // Obtiene una lista de productos recomendados en base a un producto
        public async Task<(bool resultado, Aciertos acierto, Errores error, GetRecommendationsResponse responseAlgolia)> ListarProductos(HistorialVisitas historialVisita)
        {
            bool resultado = false;
            Aciertos acierto = new Aciertos();
            Errores error = new Errores();
            GetRecommendationsResponse responseAlgolia = new GetRecommendationsResponse(); // Almacena la respuesta

            try
            {
                // Crear objeto de parámetros con una única solicitud
                GetRecommendationsParams parametros = new GetRecommendationsParams
                {
                    // Se requiere la lista aunque sea de un solo elemento
                    Requests = new List<RecommendationsRequest>
                    {
                        // Crear la solicitud de recomendación
                        new RecommendationsRequest
                        (
                            new RelatedQuery
                            {
                                IndexName = "Productos", // nombre del indice
                                ObjectID = historialVisita.IdProducto.ToString(), // producto con el que se va hacer la relacion o la busqueda
                                Model = (RelatedModel)Enum.Parse(typeof(RelatedModel), "RelatedProducts", true), // tipo de metodo que va hacer algolia
                                Threshold = 42.1 // valor default de algolia
                                                    // Threshold: umbral de relevancia para las recomendaciones
                                                    // valor mas alto = productos mas semejantes
                                                    // valor mas bajo = productos con menor relevancia o similitud
                            }
                        )
                    }
                };

                // Entrada a Algolia
                try
                {
                    RecommendClient client = new RecommendClient(new RecommendConfig("appId", "apiKey")); // CLiente para conectar con la API

                    responseAlgolia = await client.GetRecommendationsAsync(parametros);

                    resultado = true;
                    acierto = new Helpers().CrearAcierto((int) EnumAciertos.ProductosRecomendadosObtenidos, "Se obtuvo todas las productos recomendados correctamente");
                }
                catch (AlgoliaException exAlgolia)
                {
                    resultado = false;
                    error = new Excepciones().ExcepcionAlgolia(exAlgolia, resultado);
                }
            }
            catch(Exception ex)
            {
                resultado = false;
                error = new Excepciones().ExcepcionLogica(ex, resultado);
            }

            return (resultado, acierto, error, responseAlgolia);
        }
    }
}