using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Practica1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageEquipos : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile photo = null;
        public PageEquipos()
        {
            InitializeComponent();
        }

        private String traeImagenToBase64() 
        {
            if (photo != null) { using (MemoryStream memory = new MemoryStream()) 
                { 
                    Stream stream = photo.GetStream(); 
                    stream.CopyTo(memory); 
                    byte[] fotobyte = memory.ToArray();
                    
                    string Base64String = Convert.ToBase64String(fotobyte); 
                    return Base64String;
                } 
            } 
            return null; 
        }


        private byte[] traeImagenTobytes()
        {
            if (photo != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotobyte = memory.ToArray();

                    
                    return fotobyte;
                }
            }
            return null;
        }

        private async void Btningresar_Clicked(object sender, EventArgs e)
        {
            var equi = new Model.Equipos
            {
                Nombre = nombre.Text,
                Fundacion = fundacion.Date,
                Correo = correo.Text,
                Categoria = categoria.SelectedItem.ToString(),
                Logo = traeImagenTobytes(),
            };

            if (await App.Dbequipos.StoreEquipo(equi) > 0)
            {
                await DisplayAlert("Aviso", "Equipo Ingresado con Exito", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Equipo no Ingresado", "OK");
            }

        }

        private async void Btnfoto_Clicked(object sender, EventArgs e)
        {
            photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "AppEquipos",
                Name = "Foto.jpg",
                SaveToAlbum = true,
            });

            if(photo != null) 
            {
               Foto.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }
        }
    }
}