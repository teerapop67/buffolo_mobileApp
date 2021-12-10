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
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
            BindingContext = this;
            LoadProfile();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            totalCoin.Text = $"{LoginPage.SetBuffCoins} BUFF";
        }

        async void LoadProfile()
        {
            var getProfile = await BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);
        }

        private async void Profile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());

        }

        private async void Lucky_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LuckydrawPage());
        }

        private async void Farm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FarmPage());

        }

        private async void Market_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MarketplacePage());        
        }

        private async void Fight_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1());
        }
    }
}
