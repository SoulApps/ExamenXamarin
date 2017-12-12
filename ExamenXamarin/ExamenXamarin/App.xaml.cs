using ExamenXamarin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ExamenXamarin
{
    public partial class App : Application
    {
        /// <summary>
        /// Propiedad dataRepo
        /// </summary>
        public static DBRepository DataRepo { get; set; }

        /// <summary>
        /// El método App llama a MainPage y crea un objeto DBRepository
        /// </summary>
        /// <param name="filename">Ruta de la BBDD</param>
        public App(string filename)
        {
            InitializeComponent();
            DataRepo = new DBRepository(filename);
            MainPage = new MainPage();

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
