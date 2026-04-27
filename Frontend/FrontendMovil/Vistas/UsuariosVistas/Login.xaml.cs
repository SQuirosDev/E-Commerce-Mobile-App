using FrontendMovil.Entidades;
using FrontendMovil.Logicas;
using FrontendMovil.Utilitarios;
using FrontendMovil.Vistas;
using FrontendMovil.Vistas.Feedbacks;

namespace FrontendMovil
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void BTN_Login_Clicked(object sender, EventArgs e)
        {
            ResLogin res = new ResLogin();
            res.ListaAciertos = new List<Aciertos>();
            res.ListaErrores = new List<Errores>();
            res.Usuario = new Usuarios();

            try
            {
                ReqLogin req = new ReqLogin
                {
                    Correo = TXT_Correo.Text,
                    Contrasena = TXT_Contrasena.Text
                    //Correo = "yauveuyujivi-9560@yopmail.com",
                    //Contrasena = "GatoLoco#123"
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
                    res = await new LogUsuarios().Login(req);

                    if (res.Resultado)
                    {
                        await DisplayAlert("Informacion", string.Join("\n", res.ListaAciertos.Select(e => $"{e.Message}")), "OK");

                        TXT_Correo.Text = String.Empty;
                        TXT_Contrasena.Text = String.Empty;

                        await Navigation.PushAsync(new Main());
                    }
                    else
                    {
                        await DisplayAlert("Error", string.Join("\n", res.ListaErrores.Select(e => $"{e.ErrorCode}: {e.Message}")), "OK");

                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Error MUY Grave, intente mas tarde", "OK");
            }
        }

        private void BTN_Registrarse_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registrarse()); //QUITAR LOS // CUANDO YA SIRVA
        }

        private void BTN_ActualizarContrasena_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ActualizarContrasena());
        }
    }
}