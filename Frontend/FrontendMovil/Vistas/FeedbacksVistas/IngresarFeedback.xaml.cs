using FrontendMovil.Entidades;
using FrontendMovil.Logicas;
using FrontendMovil.Utilitarios;
using FrontendMovil.ViewModels;

namespace FrontendMovil.Vistas.Feedbacks;

public partial class IngresarFeedback : ContentPage
{
	public IngresarFeedback(int idProducto)
	{
		InitializeComponent();
        BindingContext = new IngresarFeedbackViewModel(idProducto);
    }

    private async void BTN_AgregarFeedback_Clicked(object sender, EventArgs e)
    {
        ResIngresarFeedback res = new ResIngresarFeedback();
        res.ListaAciertos = new List<Aciertos>();
        res.ListaErrores = new List<Errores>();

        try
        {
            // Obtiene el ViewModel actual desde el BindingContext y lo convierte al tipo específico para acceder a sus propiedades.
            var viewModel = BindingContext as IngresarFeedbackViewModel;

            ReqIngresarFeedback req = new ReqIngresarFeedback 
            { 
                Feedback = new FeedBacks 
                { 
                    Calificacion = Convert.ToDecimal(TXT_Calificacion.Text), 
                    Comentario = TXT_Comentario.Text 
                }, 
                Id = new Ids 
                { 
                    IdUsuario = Sesiones.Usuario.IdUsuario, 
                    IdProducto = viewModel.IdProducto,
                }, 
                Estado = Sesiones.Estado
            };

            #region Validacion

            res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdProducto", "IdUsuario"));
            res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
            res.ListaErrores.AddRange(new Validaciones().Validar(req.Feedback, "Calificacion", "Comentario"));

            #endregion Validacion

            if (res.ListaErrores.Any())
            {
                await DisplayAlert("Error", string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}")), "OK");
            }
            else
            {
                res = await new LogFeedBacks().Ingresar(req);

                if (res.Resultado)
                {
                    await DisplayAlert("Informacion", string.Join("\n", res.ListaAciertos.Select(e => $"{e.Message}")), "OK");

                    TXT_Comentario.Text = String.Empty;
                    TXT_Calificacion.Text = String.Empty;

                    await Navigation.PushAsync(new ObtenerProducto(viewModel.IdProducto));
                }
                else
                {
                    await DisplayAlert("Error", string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}")), "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error Frontend: \n{ex} \n{ex.Message}", "OK");
        }
    }

    // Menu inferior

    private void BTN_Main_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Main());
    }

    private void BTN_Buscar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProductosBusqueda());
    }

    private void BTN_IngresarProducto_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new IngresarProducto());
    }

    private void BTN_Chats_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Chats());
    }

    private void BTN_Cuenta_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ObtenerUsuario());
    }
}