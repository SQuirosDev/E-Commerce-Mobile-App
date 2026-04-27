using FrontendMovil.Logicas;
using FrontendMovil.Entidades;
using FrontendMovil.Utilitarios;

namespace FrontendMovil.Vistas;

public partial class ProductosBusqueda : ContentPage
{
    public ProductosBusqueda()
	{
		InitializeComponent();
	}

    private async void BTN_BuscarProducto_Clicked(object sender, EventArgs e)
    {
        List<Productos> ListaProductos = new List<Productos>();

        ResListarProductosBuscados res = new ResListarProductosBuscados();
        res.ListaAciertos = new List<Aciertos>();
        res.ListaErrores = new List<Errores>();
        res.ListaProductosBuscados = new List<Productos>();

        try
        {
            ReqListarProductosBuscados req = new ReqListarProductosBuscados
            {
                Busqueda = TXT_Busqueda.Text,
                Estado = Sesiones.Estado
            };

            #region Validaciones

            res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
            res.ListaErrores.AddRange(new Validaciones().Validar(req, "Busqueda"));

            #endregion Validaciones

            if (res.ListaErrores.Any())
            {
                await DisplayAlert("Error", string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}")), "OK");
            }
            else
            {
                res = await new LogProductos().ListarBuscados(req);

                if (res.Resultado)
                {
                    await DisplayAlert("Informacion", string.Join("\n", res.ListaAciertos.Select(e => $"{e.Message}")), "OK");

                    TXT_Busqueda.Text = String.Empty;

                    ListaProductos.AddRange(res.ListaProductosBuscados);

                    // Asignamos la lista al CollectionView
                    CollectionViewProductos.ItemsSource = ListaProductos;
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

    private void BTN_VerProducto_Clicked(object sender, EventArgs e)
    {
        // 'sender' es el Button que fue pulsado
        var button = (Button)sender;

        // Obtenemos el Producto que está enlazado a esta fila
        var producto = (Productos)button.BindingContext;

        // De él sacamos el Id
        int id = producto.IdProducto;

        // Y lo pasamos al constructor de la siguiente página
        Navigation.PushAsync(new ObtenerProducto(id));
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