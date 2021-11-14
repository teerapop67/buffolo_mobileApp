using buff_ject.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace buff_ject
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            LoadProfile();

        }

        async void LoadProfile()
        {
            var getProfile = await BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);
            UrlProfile.Source = getProfile.CharactorURL;
        }
    }
}
