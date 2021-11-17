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
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
            Loading();
        }

        async void Loading()
        {
            imageLogo.Opacity = 0;
            await imageLogo.FadeTo(1, 7000);
            await Navigation.PushAsync(new LoginPage());
        }
    }
}