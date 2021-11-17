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
                Agi = 20,
                Str = 50,
                Vit = 10,
                Username = LoginPage.SetUsername
                
            });

            Items.Add(new Models.Collection()
            {
                ItemImage = "Bow.png",
                ItemName = "Wood Bow",
                ItemPrice = "85 buff",
                Agi = 20,
                Str = 50,
                Vit = 10,
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
                Str = 50,
                Vit = 10,
                Username = LoginPage.SetUsername
            });

            Items.Add(new Models.Collection()
            {
                ItemImage = "Diamondarmor.png",
                ItemName = "Diamond Armor",
                ItemPrice = "200 buff",
                Agi = 20,
                Str = 50,
                Vit = 10,
                Username = LoginPage.SetUsername
            });

            Items.Add(new Models.Collection()
            {
                ItemImage = "StardustStaff.png",
                ItemName = "Stardust",
                ItemPrice = "65 buff",
                Agi = 20,
                Str = 50,
                Vit = 10,
                Username = LoginPage.SetUsername
            });

            Items.Add(new Models.Collection()
            {
                ItemImage = "RedDiamondSword.png",
                ItemName = "Red Diamond Sword",
                ItemPrice = "250 buff",
                Agi = 20,
                Str = 50,
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
            var selected = await DisplayAlert("Complete:", "Are you sure to select this charactor?", "Yes", "No");
            if (selected)
            {
                await DisplayAlert("Complete:", $"You bought {param.ItemName}", "OK");
            }
        }

        private async void goBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }
    }
}