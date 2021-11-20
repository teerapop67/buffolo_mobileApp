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
    public partial class FarmPage : ContentPage
    {
        public IList<Profile> ChaName { get; private set; }
        public static int havesting = 0;
        public bool isStake;


        public int selectedNamePower { get; set; }
        public string selectedName { get; set; }
        private static Timer aTimer;

        public FarmPage()
        {
            InitializeComponent();
            CreateNameCharactor();
            BindingContext = this;

        }

        protected override void OnAppearing()
        {
            if(havesting > 0)
            {
                stake.Text = "Staking...";
                PickerChaName.SelectedIndex = 0;
                ShowCharactor.Text = selectedName;
                HavestedCoin.Text = havesting.ToString();
            } else
            {
                HavestedCoin.Text = "0.00";

            }
            totalCoin.Text = $"{LoginPage.SetBuffCoins} BUFF";
        }
        void CreateNameCharactor()
        {
            ChaName = new List<Profile>();
            ChaName.Add(new Profile
            {
                NameCharactor = LoginPage.SetNameCharactor,
                TotalPower = LoginPage.SetTotalPower
                
            });
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            havesting = havesting + ((LoginPage.SetTotalPower * 2)/100) + 10 ;
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }


        public void stake_Clicked(object sender, EventArgs e)
        {
            if (havesting == 0 && selectedName != null)
            {
                aTimer = new Timer();
                aTimer.Interval = 3000;


                aTimer.Elapsed += OnTimedEvent;
                aTimer.AutoReset = true;
                aTimer.Enabled = true;

                stake.Text = "Staking...";
                HavestedCoin.TextColor = Color.GreenYellow;
                recentCoin.TextColor = Color.LightYellow;
                HavestedCoin.Text = havesting.ToString();
            }
            else if (selectedName == null)
            {
                DisplayAlert("BUFF WARNING", "Select Charactor First", "OK");
            }

            isStake = true;

        }

        private async void havest_Clicked(object sender, EventArgs e)
        {
            aTimer.Enabled = false;
            stake.Text = "Stake";
            isStake = false;

            var Getprofile = await BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);

            LoginPage.SetBuffCoins += havesting;
            totalCoin.Text = $"{LoginPage.SetBuffCoins} BUFF";
            havesting = 0;
            HavestedCoin.TextColor = Color.White;
            recentCoin.TextColor = Color.White;
            HavestedCoin.Text = "0.00";
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
                Id = Getprofile.Id
            };
            await BaseViewModel.DataStore.UpdateItemAsync(UpdateProfile);

            await DisplayAlert("BUFF EARNED", "Your havested complete!", "OK");


        }

        private async void goBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void PickerChaName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PickerChaName.SelectedIndex >= 0)
            {
                Profile SelectedName = (Profile)PickerChaName.ItemsSource[PickerChaName.SelectedIndex];
                ShowCharactor.Text = SelectedName.NameCharactor;
                selectedName = SelectedName.NameCharactor;
                selectedNamePower = SelectedName.TotalPower;
            }
        }

        private void recentCoin_Clicked(object sender, EventArgs e)
        {
            if(isStake)
            {
                HavestedCoin.Text = havesting.ToString();
            }

        }
    }
}