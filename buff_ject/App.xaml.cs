using buff_ject.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("mttregular.otf", Alias = "mttregular")]
[assembly: ExportFont("msregular.otf", Alias = "msregular")]
[assembly: ExportFont("moregular.otf", Alias = "moregular")]
[assembly: ExportFont("mrregular.otf", Alias = "mrregular")]
[assembly: ExportFont("FontAwesome5FreeRegular400.otf", Alias = "FontAwesome5Regular")]


namespace buff_ject
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.Register<IDataStore<Models.Profile>, FSDataStore>();

            MainPage = new NavigationPage(new Views.LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
