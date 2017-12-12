using ExamenXamarin.model;
using ExamenXamarin.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenXamarin.gerente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GerenteActivity : ContentPage
    {
        public GerenteActivity()
        {
            InitializeComponent();
            LoadDataAsync();
            LoadObjetivosAsync();
            LoadHorariosAsync();

            lblError.IsVisible = false;

            //botones
            btnAdd.Clicked += async (sender, args) =>
            {
                 await ConfirmData();
            };
            //botones
        }


        /// <summary>
        /// Método para leer los datos en la BBDD
        /// </summary>
        /// <remarks>
        /// Usando una colección observable, rellenamos los datos
        /// obtenidos de la lista de pedidos. Pasamos como ItemsSource
        /// la colección. Esto hará que se muestre en la aplicación
        /// </remarks>
        private async Task LoadDataAsync()
        {
            List<Usuario> userList = new List<Usuario>();
            ObservableCollection<User> uList = new ObservableCollection<User>();

            //obtenemos la lista de los usuarios
            userList = await App.DataRepo.GetAllUsersAsync();
            //vamos creando por cada elemento de la lista de pedidos un objeto order
            foreach (Usuario usuario in userList)
            {

                Horario horario = await App.DataRepo.GetHorarioById(usuario.HORARIO);
                Objetivo obj = await App.DataRepo.GetObjetivoById(usuario.OBJETIVO);
                string h = horario.HORARIO;
                string dni = usuario.DNI;
                string nombre = usuario.NOMBRE;
                int edad = usuario.EDAD;
                double altura = usuario.ALTURA;
                double peso = usuario.PESO;
                double imc = usuario.PESO / (Math.Sqrt((altura / 100)));
                string o = obj.OBJETIVO;

                uList.Add(new User
                {
                    Dni = dni,
                    Nombre = nombre,
                    DatoHorario = h,
                    Edad = edad,
                    Altura = altura,
                    Peso = peso,
                    Imc = imc,
                    Obj = o
                });
            }
            //Una vez agregamos todos los datos, pasamos los datos a la colección.
            lstUsers.ItemsSource = uList;
            lblError.Text = uList.Count.ToString();
        }

        /// <summary>
        /// Método que rellena el picker de objetivos
        /// </summary>
        private async Task LoadObjetivosAsync()
        {

            List<Objetivo> objList = new List<Objetivo>();
            ObservableCollection<Objetivo> list = new ObservableCollection<Objetivo>();

            objList = await App.DataRepo.GetAllObjetivosAsync();

            foreach (Objetivo obj in objList)
            {
                list.Add(obj);
            }
            pickerObjetivo.ItemsSource = list;
        }

        /// <summary>
        /// Método que rellena el picker de horarios
        /// </summary>
        private async Task LoadHorariosAsync()
        {
            List<Horario> horarioList = new List<Horario>();
            ObservableCollection<Horario> list = new ObservableCollection<Horario>();

            horarioList = await App.DataRepo.GetAllHorariosAsync();

            foreach (Horario horario in horarioList)
            {
                list.Add(horario);
            }
            pickerHorario.ItemsSource = list;
        }

        /// <summary>
        /// Método que recoge todos los datos y los guarda en la base de datos
        /// </summary>
        /// <remarks>Comprobamos que todos los datos estén rellenos o sean correctos para introducir los datos</remarks>
        private async Task ConfirmData()
        {
            lblError.IsVisible = false;
            if (String.IsNullOrEmpty(txtName.Text) || String.IsNullOrEmpty(txtAltura.Text) || String.IsNullOrEmpty(txtDNI.Text) ||
                String.IsNullOrEmpty(txtEdad.Text) || String.IsNullOrEmpty(txtPeso.Text))
            {
                lblError.Text = MessageUtils.ErrorCamposNoRellenos;
                lblError.IsVisible = true;
            }
            else
            {
                // si no, miramos el campo del DNI
                if(txtDNI.Text.Length != 9)
                {
                    lblError.Text = MessageUtils.ErrorCamposNoRellenos;
                    lblError.IsVisible = true;
                }
                else
                {
                    // intentamos agregar el usuario a la BBDD
                    // comprobamos que exista el usuario
                    string dni = txtDNI.Text;
                    Usuario user;

                    user = await App.DataRepo.GetUserByDNIAsync(dni);

                    // si no es igual a null, ya existe, por lo que mostramos un error
                    if(user != null)
                    {
                        lblError.Text = MessageUtils.ElUsuarioExiste;
                        lblError.IsVisible = true;
                    }
                    else
                    {
                        int altura = int.Parse(txtAltura.Text);
                        double peso = double.Parse(txtPeso.Text);

                        double imc = peso / (Math.Sqrt((altura / 100)));
                        Usuario usuario = new Usuario
                        {
                            DNI = dni,
                            NOMBRE = txtName.Text,
                            PASSWORD = dni,
                            HORARIO = ((Horario)pickerHorario.SelectedItem).ID,
                            EDAD = int.Parse(txtEdad.Text),
                            ALTURA = altura,
                            PESO = peso,
                            IMC = imc,
                            OBJETIVO = ((Objetivo) pickerObjetivo.SelectedItem).ID,
                            TIPO = "USUARIO"
                        };

                        // agregamos el usuario
                        await App.DataRepo.AddNewUser(usuario);
                    }
                }
            }
        }
    }
}