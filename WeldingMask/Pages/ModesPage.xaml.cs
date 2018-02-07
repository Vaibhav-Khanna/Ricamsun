using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WeldingMask.Pages
{
    public partial class ModesPage : ContentPage
    {
        public ModesPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            date.Text = $"©{DateTime.UtcNow.Year} RicamSun - Tout droits réservés";
        }
    }
}
