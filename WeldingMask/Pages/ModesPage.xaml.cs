using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace WeldingMask.Pages
{
    public partial class ModesPage : ContentPage
    {
        public ModesPage()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            date.Text = $"©{DateTime.UtcNow.Year} RicamSun - Tout droits réservés";
        }
    }
}
