using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Practica1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnsegunda_Clicked(object sender, EventArgs e)
        {
            var estudiante = new Model.Estudiante
            {
                nombre = nombre.Text,
                apellido = apellido.Text,
                telefono = telefono.Text,
            };

            



            var pagina = new Views.PageTwo();
            pagina.BindingContext = estudiante;
            await Navigation.PushAsync(pagina);
        }
    }
}
