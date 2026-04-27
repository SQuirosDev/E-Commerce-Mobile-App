using FrontendMovil.Logicas;
using FrontendMovil.Entidades;
using FrontendMovil.Utilitarios;

namespace FrontendMovil.Vistas;

public partial class Registrarse : ContentPage
{
	public Registrarse()
	{
		InitializeComponent();
	}

    private async void BTN_Registrarse_Clicked(object sender, EventArgs e)
    {
        ResRegistrar res = new ResRegistrar();
        res.ListaAciertos = new List<Aciertos>();
        res.ListaErrores = new List<Errores>();

        try
        {
            ReqRegistrar req = new ReqRegistrar
            {
                Usuario = new Usuarios
                {
                    Nombre = TXT_NombreCompleto.Text,
                    Telefono = TXT_Telefono.Text,
                    Correo = TXT_Correo.Text,
                    Contrasena = TXT_Contrasena.Text,
                    Longitud = Convert.ToDouble(TXT_Longitud.Text),
                    Latitud = Convert.ToDouble(TXT_Latitud.Text),
                }
            };

            #region Validacion

            res.ListaErrores.AddRange(new Validaciones().Validar(req.Usuario, "Nombre", "Correo", "Telefono", "Contrasena", "Longitud", "Latitud"));
            res.ListaErrores.AddRange(new Validaciones().ValidarCorreo(req.Usuario, "Correo"));
            res.ListaErrores.AddRange(new Validaciones().ValidarContrasena(req.Usuario, "Contrasena"));

            #endregion Validacion

            if (res.ListaErrores.Any())
            {
                await DisplayAlert("Error", string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}")), "OK");
            }
            else
            {
                res = await new LogUsuarios().Registrarse(req);

                if (res.Resultado)
                {
                    await DisplayAlert("Informacion", string.Join("\n", res.ListaAciertos.Select(e => $"{e.Message}")), "OK");

                    TXT_NombreCompleto.Text = String.Empty;
                    TXT_Telefono.Text = String.Empty;
                    TXT_Correo.Text = String.Empty;
                    TXT_Contrasena.Text = String.Empty;
                    TXT_Longitud.Text = String.Empty;
                    TXT_Latitud.Text = String.Empty;

                    await Navigation.PushAsync(new CodigoVerificacion(req.Usuario.Correo));
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