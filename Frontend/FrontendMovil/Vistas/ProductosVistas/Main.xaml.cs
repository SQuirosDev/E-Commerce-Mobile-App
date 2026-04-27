using FrontendMovil.Entidades;
using FrontendMovil.Logicas;
using FrontendMovil.Utilitarios;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Maps;


namespace FrontendMovil.Vistas;

public partial class Main : ContentPage
{
    public List<Productos> ListaProductos = new List<Productos>();
    public List<Productos> ListaProductosRecomendados = new List<Productos>();

    public Main()
	{
        DisplayAlert("Bienvenida", $"Bienvenido a ShareX, {Sesiones.Usuario.Nombre}", "OK");
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Llamamos al método cuando la vista aparece
        ListaProductos = new Helpers().ListaLimitada(await this.ListarProductos());
        ListaProductosRecomendados = new Helpers().ListaLimitada(await this.ListarProductosRecomendados());

        // Asignamos la lista al CollectionView
        CollectionViewProductos.ItemsSource = ListaProductos;
        CollectionViewProductosRecomendados.ItemsSource = ListaProductosRecomendados;
    }

    private async Task<List<Productos>> ListarProductos()
    {
        ResListarProductos res = new ResListarProductos();
        res.ListaAciertos = new List<Aciertos>();
        res.ListaErrores = new List<Errores>();
        res.ListaProductos = new List<Productos>();

        try
        {
            ReqListarProductos req = new ReqListarProductos
            {
                Estado = 1,
            };

            #region Validacion

            res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));

            #endregion Validacion

            if (res.ListaErrores.Any())
            {
                TXT_Mensaje.Text = string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}"));
            }
            else
            {
                res = await new LogProductos().Listar(req);

                if (res.Resultado)
                {
                    TXT_Mensaje.Text = string.Join("\n", res.ListaAciertos.Select(e => e.Message));
                }
                else
                {
                    TXT_Mensaje.Text = string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}"));
                }
            }
        }
        catch (Exception ex)
        {
            TXT_Mensaje.Text = "Error MUY Grave, intente mas tarde";
        }

        return res.ListaProductos;
    }

    private async Task<List<Productos>> ListarProductosRecomendados()
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

    private void BTN_Productos_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ListarProductos());
    }

    private void BTN_ProductosRecomendados_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ListarProductosRecomendados());
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

    // Falta esto
    private void BTN_MapaVendedores_Clicked(object sender, EventArgs e)
    {
        TXT_Distancia.Text = String.Empty;
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