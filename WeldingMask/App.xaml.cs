using Xamarin.Forms;
using FreshMvvm;
using WeldingMask.PageModels;
using Plugin.Permissions;

namespace WeldingMask
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var page = FreshPageModelResolver.ResolvePageModel<PermissionsPageModel>();
            var container = new FreshNavigationContainer(page){ BarTextColor = Color.White, BarBackgroundColor = Color.Black };

            MainPage = container;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
