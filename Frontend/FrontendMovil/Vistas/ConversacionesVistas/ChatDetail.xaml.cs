using FrontendMovil.Entidades;
using Microsoft.AspNetCore.SignalR.Client;

namespace FrontendMovil.Vistas
{
    public partial class ChatDetail : ContentPage
    {
        private HubConnection hubConnection;

        public ChatDetail()
        {
            InitializeComponent();

            // Fijamos Usuario 1 por defecto
            txtUsername.Text = Sesiones.Usuario.Nombre;

            // Construimos el HubConnection (usa tu IP o dominio accesible)
            var baseUrl = "https://localhost:44333/chatHub";
            hubConnection = new HubConnectionBuilder()
                .WithUrl(baseUrl)
                .WithAutomaticReconnect()  // reintenta si se cae
                .Build();

            // Callback para mensajes entrantes
            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                // esto siempre en el hilo UI
                Dispatcher.Dispatch(() =>
                {
                    lblChat.Text += $"[{DateTime.Now:HH:mm}] {user}: {message}{Environment.NewLine}";
                });
            });

            // Inicialmente no permitimos enviar hasta conectar
            BTN_SEND.IsEnabled = false;
        }

        // Se llama cuando la página ya está en pantalla
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                await hubConnection.StartAsync();
                BTN_SEND.IsEnabled = true;   // habilitamos el botón
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo conectar al chat: {ex.Message}", "OK");
            }
        }

        private async void BTN_SEND_Clicked(object sender, EventArgs e)
        {
            // Verificamos que esté conectado
            if (hubConnection.State != HubConnectionState.Connected)
            {
                await DisplayAlert("Aviso", "Aún no estás conectado al servidor.", "OK");
                return;
            }

            var user = txtUsername.Text?.Trim();
            var msg = txtMessage.Text?.Trim();
            if (string.IsNullOrWhiteSpace(msg))
                return;

            try
            {
                // Usamos InvokeAsync en vez de InvokeCoreAsync
                await hubConnection.InvokeAsync("SendMessageToAll", user, msg);
                txtMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo enviar el mensaje: {ex.Message}", "OK");
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
}
