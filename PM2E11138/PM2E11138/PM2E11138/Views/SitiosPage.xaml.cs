using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E11138.Models;
using PM2E11138.Views;
using Xamarin.Essentials;


namespace PM2E11138.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SitiosPage : ContentPage
    {
        public SitiosPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
           ListaSitios.ItemsSource = await App.BaseDatos.ListaSitios();

            var listasitios = await App.BaseDatos.ListaSitios();
            ObservableCollection<Sitios> observableCollectionFotos = new ObservableCollection<Sitios>();
            ListaSitios.ItemsSource = observableCollectionFotos;
            foreach (Sitios imagen in listasitios)
            {
                observableCollectionFotos.Add(imagen);
            }

        }

        private async void ListaSitios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Sitios item = (Sitios)e.Item;

            var accion = await DisplayAlert("Accion", "¿Que desea hacer?", "Ver Sitio en el Mapa","Borrar Sitio");

            if (accion)
            {
                

                var rDescripcion = await App.BaseDatos.ObtenerDescripcion(item.Descripcion);
                var rLatitud = await App.BaseDatos.ObtenerLatitud(item.Latitud);
                var rLongitud = await App.BaseDatos.ObtenerLongitud(item.Longitud);


                await Xamarin.Essentials.Map.OpenAsync(rLatitud.Latitud, rLongitud.Longitud, new MapLaunchOptions
                {
                    Name = rDescripcion.Descripcion,

                });


            }
            else 
            {

                var resultado = await App.BaseDatos.EliminarSitio(item);
                if (resultado != 0)
                    await DisplayAlert("Aviso", "Sitio Eliminado", "OK");
                else
                    await DisplayAlert("Aviso", "Error!", "OK");

                
                var pagina = new SitiosPage();
                {
                    BindingContext = item;
                 };
                await Navigation.PopAsync();
                

            }

        }

       
    }
}