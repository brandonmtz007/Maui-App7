using Newtonsoft.Json;
namespace MauiApp6
{
    public partial class MainPage : ContentPage
    {
        class Usuarios
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public string usuario { get; set; }
            public string contrasena { get; set; }
        }
        List<Usuarios> registros;
        HttpClient client;
        public MainPage()
        {
            InitializeComponent();
            client = new HttpClient(); client.MaxResponseContentBufferSize = 256000;
            try
            {
                var Usuario = Preferences.Default.Get("usuario", "");
                if (Usuario != "")
                {
                    var Contrasena = Preferences.Default.Get("contrasena", "");
                    usuario.Text = Usuario;
                    contrasena.Text = Contrasena;
                    recordar.IsToggled = true;
                }
            }
            catch
            {
                usuario.Text = "";
                contrasena.Text = "";
            }
        }
        public async Task HttpSample()
        {
            string direccion = "https://tilinazo.000webhostapp.com/api/login.php?usuario=" +
            usuario.Text + "&contrasena=" + contrasena.Text;
            var uri = new Uri(string.Format(direccion, string.Empty));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content.Length <= 0)
                {
                    await DisplayAlert("Autenticación ", "Lo sentimos no estas registrado", "OK");
                }
                else
                {
                    registros = JsonConvert.DeserializeObject<List<Usuarios>>(content);
                    string s = "";
                    Usuarios usuario = new Usuarios();
                    registros.ForEach(delegate (Usuarios x)
                    {
                        Console.WriteLine(x.nombre); s = s + x.nombre + "\n"; usuario = x;
                    });
                    string mensaje = registros.Count + " Registro \n" + s;
                    await DisplayAlert("Datos ", usuario.nombre, "OK");
                    var inicioPage = new Principal();
                    inicioPage.BindingContext = usuario;
                    if (recordar.IsToggled)
                    {
                        Preferences.Default.Set("usuario", usuario.usuario);
                        Preferences.Default.Set("contrasena", usuario.contrasena);
                    }
                    else
                    {
                        Preferences.Default.Set("usuario", "");
                        Preferences.Default.Set("contrasena", "");
                    }
                    await Navigation.PushAsync(inicioPage);
                }
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await HttpSample();
        }
        private async void Button_Clicked2(object sender, EventArgs e)
        {
            var Something = new GuardarUsuario();
            await Navigation.PushAsync(Something);
        }
    }

    internal class GuardarUsuario : Page
    {
    }
}