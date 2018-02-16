using Xamarin.Forms;
using FreshMvvm;
using WeldingMask.PageModels;
using Plugin.Permissions;
using System;

namespace WeldingMask
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();


            Page page;
            FreshNavigationContainer container;

            if (Current.Properties.ContainsKey("DisclaimerAgree"))
            {
                page = FreshPageModelResolver.ResolvePageModel<ModesPageModel>();
                container = new FreshNavigationContainer(page) { BarTextColor = Color.White, BarBackgroundColor = Color.Black };
            }
            else
            {
                page = FreshPageModelResolver.ResolvePageModel<DisclaimerPageModel>();
                container = new FreshNavigationContainer(page) { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
            }

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
