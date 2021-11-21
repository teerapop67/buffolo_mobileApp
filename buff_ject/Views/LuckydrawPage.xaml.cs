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
        public LuckydrawPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void backButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void drawButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}