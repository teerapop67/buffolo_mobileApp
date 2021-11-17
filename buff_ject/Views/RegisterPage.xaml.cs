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

            var getProfile = await BaseViewModel.DataStore.GetItemAsync(usernameRegis.Text);


            user = usernameRegis.Text;

            if(usernameRegis.Text != null && passwordRegis.Text != null && emailRegis.Text != null)
            {
                
                if (getProfile == null)
                {
                    await BaseViewModel.DataStore.AddItemAsync(UserAuth);

                    await Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    await DisplayAlert("BUFF WARNING", "This Username already exist", "OK");
                }

            }
            else
            {
                await DisplayAlert("BUFF WARNING", "Please fill out all of entry", "OK");
            }

            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());

        }
    }
}