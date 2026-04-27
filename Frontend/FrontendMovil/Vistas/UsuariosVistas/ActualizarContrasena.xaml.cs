using FrontendMovil.Logicas;
using FrontendMovil.Entidades;
using FrontendMovil.ViewModels;
using FrontendMovil.Utilitarios;

namespace FrontendMovil.Vistas;

public partial class ActualizarContrasena : ContentPage
{
	public ActualizarContrasena()
	{
		InitializeComponent();
    }

    private async void BTN_ActualizarContrasena_Clicked(object sender, EventArgs e)
    {
        ResActualizarContrasena res = new ResActualizarContrasena();
        res.ListaAciertos = new List<Aciertos>();
        res.ListaErrores = new List<Errores>();

        try
        {
            ReqActualizarContrasena req = new ReqActualizarContrasena
            {
                Correo = TXT_Correo.Text,
                Contrasena = TXT_NuevaContrasena.Text
            };

            #region Validacion

            res.ListaErrores.AddRange(new Validaciones().Validar(req, "Correo", "Contrasena"));
            res.ListaErrores.AddRange(new Validaciones().ValidarCorreo(req, "Correo"));
            res.ListaErrores.AddRange(new Validaciones().ValidarContrasena(req, "Contrasena"));

            #endregion Validacion

            if (res.ListaErrores.Any())
            {
                await DisplayAlert("Error", string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}")), "OK");
            }
            else
            {
                res = await new LogUsuarios().ActualizarContrasena(req);

                if (res.Resultado)
                {
                    await DisplayAlert("Informacion", string.Join("\n", res.ListaAciertos.Select(e => $"{e.Message}")), "OK");

                    TXT_Correo.Text = String.Empty;
                    TXT_NuevaContrasena.Text = String.Empty;

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
            await DisplayAlert("Error", $"Error MUY Grave, intente mas tarde", "OK");
        }
    }
}