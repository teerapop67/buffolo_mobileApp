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
                Charactor = "charactor1.jpg",
                NameCharactor = "TAKAMARU"
            });
            Chalactors.Add(new Models.Profile()
            {
                Charactor = "charactor2.jpg",
                NameCharactor = "SHINDOSI"

            });
            Chalactors.Add(new Models.Profile()
            {
                Charactor = "charactor3.jpg",
                NameCharactor = "TOONKADU",

            });
            Chalactors.Add(new Models.Profile()
            {
                Charactor = "charactor4.jpg",
                NameCharactor = "BENZIKA"

            });
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var param = ((TappedEventArgs)e).Parameter as Models.Profile;

            Xamarin.Forms.FileImageSource objFileImageSource = (Xamarin.Forms.FileImageSource)param.Charactor;
            string CharactorURL = objFileImageSource;
            await DisplayAlert("WANRNB:", $"{param.NameCharactor} : {CharactorURL}", "OK");
            btnSelect.Text = $"Select: {param.NameCharactor}";
        }
    }
}