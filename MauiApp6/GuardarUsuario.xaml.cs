namespace MauiApp6;
public partial class GuardarUsuario : ContentPage
{
    public GuardarUsuario()
    {
        InitializeComponent();
    }
    public async Task LlamadaGetAsync(string url)
    {
        //Creamos una instancia de HttpClient
        var client = new HttpClient();
        //Asignamos la URL
        client.BaseAddress = new Uri(url);
        //Llamada asíncrona al sitio
        var response = await client.GetAsync(client.BaseAddress);
        //Nos aseguramos de recibir una respuesta satisfactoria
        response.EnsureSuccessStatusCode();
        //Convertimos la respuesta a una variable string
        var cadenaResultante = response.Content.ReadAsStringAsync().Result;
        await DisplayAlert("Se creo el usuario ", "Se creo" + cadenaResultante, "OK");
        resultado.Text = cadenaResultante;
        nombre.Text = "";
        contrasena.Text = "";
        usuario.Text = "";
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        _ = LlamadaGetAsync("https://tilinazo.000webhostapp.com/agregar.php?nombre=" +
        nombre.Text + "&contrasena=" + contrasena.Text + "&usuario=" + usuario.Text);
    }
    private async void Button_Clicked2(object sender, EventArgs e)
    {
        var Something = new MainPage();
        await Navigation.PushAsync(Something);
    }
}