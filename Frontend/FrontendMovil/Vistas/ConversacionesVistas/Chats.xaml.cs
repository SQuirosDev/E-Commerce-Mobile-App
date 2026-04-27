namespace FrontendMovil.Vistas;

public partial class Chats : ContentPage
{
	public Chats()
	{
		InitializeComponent();
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

    private async void BTN_Chat_Detail_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChatDetail());
    }
}