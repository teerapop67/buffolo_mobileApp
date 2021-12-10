using buff_ject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace buff_ject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarketplacePage : ContentPage
    {
        public ObservableCollection<Models.Collection> Items { get; set; }
        public MarketplacePage()
        {
            InitializeComponent();
            BindingContext = this;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Items = new ObservableCollection<Models.Collection>();
            Item.ItemsSource = Items;
            totalCoin.Text = $"{LoginPage.SetBuffCoins} BUFF";
            LoadItemData();
        }

        void LoadItemData()
        {
            Items.Add(new Models.Collection()
            {
                ItemImage = "Diamond_Sword.png",
                ItemName = "Diamond Sword",
                ItemPrice = "150 buff",
                Agi = 120,
                Str = 50,
                Vit = 20,
                Username = LoginPage.SetUsername
                
            });

            Items.Add(new Models.Collection()
            {
                ItemImage = "Bow.png",
                ItemName = "Wood Bow",
                ItemPrice = "85 buff",
                Agi = 20,
                Str = 80,
                Vit = 70,
                Username = LoginPage.SetUsername
            });

            Items.Add(new Models.Collection()
            {
                ItemImage = "CaptianBenz.png",
                ItemName = "CaptianBenz",
                ItemPrice = "999 buff",
                Agi = 999,
                Str = 999,
                Vit = 999,
                Username = LoginPage.SetUsername
            });

            Items.Add(new Models.Collection()
            {
                ItemImage = "Shield.png",
                ItemName = "Wood Shield",
                ItemPrice = "50 buff",
                Agi = 20,
                Str = 100,
                Vit = 80,
                Username = LoginPage.SetUsername
            });

            Items.Add(new Models.Collection()
            {
                ItemImage = "Diamondarmor.png",
                ItemName = "Diamond Armor",
                ItemPrice = "200 buff",
                Agi = 120,
                Str = 25,
                Vit = 12,
                Username = LoginPage.SetUsername
            });

            Items.Add(new Models.Collection()
            {
                ItemImage = "StardustStaff.png",
                ItemName = "Stardust",
                ItemPrice = "65 buff",
                Agi = 20,
                Str = 50,
                Vit = 90,
                Username = LoginPage.SetUsername
            });

            Items.Add(new Models.Collection()
            {
                ItemImage = "RedDiamondSword.png",
                ItemName = "Red Diamond Sword",
                ItemPrice = "250 buff",
                Agi = 80,
                Str = 30,
                Vit = 10,
                Username = LoginPage.SetUsername
            });

            Items.Add(new Models.Collection()
            {
                ItemImage = "AK47.png",
                ItemName = "AK47",
                ItemPrice = "800 buff",
                Agi = 150,
                Str = 100,
                Vit = 90,
                Username = LoginPage.SetUsername
            });
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var param = ((TappedEventArgs)e).Parameter as Models.Collection;
            var selected = await DisplayAlert("BUFF MARKET", $"Are you sure to BUY this Item? Str: {param.Str}  Agi: {param.Agi}  Vit: {param.Vit}", "Buy", "Later");
            string[] setPrice = param.ItemPrice.Split(' ');


            Collection UpdateCollections = new Collection()
            {
                Username = LoginPage.SetUsername,
                ItemPrice = setPrice[0],
                ItemName = param.ItemName,
                ItemImage = param.ItemImage,
                Str = param.Str,
                Agi = param.Agi,
                Vit = param.Vit
            };

            var Getprofile = await BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);

            Profile UpdateProfile = new Profile()
            {
                Username = Getprofile.Username,
                CharactorURL = Getprofile.CharactorURL,
                NameCharactor = Getprofile.NameCharactor,
                Password = Getprofile.Password,
                StrUser = Getprofile.StrUser + param.Str,
                AgiUser = Getprofile.AgiUser + param.Agi,
                VitUser = Getprofile.VitUser + param.Vit,
                BuffCoin = LoginPage.SetBuffCoins - Int16.Parse(setPrice[0]),
                Email = Getprofile.Email,
                Id = Getprofile.Id
            };



            if (selected)
            {
                //var Getprofile = await BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);

                if(Getprofile.BuffCoin >= Int16.Parse(setPrice[0]))
                {
                    await BaseViewModel.DataStoreCollect.AddItemAsyncCollec(UpdateCollections);
                    await BaseViewModel.DataStore.UpdateItemAsync(UpdateProfile);
                    LoginPage.SetBuffCoins = UpdateProfile.BuffCoin;
                    LoginPage.SetTotalPower += UpdateProfile.StrUser + UpdateProfile.AgiUser + UpdateProfile.VitUser;
                    totalCoin.Text = $"{LoginPage.SetBuffCoins} BUFF";

                    await DisplayAlert("Complete:", $"You have bought {UpdateCollections.ItemName} : Price {UpdateCollections.ItemPrice} ", "OK");

                }
                else
                {
                    await DisplayAlert("BUFF WARNING", "Your Buff is not enough try again later", "OK");
                }
            }
        }

        private async void goBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }
    }
}