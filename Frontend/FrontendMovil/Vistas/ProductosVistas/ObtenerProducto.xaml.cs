using FrontendMovil.Logicas;
using FrontendMovil.Entidades;
using FrontendMovil.Utilitarios;
using FrontendMovil.ViewModels;
using FrontendMovil.Vistas.Feedbacks;

namespace FrontendMovil.Vistas;

public partial class ObtenerProducto : ContentPage
{
    public List<FeedBacks> ListaFeedbacks = new List<FeedBacks>();
    int IdProducto = 0;

    public ObtenerProducto(int idProducto)
	{
		InitializeComponent();
        BindingContext = new ObtenerProductoViewModel(idProducto);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Llamamos al método cuando la vista aparece
        ListaFeedbacks = await this.ListarFeedbacks();

        // Asignamos la lista al CollectionView
        CollectionViewFeedbacks.ItemsSource = ListaFeedbacks;
    }

    private async Task<List<FeedBacks>> ListarFeedbacks()
    {
        ResObtenerProducto res = new ResObtenerProducto();
        res.ListaAciertos = new List<Aciertos>();
        res.ListaErrores = new List<Errores>();
        res.Producto = new Productos();
        res.ListaFeedbacks = new List<FeedBacks>();

        try
        {
            // Obtiene el ViewModel actual desde el BindingContext y lo convierte al tipo específico para acceder a sus propiedades.
            var viewModel = BindingContext as ObtenerProductoViewModel;

            ReqObtenerProducto req = new ReqObtenerProducto
            {
                Id = new Ids
                {
                    IdProducto = viewModel.IdProducto,
                    IdUsuario = Sesiones.Usuario.IdUsuario
                },
                Estado = Sesiones.Estado
            };

            #region Validacion

            res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdProducto"));
            res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));

            #endregion Validacion

            if (res.ListaErrores.Any())
            {
                await DisplayAlert("Error", string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}")), "OK");
            }
            else
            {
                res = await new LogProductos().Obtener(req);

                if (res.Resultado)
                {
                    await DisplayAlert("Informacion", string.Join("\n", res.ListaAciertos.Select(e => $"{e.Message}")), "OK");

                    ImagenProducto.Source = ImageSource.FromStream(() => new MemoryStream(res.Producto.Imagen));
                    TXT_Nombre.Text = $"<b>Producto:</b> {res.Producto.Nombre}";
                    TXT_Descripcion.Text = $"<b>Descripcion:</b>\n{res.Producto.Descripcion}";
                    TXT_Precio.Text = $"<b>Precio:</b> {res.Producto.Precio.ToString()}";
                    TXT_PrecioAlquiler.Text = $"<b>Precio Alquiler:</b> {res.Producto.PrecioAlquiler.ToString()}";
                    TXT_Stock.Text = $"<b>Stock:</b> {res.Producto.Stock.ToString()}";
                    TXT_CalificacionPromedio.Text = $"<b>Calificacion:</b> {res.Producto.CalificacionPromedio.ToString()}";
                    TXT_Categoria.Text = $"<b>Categoria:</b> {res.Producto.Categoria}";
                    IdProducto = res.Producto.IdProducto;
                }
                else
                {
                    await DisplayAlert("Error", string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}")), "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error Frontend: \n{ex} \n{ex.Message}", "OK");
        }

        return res.ListaFeedbacks;
    }

    private void BTN_AgregarComentario_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new IngresarFeedback(IdProducto));
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