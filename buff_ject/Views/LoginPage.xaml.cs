using buff_ject.Models;
using buff_ject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace buff_ject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public IDataStore<Profile> DataStore => DependencyService.Get<IDataStore<Profile>>();
        public ObservableCollection<Profile> Profile { get; set; }
        public string usernameCopy;

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {

            var items = await DataStore.GetItemAsync(usernameAuth.Text);

            if (items == null)
            {
                _ = DisplayAlert("Warning", "Please Register first", "OK");

                await Navigation.PushAsync(new RegisterPage());

            }
            else if (items.Username == usernameAuth.Text && passwordAuth.Text == items.Password)
            {
                _ = DisplayAlert("Welcome", $"Welcome to buff { usernameAuth.Text }", "OK");
            }

            
        }

        

        private async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
            
        }
    }
}