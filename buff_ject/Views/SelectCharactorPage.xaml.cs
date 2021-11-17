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
    public partial class SelectCharactorPage : ContentPage
    {
        public ObservableCollection<Models.Profile> Chalactors { get; set; }
        public static string SaveUrlCharactor;
        public static string SaveNameCharctor;

        public SelectCharactorPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Chalactors = new ObservableCollection<Models.Profile>();
            pCharactor.ItemsSource = Chalactors;
            LoadCharactorData();
        }

        void LoadCharactorData()
        {

            Chalactors.Add(new Models.Profile()
            {
                CharactorURL = "charactor1.jpg",
                NameCharactor = "TAKAMARU"
            });
            Chalactors.Add(new Models.Profile()
            {
                CharactorURL = "charactor2.jpg",
                NameCharactor = "SHINDOSI"

            });
            Chalactors.Add(new Models.Profile()
            {
                CharactorURL = "charactor3.jpg",
                NameCharactor = "TOONKADU",

            });
            Chalactors.Add(new Models.Profile()
            {
                CharactorURL = "charactor4.jpg",
                NameCharactor = "BENZIKA"

            });
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            var param = ((TappedEventArgs)e).Parameter as Models.Profile;

            Xamarin.Forms.FileImageSource objFileImageSource = (Xamarin.Forms.FileImageSource)param.CharactorURL;
            string CharactorUrl = objFileImageSource;
            SaveNameCharctor = param.NameCharactor;
            SaveUrlCharactor = CharactorUrl;
            btnSelect.Text = $"Select: {param.NameCharactor}";
        }

        private async void btnSelect_Clicked(object sender, EventArgs e)
        {
            var selected = await DisplayAlert("Complete", "Are you sure to select this charactor?", "Yes", "No");

            if(selected)
            {

                var Getprofile = await BaseViewModel.DataStore.GetItemAsync(LoginPage.SetUsername);

                LoginPage.SetNameCharactor = SaveNameCharctor;

                Profile UpdateProfile = new Profile()
                {
                    Username = Getprofile.Username,
                    CharactorURL = SaveUrlCharactor,
                    NameCharactor = SaveNameCharctor,
                    Password = Getprofile.Password,
                    StrUser = 1,
                    AgiUser = 1,
                    VitUser = 1,
                    BuffCoin = 0,
                    Email = Getprofile.Email,
                    Id  = Getprofile.Id
                };
                await BaseViewModel.DataStore.UpdateItemAsync(UpdateProfile);
                await Navigation.PushAsync(new MenuPage());
            }
        }
    }
}