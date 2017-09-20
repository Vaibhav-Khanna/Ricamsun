using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeldingMask.Pages.Base;
using WeldingMask.Renderers;
using Xamarin.Forms;

namespace WeldingMask.Pages
{
    public partial class CameraPage : BasePage
    {
       

        public CameraPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           
        }

    }
}
