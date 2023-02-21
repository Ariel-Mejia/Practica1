using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;
using Plugin.Geolocator.Abstractions;

namespace Practica1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMaps : ContentPage
    {
        public PageMaps()
        {
            InitializeComponent();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var conectividad = Connectivity.NetworkAccess;


            /*
            if (conectividad == NetworkAccess.Internet)
            {
                var localizacion = await Geolocation.GetLocationAsync();
                if (localizacion != null) 
                {
                    Pin ubicacion = new Pin();
                    ubicacion.Label = "Mi Ubicacion";
                    ubicacion.Address = "Mi Ubicacion";
                    ubicacion.Position = new Position(localizacion.Latitude, localizacion.Longitude);
                    mapa.Pins.Add(ubicacion);

                    mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(localizacion.Latitude, localizacion.Longitude), Distance.FromKilometers(1)));
                
                }

            }
            */

            if (conectividad == NetworkAccess.Internet)
            {
                var locl = CrossGeolocator.Current;
                if (locl != null)
                {
                    locl.PositionChanged += Locl_PositionChanged;
                    if (!locl.IsListening)
                    {
                        await locl.StartListeningAsync(TimeSpan.FromSeconds(10), 100);
                    }

                    var posicion = await locl.GetPositionAsync();
                    var mapcenter = new Xamarin.Forms.Maps.Position(posicion.Latitude, posicion.Longitude);
                    mapa.MoveToRegion(new MapSpan(mapcenter,1 ,1));
                }
                
                else
                {
                    var posicion = await locl.GetLastKnownLocationAsync();
                    var mapcenter = new Xamarin.Forms.Maps.Position(posicion.Latitude, posicion.Longitude);
                    mapa.MoveToRegion(new MapSpan(mapcenter, 1, 1));
                }
            }
        }

        private void Locl_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var mapcenter = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude);
            mapa.MoveToRegion(new MapSpan(mapcenter, 1, 1));
        }
    }
}