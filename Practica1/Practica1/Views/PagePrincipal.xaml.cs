using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Practica1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePrincipal : ContentPage
    {
        public PagePrincipal()
        {
            InitializeComponent();
        }

        private void listaequipos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var elemento = e.CurrentSelection.FirstOrDefault()as Model.Equipos;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Listaequipos.ItemsSource = await App.Dbequipos.ListaEquipos();
        }

        private async void Toolagregar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.PageEquipos());
        }

        private async void Toolupdate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.PageEquipos());
        }

        private async void Toolmaps_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.PageMaps());
        }
    }
}