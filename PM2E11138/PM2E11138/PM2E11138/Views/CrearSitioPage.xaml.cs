using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E11138.Views;
using PM2E11138.Models;
using System.IO;
using Plugin.Media;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Essentials;
using System.Threading;

namespace PM2E11138.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearSitioPage : ContentPage
    {
        public CrearSitioPage()
        {
            InitializeComponent();
        }

        byte[] imageToSave;

        private async  void Agregar_Clicked(object sender, EventArgs e)
        {
            
            var sites = new Sitios
            {
                Codigo = Convert.ToInt32(Codigo.Text),
                Imagen = imageToSave,
                Latitud = (float)Convert.ToDouble(Latitud.Text),
                Longitud = (float)Convert.ToDouble(Longitud.Text),
                Descripcion = Descripcion.Text
            };

            if (Descripcion.Text !=null)
            {


                var resultado = await App.BaseDatos.GuardarSitio(sites);

                if (resultado != 0)
                {
                    await DisplayAlert("", "Sitio Agregado.", "OK");
                    var pagina = new CrearSitioPage();
                    await Navigation.PushAsync(pagina);
                }
                else
                {
                    await DisplayAlert("", "Ha ocurrido un error.", "OK");
                }

            }
            else {
               await DisplayAlert("","Escriba una descripcion del sitio","OK");
            }


        }

        private async void Lista_Clicked(object sender, EventArgs e)
        {
            var pagina = new SitiosPage();
            await Navigation.PushAsync(pagina);
        }


        private void Salir_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private async void TomarFoto_Clicked(object sender, EventArgs e)
        {
            var takepic = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "PhotoApp",
                Name = DateTime.Now.ToString() + "_Pic.jpg",
                SaveToAlbum = true
            });

          //  await DisplayAlert("Ubicacion de la foto: ", takepic.Path, "Ok");

            if (takepic != null)
            {
                imageToSave = null;
                MemoryStream memoryStream = new MemoryStream();

                takepic.GetStream().CopyTo(memoryStream);
                imageToSave = memoryStream.ToArray();

                img.Source = ImageSource.FromStream(() => { return takepic.GetStream(); });
            }

            try
            {
                
                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                
                var cts = new CancellationTokenSource();
                
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                

                if (location != null)
                {
                    Latitud.Text = location.Latitude.ToString();
                    Longitud.Text = location.Longitude.ToString();
                    
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                
                await DisplayAlert("Mi App", "Mi dispositivo no soporta GPS", "Continuar");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                
                await DisplayAlert("Mi App", "Mi dispositivo genero un error", "Continuar");
            }
            catch (PermissionException pEx)
            {
                
                await DisplayAlert("Mi App", "Mi dispositivo no tiene permiso para el gps", "Continuar");
            }
            catch (Exception ex)
            {
                
                await DisplayAlert("Mi App", "Mi dispositivo fallo al traer mi ubicación", "Continuar");
            }


        }

      

        private byte[] GetImageBytes(Stream stream)
        {
            byte[] ImageBytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                stream.CopyTo(memoryStream);
                ImageBytes = memoryStream.ToArray();
            }
            return ImageBytes;
        }

    }

    

}