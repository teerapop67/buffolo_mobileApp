using buff_ject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;
using System.ComponentModel;

namespace buff_ject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LuckydrawPage : ContentPage
    {
        //public IList<Profile> ChaName { get; private set; }



        public List<string> ranList = new List<string>();
        static Random rnd = new Random();
        static int limit ;
        public LuckydrawPage()
        {
            InitializeComponent();
            addList();
            var GetProfile2 = BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);
            BindingContext = this;
        }


        //private void defineUsername()
        //{
        //    ChaName = new List<Profile>();
        //    ChaName.Add(new Profile
        //    {
        //        NameCharactor = LoginPage.SetNameCharactor,
        //    });
        //}

        protected override async void OnAppearing()
        {

            // Get Profile data
            var Getprofile = await BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);
            limit = Getprofile.drawTime;

            // await DisplayAlert("TEST DRAW", Getprofile.Username.ToString(), "TEST END");
            // await DisplayAlert("TEST DRAW", Getprofile.drawTime.ToString(), "TEST END");

            limitDraw.Text = limit.ToString() + " Draw left";
            totalCoin.Text = $"{LoginPage.SetBuffCoins} BUFF";


        }
        private async void backButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void drawButton_Clicked(object sender, EventArgs e)
        {
            var Getprofile = await BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);
            limit = Getprofile.drawTime;
            //await DisplayAlert("limit time", limit.ToString(), "OK");

            if (limit > 0)
            {
                int r = rnd.Next(ranList.Count);
                gotItem.Text = ranList[r].ToString();
                limit -= 1;
                limitDraw.Text = limit.ToString() + " Draw left";

                // Initial Draw Clearing
                Profile UpdateDraw = new Profile()
                {
                    Username = Getprofile.Username,
                    CharactorURL = Getprofile.CharactorURL,
                    NameCharactor = Getprofile.NameCharactor,
                    Password = Getprofile.Password,
                    StrUser = Getprofile.StrUser,
                    AgiUser = Getprofile.AgiUser,
                    VitUser = Getprofile.VitUser,
                    BuffCoin = LoginPage.SetBuffCoins,
                    Email = Getprofile.Email,
                    Id = Getprofile.Id,
                    drawTime = limit
                };
                await BaseViewModel.DataStore.UpdateItemAsync(UpdateDraw);

                if (ranList[r] == "5" || ranList[r] == "10" || ranList[r] == "100" || ranList[r] == "500" || ranList[r] == "1000" || ranList[r] == "1")
                {
                    LoginPage.SetBuffCoins += Convert.ToInt32(ranList[r]);
                    await DisplayAlert("Got", ranList[r].ToString(), "OK then");
                    Profile UpdateProfile = new Profile()
                    {
                        Username = Getprofile.Username,
                        CharactorURL = Getprofile.CharactorURL,
                        NameCharactor = Getprofile.NameCharactor,
                        Password = Getprofile.Password,
                        StrUser = Getprofile.StrUser,
                        AgiUser = Getprofile.AgiUser,
                        VitUser = Getprofile.VitUser,
                        BuffCoin = LoginPage.SetBuffCoins,
                        Email = Getprofile.Email,
                        Id = Getprofile.Id,
                        drawTime = limit

                    };
                    totalCoin.Text = $"{LoginPage.SetBuffCoins} BUFF";
                    await BaseViewModel.DataStore.UpdateItemAsync(UpdateProfile);
                }
            }
            else
            {
                await DisplayAlert("Out of draw", "Go away noob", "OK then");
            }
            

        }
        private void addList()
        {
            ranList.Add("SaltZa");
            ranList.Add("SaltWow");
            ranList.Add("SaltOmg");
            ranList.Add("SaltJesus");
            ranList.Add("SaltYourMom !");
            ranList.Add("SaltNani !!");
            ranList.Add("SaltOhMyFkingGOd");
            ranList.Add("SaltWTF");
            ranList.Add("SaltOHbubu");
            ranList.Add("Saltอะแหน่อะเหนะ");
            ranList.Add("Saltอ้าวๆ");
            ranList.Add("Saltไปดี๊");
            ranList.Add("Saltอีกแล้วเหรอครัชช");
            ranList.Add("Saltแน่นอนครับ");
            ranList.Add("10");
            ranList.Add("10");
            ranList.Add("10");
            ranList.Add("5");
            ranList.Add("5");
            ranList.Add("5");
            ranList.Add("5");
            ranList.Add("100");
            ranList.Add("15");
            ranList.Add("1");
        }
    }
}