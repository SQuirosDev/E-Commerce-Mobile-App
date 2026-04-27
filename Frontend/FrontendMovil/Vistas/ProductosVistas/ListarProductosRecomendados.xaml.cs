using FrontendMovil.Entidades;
using FrontendMovil.Logicas;
using FrontendMovil.Utilitarios;

namespace FrontendMovil.Vistas;

public partial class ListarProductosRecomendados : ContentPage
{
    public List<Productos> ListaProductosRecomendados = new List<Productos>();

    public ListarProductosRecomendados()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Llamamos al método cuando la vista aparece
        ListaProductosRecomendados = new Helpers().ListaLimitada2(await this.ProductosRecomendados());

        // Asignamos la lista al CollectionView
        CollectionViewProductosRecomendados.ItemsSource = ListaProductosRecomendados;
    }

    private async Task<List<Productos>> ProductosRecomendados()
    {
        ResListarProductosRecomendados res = new ResListarProductosRecomendados();
        res.ListaAciertos = new List<Aciertos>();
        res.ListaErrores = new List<Errores>();
        res.ListaProductosRecomendados = new List<Productos>();

        try
        {
            ReqListarProductosRecomendados req = new ReqListarProductosRecomendados
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
                TXT_MensajeRecomendado.Text = string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}"));
            }
            else
            {
                res = await new LogProductos().ListarRecomendados(req);

                if (res.Resultado)
                {
                    TXT_MensajeRecomendado.Text = string.Join("\n", res.ListaAciertos.Select(e => e.Message));
                }
                else
                {
                    TXT_MensajeRecomendado.Text = string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}"));
                }
            }
        }
        catch (Exception ex)
        {
            TXT_MensajeRecomendado.Text = "Error MUY Grave, intente mas tarde";
        }

        return res.ListaProductosRecomendados;
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