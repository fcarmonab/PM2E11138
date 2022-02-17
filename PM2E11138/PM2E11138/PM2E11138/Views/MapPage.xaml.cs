using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using PM2E11138.Views;
using PM2E11138.Models;

namespace PM2E11138.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

                       
            Pin ubicacion = new Pin();
            ubicacion.Label = "";
            ubicacion.Address = "Cerca de UTH";
            ubicacion.Position = new Position(15.5510539, -88.0109923); // Recibir datos de sitio seleccionado
            mapa.Pins.Add(ubicacion);
            mapa.MoveToRegion(new MapSpan(new Position(15.5510539, -88.0109923),1,1));


        }

        private void CompartinImagen_Clicked(object sender, EventArgs e)
        {

        }
    }
}