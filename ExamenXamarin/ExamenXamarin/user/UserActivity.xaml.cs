using ExamenXamarin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenXamarin.user
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserActivity : ContentPage
    {
        private Usuario usuario;

        private UserActivity()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor que tendrá como parámetro el usuario que viene de la otra
        /// actividad
        /// </summary>
        /// <remarks>
        /// El constructor está sobrecargado. el otro está puesto en privado para que no sea
        /// accesible y se llame a este cada vez que queramos iniciar la actividad.
        /// </remarks>
        /// <param name="user">Usuario que llega de la otra actividad</param>
        public UserActivity(Usuario user)
        {
            lblBienvenido.Text = "Bienvenido/a " + user.NOMBRE;
            InitializeComponent();
            this.usuario = user;

            FillDataAsync();

            btnBack.Clicked += async (sender, args) =>
            {
                await OpenMainPageAsync();
            };
        }

        /// <summary>
        /// Método que rellena todos los datos de los distintos label del usuario
        /// </summary>
        /// <remarks>Hace llamadas a la base de datos.</remarks>
        /// <returns></returns>
        private async Task FillDataAsync()
        {
            double imc = usuario.PESO / Math.Sqrt((usuario.ALTURA / 100));
            Horario horario = await App.DataRepo.GetHorarioById(usuario.HORARIO);
            Objetivo objetivo = await App.DataRepo.GetObjetivoById(usuario.OBJETIVO);
            lblDNI.Text = usuario.DNI;
            lblEdad.Text = usuario.EDAD.ToString();
            lblAltura.Text = usuario.ALTURA.ToString();
            lblPeso.Text = usuario.PESO.ToString();
            lblImc.Text = imc.ToString();
            lblHorario.Text = horario.HORARIO;
            lblObjetivo.Text = objetivo.OBJETIVO;
        }

        /// <summary>
        /// Método que abrirá la ventana de mainPage
        /// </summary>
        private async Task OpenMainPageAsync()
        {
            await Navigation.PushModalAsync(new MainPage());
        }

    }
}