using FrontendMovil.Logicas;
using FrontendMovil.Entidades;
using FrontendMovil.ViewModels;
using FrontendMovil.Utilitarios;

namespace FrontendMovil.Vistas;

public partial class CodigoVerificacion : ContentPage
{
	public CodigoVerificacion(string correo)
	{
		InitializeComponent();
        BindingContext = new CodigoVerificacionViewModel(correo);
    }

    private async void BTN_Verificar_Clicked(object sender, EventArgs e)
    {
        ResCogidoVerificacion res = new ResCogidoVerificacion();
        res.ListaAciertos = new List<Aciertos>();
        res.ListaErrores = new List<Errores>();

        try
        {
            // Obtiene el ViewModel actual desde el BindingContext y lo convierte al tipo específico para acceder a sus propiedades.
            var viewModel = BindingContext as CodigoVerificacionViewModel;

            ReqCodigoVerificacion req = new ReqCodigoVerificacion
            {
                Correo = viewModel.Correo,
                CodigoVerificacion = TXT_CodigoVerificacion.Text
            };

            #region Validacion

            res.ListaErrores.AddRange(new Validaciones().Validar(req, "Correo", "CodigoVerificacion"));

            #endregion Validacion

            if (res.ListaErrores.Any())
            {
                await DisplayAlert("Error", string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}")), "OK");
            }
            else
            {
                res = await new LogUsuarios().CodigoVerificacion(req);

                if (res.Resultado)
                {
                    await DisplayAlert("Informacion", string.Join("\n", res.ListaAciertos.Select(e => $"{e.Message}")), "OK");

                    TXT_CodigoVerificacion.Text = String.Empty;

                    await Navigation.PushAsync(new Login());
                }
                else
                {
                    await DisplayAlert("Error", string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}")), "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Error MUY Grave, intente mas tarde", "OK");
        }
    }
}