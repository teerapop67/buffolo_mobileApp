using buff_ject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace buff_ject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ObservableCollection<Models.Collection> Collections { get; set; }
        ObservableCollection<ImageSource> imageList = null;
        bool IsPicker = false;
        public int countza = 0;

        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = this;

        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();

            
            var Getprofile = await BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);
            var GetTotalItem = await BaseViewModel.DataStoreCollect.GetItemsAsyncCollec();

            Collections = new ObservableCollection<Models.Collection>();
            Collec.ItemsSource = Collections;
            ProfileUrl.Source = Getprofile.CharactorURL;
            UserProfile.Text = Getprofile.Username;
            CoinProfile.Text = Getprofile.BuffCoin.ToString();
            
            AgiProfile.Text = Getprofile.AgiUser.ToString();
            StrProfile.Text = Getprofile.StrUser.ToString();
            VitProfile.Text = Getprofile.VitUser.ToString();
            LoadCollecData();
            
        }

        protected override void OnDisappearing()
        {
            IsPicker = false;

            base.OnDisappearing();
        }

        async void LoadCollecData()
        {

            var GetTotalItem = await BaseViewModel.DataStoreCollect.GetItemsAsyncCollec();


            foreach (Collection item in GetTotalItem)
            {
                if(item.Username == LoginPage.SetUsername)
                {
                    countza += 1;
                    Collections.Add(new Collection()
                    {
                        ItemImage = item.ItemImage,
                        ItemName = item.ItemName,
                        ItemPrice = item.ItemPrice,
                        Username = LoginPage.SetUsername,
                        Str = item.Str,
                        Vit = item.Vit,
                        Agi = item.Agi
                    });
                }
            }
            ItemProfile.Text = countza.ToString();
        }

        private async void goBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }

        private void editProfile_Clicked(object sender, EventArgs e)
        {

        }

    }
}