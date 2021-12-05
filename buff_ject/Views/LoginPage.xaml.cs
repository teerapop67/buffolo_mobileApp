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
        public ObservableCollection<Profile> Profile { get; set; }
        public static string SetUsername { get; set; }
        public static string SetPassword { get; set; }
        public static string SetNameCharactor{ get; set; }
        public static int SetTotalPower { get; set; }
        public static int SetBuffCoins { get; set; }
        public static string SetCharactorURL { get; set; }

        public static int SetDrawTime { get; set; }


        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            //get file img from local
            //Xamarin.Forms.FileImageSource objFileImageSource = (Xamarin.Forms.FileImageSource)logoImage.Source;
            //string strFileName = objFileImageSource;
            //_ = DisplayAlert("Warning", strFileName, "OK");


            // หาจาก Database ว่ามีข้อมูลของผู้ใช้นี้อยู่ในฐานข้อมูลไหม โดยเอาข้อมูลมาจาก usernameAuth Entry
            var items = await BaseViewModel.DataStore.GetItemAsync(usernameAuth.Text);
            


            if (items == null)
            {
                await DisplayAlert("BUFF WARNING", "Please Register first", "OK");


            }
            else if (items.Username == usernameAuth.Text && items.Password == passwordAuth.Text)
            {
                
                SetUsername = usernameAuth.Text;
                SetPassword = passwordAuth.Text;
                if (items.NameCharactor != null)
                {
                    SetNameCharactor = items.NameCharactor;
                    SetTotalPower = items.AgiUser + items.StrUser + items.VitUser;
                    SetDrawTime = items.drawTime;
                    SetBuffCoins = items.BuffCoin;
                    await Navigation.PushAsync(new MenuPage());
                }
                else
                {
                    await Navigation.PushAsync(new SelectCharactorPage());
                }
            }
            else
            {
                await DisplayAlert("BUFF WARNING", $"Your {usernameAuth.Text} or {passwordAuth.Text} WRONG!!", "OK");
            }

            
        }

        

        private async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
            
        }
    }
}