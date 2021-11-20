using buff_ject.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("mttregular.otf", Alias = "mttregular")]
[assembly: ExportFont("msregular.otf", Alias = "msregular")]
[assembly: ExportFont("moregular.otf", Alias = "moregular")]
[assembly: ExportFont("mrregular.otf", Alias = "mrregular")]
[assembly: ExportFont("FontAwesome5FreeSolid900.otf", Alias = "AWF")]
[assembly: ExportFont("PressStart2P-Regular.ttf", Alias = "PressStart")]


namespace buff_ject
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.Register<IDataStore<Models.Profile>, FSDataStore>();
            DependencyService.Register<IDataStoreCollection<Models.Collection>, FSDataStoreCollection>();

            MainPage = new NavigationPage(new Views.LoadingPage());
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
