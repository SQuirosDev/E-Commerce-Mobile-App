using FrontendMovil.Entidades;
using FrontendMovil.Logicas;
using FrontendMovil.Utilitarios;

namespace FrontendMovil.Vistas;

public partial class IngresarProducto : ContentPage
{
    FileResult imagenSeleccionada;

    public IngresarProducto()
	{
        if (Sesiones.Usuario.Rol == "VENDEDOR")
        {
            InitializeComponent();
        }
        else
        {
            DisplayAlert("Error", "Para agregar un producto tiene que ser un vendedor", "OK");

            Navigation.PushAsync(new Main());
        }
    }

    private async void BTN_SeleccionarImagen_Clicked(object sender, EventArgs e)
    {
        try
        {
            imagenSeleccionada = await MediaPicker.PickPhotoAsync(
                new MediaPickerOptions
                {
                    Title = "Selecciona una imagen"
                }
            );

            if (imagenSeleccionada != null)
            {
                // Mostrar en el control Image
                var stream = await imagenSeleccionada.OpenReadAsync();
                ImagenPreview.Source = ImageSource.FromStream(() => stream);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo seleccionar imagen: {ex.Message}", "OK");
        }
    }

    private async void BTN_GuardarProducto_Clicked(object sender, EventArgs e)
    {
        ResIngresarProducto res = new ResIngresarProducto();
        res.ListaAciertos = new List<Aciertos>();
        res.ListaErrores = new List<Errores>();

        try
        {
            ReqIngresarProducto req = new ReqIngresarProducto 
            { 
                Producto = new Productos 
                { 
                    Nombre = TXT_Nombre.Text, 
                    Descripcion = TXT_Descripcion.Text, 
                    Precio = Convert.ToDecimal(TXT_Precio.Text), 
                    PrecioAlquiler = Convert.ToDecimal(TXT_PrecioAlquiler.Text), 
                    Stock = Convert.ToInt32(TXT_Stock.Text), 
                    Categoria = CBO_Categoria.SelectedItem.ToString(), 
                    TipoOperacion = 'B', 
                    Imagen = await new Helpers().ConvertirABytesAsync(imagenSeleccionada) 
                }, 
                Id = new Ids 
                { 
                    IdUsuario = Sesiones.Usuario.IdUsuario
                }, 
                Estado = Sesiones.Estado
            };

            #region Validacion

            res.ListaErrores.AddRange(new Validaciones().Validar(req, "Estado"));
            res.ListaErrores.AddRange(new Validaciones().Validar(req.Id, "IdUsuario"));
            res.ListaErrores.AddRange(new Validaciones().Validar(req.Producto, "Nombre", "Descripcion", "Precio", "PrecioAlquiler", "TipoOperacion", "Stock", "Categoria", "Imagen"));

            #endregion Validacion

            if (res.ListaErrores.Any())
            {
                await DisplayAlert("Error", string.Join("\n", res.ListaErrores.Select(e => $"{e.Message}")), "OK");
            }
            else
            {
                res = await new LogProductos().Ingresar(req);

                if (res.Resultado)
                {
                    await DisplayAlert("Informacion", string.Join("\n", res.ListaAciertos.Select(e => $"{e.Message}")), "OK");

                    TXT_Nombre.Text = String.Empty;
                    TXT_Descripcion.Text = String.Empty;
                    TXT_Precio.Text = String.Empty;
                    TXT_PrecioAlquiler.Text = String.Empty;
                    TXT_Stock.Text = String.Empty;
                    CBO_Categoria.SelectedIndex = -1;
                    ImagenPreview.Source = String.Empty;

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