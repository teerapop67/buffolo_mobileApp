using buff_ject.Models;
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
    public partial class EditProfilePage : ContentPage
    {
        public EditProfilePage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            editUser.Text = LoginPage.SetUsername;
            editPass.Text = LoginPage.SetPassword;
            editEmail.Text = LoginPage.SetEmail;
        }
            private async void backButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void edit_Clicked(object sender, EventArgs e)
        {
            var GetProfile = await BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);
            var GetTotalItem = await BaseViewModel.DataStoreCollect.GetItemsAsyncCollec();


            if ( editUser.Text != null && editEmail.Text != null && editPass.Text != null)
            {
                await DisplayAlert("BUFF sdasd", $"{editEmail.Text} | {editUser.Text} | {editPass.Text}", "OK");

                Profile UpdateProfiles = new Profile()
                {
                    Username = editUser.Text,
                    CharactorURL = GetProfile.CharactorURL,
                    NameCharactor = GetProfile.NameCharactor,
                    Password = editPass.Text,
                    StrUser = GetProfile.StrUser,
                    AgiUser = GetProfile.AgiUser,
                    VitUser = GetProfile.VitUser,
                    BuffCoin = LoginPage.SetBuffCoins,
                    Email = editEmail.Text,
                    Id = GetProfile.Id,
                    drawTime = GetProfile.drawTime,
                    turnTime = GetProfile.turnTime
                };


           



                await BaseViewModel.DataStore.UpdateItemAsync(UpdateProfiles);

                foreach (Collection item in GetTotalItem)
                {
                    if (item.Username == LoginPage.SetUsername)
                    {
                        Collection UpdateCollections = new Collection()
                        {
                            Username = editUser.Text,
                            ItemPrice = item.ItemPrice,
                            ItemName = item.ItemName,
                            ItemImage = item.ItemImage,
                            Str = item.Str,
                            Agi = item.Agi,
                            Vit = item.Vit
                        };
                        await BaseViewModel.DataStoreCollect.UpdateItemAsyncCollec(UpdateCollections);

                    }
                }

                LoginPage.SetPassword = editPass.Text;
                LoginPage.SetUsername = editUser.Text;
                LoginPage.SetEmail = editEmail.Text;
                await DisplayAlert("BUFF EDIT", "EDIT COMPLETED", "OK");
                await Navigation.PopAsync();

            }

        }
    }
}