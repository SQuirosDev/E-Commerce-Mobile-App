using FrontendMovil.Logicas;
using FrontendMovil.Entidades;
using FrontendMovil.Utilitarios;

namespace FrontendMovil.Vistas;

public partial class ObtenerUsuario : ContentPage
{
	public ObtenerUsuario()
	{
		InitializeComponent();
        Usuario();
    }

    public async void Usuario()
    {
        ResObtenerUsuarios res = new ResObtenerUsuarios();
        res.ListaAciertos = new List<Aciertos>();
        res.ListaErrores = new List<Errores>();
        res.Usuario = new Usuarios();

        try
        {
            ReqObtenerUsuario req = new ReqObtenerUsuario
            {
                Id = new Ids
                {
                    IdUsuario = Sesiones.Usuario.IdUsuario
                },
                Estado = Sesiones.Estado
            };

            #region Validacion

            res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
            res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdUsuario"));

            #endregion Validacion

            if (res.ListaErrores.Any())
            {
                await DisplayAlert("Error", string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}")), "OK");
            }
            else
            {
                res = await new LogUsuarios().Obener(req);

                if (res.Resultado)
                {
                    await DisplayAlert("Informacion", string.Join("\n", res.ListaAciertos.Select(e => $"{e.Message}")), "OK");

                    TXT_Nombre.Text = $"<b>Nombre:</b> {res.Usuario.Nombre}";
                    TXT_Correo.Text = $"<b>Correo:</b> {res.Usuario.Correo}";
                    TXT_Telefono.Text = $"<b>Telefono:</b> {res.Usuario.Telefono}";
                    TXT_Ubicacion.Text = $"<b>Ubicacion:</b>\n<b>Longitud:</b> {res.Usuario.Longitud}\n<b>Latitud:</b> {res.Usuario.Latitud}";
                    TXT_Rol.Text = $"<b>Rol:</b> {res.Usuario.Rol}";
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

    private async void BTN_Vendedor_Clicked(object sender, EventArgs e)
    {
        ResCambiarRol res = new ResCambiarRol();
        res.ListaAciertos = new List<Aciertos>();
        res.ListaErrores = new List<Errores>();

        try
        {
            ReqCambiarRol req = new ReqCambiarRol
            {
                Id = new Ids
                {
                    IdUsuario = Sesiones.Usuario.IdUsuario
                },
                Estado = Sesiones.Estado
            };

            #region Validacion

            res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
            res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdUsuario"));

            #endregion Validacion

            if (res.ListaErrores.Any())
            {
                await DisplayAlert("Error", string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}")), "OK");
            }
            else
            {
                res = await new LogUsuarios().CambiarRol(req);

                if (res.Resultado)
                {
                    await DisplayAlert("Informacion", string.Join("\n", res.ListaAciertos.Select(e => $"{e.Message}")), "OK");

                    this.Usuario();
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

    private void BTN_Logout_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Informacion", "Se cerro la sesion correctamente", "OK");

        new Helpers().CrearSesion(0, "", false, "", "", "", 0, 0, 0, DateTime.Now);

        Navigation.PushAsync(new Login());
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