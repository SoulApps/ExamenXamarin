using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using ExamenXamarin.model;
using ExamenXamarin.utils;
using ExamenXamarin.gerente;
using ExamenXamarin.user;

namespace ExamenXamarin
{
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// El constructor inicializa los componentes y gestiona los eventos
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            btnLogin.Clicked += (sender, args) =>
            {
                CheckUserAsync();
            };
            // al iniciar, el mensaje de error no se muestra.
            lblError.IsVisible = false;
        }

        /// <summary>
        /// Comprueba que el usuario exista en la BBDD y sea correcto
        /// </summary>
        /// <remarks>
        /// Recogemos los datos introducidos en los campos de texto. Una vez
        /// recogidos los datos, comprobamos que en la BBDD haya un usuario con ese
        /// nombre. En ese caso, comprobamos si la contraseña es igual.
        /// Si los datos son correctos, abrimos la actividad del gerente o el usuario
        /// en función del tipo de usuario.
        /// </remarks>
        private async void CheckUserAsync()
        {
            lblError.IsVisible = false;
            // Obtenemos los datos
            string name = "";
            string passwd = "";
            Usuario user = null;


            // validación
            if (txtUserName.Text == null && txtPasswd.Text == null)
            {
                lblError.Text = MessageUtils.ErrorLoginNoValido;
                lblError.IsVisible = true;
            }
            else
            {
                name = txtUserName.Text.ToString();
                passwd = txtPasswd.Text.ToString();
                if (txtUserName.Text.Length != 9 && txtPasswd.Text.Length == 9
                || txtUserName.Text == null && txtPasswd.Text.Length == 9)
                {
                    lblError.Text = MessageUtils.ErrorInserteUser;
                    lblError.IsVisible = true;
                }
                else if (txtPasswd.Text.Length != 9 && txtUserName.Text.Length == 9
                    || txtPasswd.Text == null && txtUserName.Text.Length == 9)
                {
                    lblError.Text = MessageUtils.ErrorInserteContra;
                    lblError.IsVisible = true;
                }
                else if (txtUserName.Text.Length == 9 && txtPasswd.Text.Length == 9)
                {
                    // intentamos cargar los datos
                    user = await App.DataRepo.GetUserByDNIAsync(name);
                    if (user != null)
                    {
                        if (user.PASSWORD.Equals(passwd))
                        {
                            // Si la password es correcta
                            // miramos el tipo
                            if (user.TIPO.Equals("USUARIO"))
                            {
                                await ShowClientActivityAsync("USUARIO", user);
                            }
                            else
                            {
                                await ShowClientActivityAsync("GERENTE", user);
                            }
                        }
                        else
                        {
                            lblError.Text = MessageUtils.ErrorContraNoValida;
                            lblError.IsVisible = true;
                        }
                    }
                    else
                    {
                        //el usuario no está dado de alta
                        lblError.Text = MessageUtils.ErrorUserDadoAlta;
                        lblError.IsVisible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Método para llamar a las distintas actividades
        /// </summary>
        /// <param name="userType">Tipo de usuario para llamar a la actividad</param>
        /// <param name="user">Objeto usuario que ha llamado a la actividad</param>
        private async Task ShowClientActivityAsync(string userType, Usuario user)
        {
            switch (userType)
            {
                case "USUARIO":
                    await Navigation.PushModalAsync(new UserActivity(user));
                    break;
                case "GERENTE":
                    await Navigation.PushModalAsync(new GerenteActivity());
                    break;
            }
        }
    }
}
