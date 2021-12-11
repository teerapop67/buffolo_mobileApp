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
            Combatscore.Text = Getprofile.score.ToString();
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

        private async void editProfile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditProfilePage());
        }

        private async void Collec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selected = await DisplayAlert("BUFF DELETE ITEM", "Do you want to sale this item?", "YES", "NO");
            
            if(selected)
            {
                Collection itemSelected = e.CurrentSelection[0] as Collection;
                var Getprofile = await BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);

                int agi = 0;
                int str = 0;
                int vit = 0;

                if ( Getprofile.AgiUser - itemSelected.Agi < 0 )
                {
                    agi = 0;
                }  
                else
                {
                    agi = Getprofile.AgiUser - itemSelected.Agi;
                }

                if (Getprofile.StrUser - itemSelected.Str < 0)
                {
                    str = 0;
                }
                else
                {
                    str = Getprofile.StrUser - itemSelected.Str;
                }

                if ( Getprofile.VitUser - itemSelected.Vit < 0 )
                {
                    vit = 0;
                }  
                else
                {
                    vit = Getprofile.VitUser - itemSelected.Vit;
                }

                Profile UpdateProfile = new Profile()
                {
                    Username = Getprofile.Username,
                    CharactorURL = Getprofile.CharactorURL,
                    NameCharactor = Getprofile.NameCharactor,
                    Password = Getprofile.Password,
                    StrUser = str,
                    AgiUser = agi,
                    VitUser = vit,
                    BuffCoin = (int)(LoginPage.SetBuffCoins + Int64.Parse(itemSelected.ItemPrice)),
                    Email = Getprofile.Email,
                    Id = Getprofile.Id,
                    turnTime = Getprofile.turnTime,
                    drawTime = Getprofile.drawTime,
                    score = Getprofile.score,   
                };

                Collections.Remove(itemSelected);

                Collec.ItemsSource = Collections;

                LoginPage.SetBuffCoins = (int)(LoginPage.SetBuffCoins + Int64.Parse(itemSelected.ItemPrice));
                AgiProfile.Text = UpdateProfile.AgiUser.ToString();
                StrProfile.Text = UpdateProfile.StrUser.ToString();
                VitProfile.Text = UpdateProfile.VitUser.ToString();
                CoinProfile.Text = LoginPage.SetBuffCoins.ToString();
                Combatscore.Text = UpdateProfile.score.ToString();
                await BaseViewModel.DataStore.UpdateItemAsync(UpdateProfile);
                await BaseViewModel.DataStoreCollect.DeleteItemAsyncCollec(itemSelected.ItemName);

                await DisplayAlert("BUFF DELETED", "Your item is already saled.", "OK");
            }


            
        }
    }
}