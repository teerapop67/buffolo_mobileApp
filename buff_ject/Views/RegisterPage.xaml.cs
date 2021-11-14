using buff_ject.Models;
using buff_ject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace buff_ject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private int IdUer;
        public static string user { get; set; }

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void Registers_Clicked(object sender, EventArgs e)
        {

            Profile UserAuth = new Profile()
            {
                Id = Guid.NewGuid().ToString(),
                Username = usernameRegis.Text,
                Password = passwordRegis.Text,
                Email = emailRegis.Text
            };

            user = usernameRegis.Text;

            await BaseViewModel.DataStore.AddItemAsync(UserAuth);

            await Navigation.PushAsync(new LoginPage());
        }
    }
}