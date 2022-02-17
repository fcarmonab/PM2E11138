using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E11138.Models;
using PM2E11138.Views;
using System.IO;

namespace PM2E11138
{
    public partial class App : Application
    {

        static SitiosDB basedatos;

        public static SitiosDB BaseDatos
        {
            get 
            {
                if (basedatos==null)
                {
                    basedatos = new SitiosDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SitiosDB.db3"));
                }
                return basedatos;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new CrearSitioPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
