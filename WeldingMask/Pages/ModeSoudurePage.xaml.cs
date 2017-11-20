using System;
using System.Collections.Generic;
using WeldingMask.PageModels;
using Xamarin.Forms;

namespace WeldingMask.Pages
{
    public partial class ModeSoudurePage : ContentPage
    {
        public ModeSoudurePage()
        {
            InitializeComponent();NavigationPage.SetHasNavigationBar(this, false);
        }

        void Shield_Tapped(object sender, System.EventArgs e)
        {
            if ((BindingContext as ModeSoudurePageModel).ShieldOn)
            {
                ShieldButton.Style = (Style)Application.Current.Resources["ShieldButtonOff"];
            }
            else
            {
                ShieldButton.Style = (Style)Application.Current.Resources["ShieldButtonOn"];
            }
            (BindingContext as ModeSoudurePageModel).ShieldOn = !(BindingContext as ModeSoudurePageModel).ShieldOn;

        }
        void Focus_Tapped(object sender, System.EventArgs e)
        {
            if ((BindingContext as ModeSoudurePageModel).FocusOn)
            {
                FocusButton.Style = (Style)Application.Current.Resources["FocusButtonOff"];
            }
            else
            {
                FocusButton.Style = (Style)Application.Current.Resources["FocusButtonOn"];
            }
            (BindingContext as ModeSoudurePageModel).FocusOn = !(BindingContext as ModeSoudurePageModel).FocusOn;
        }

        void Exposure_Tapped(object sender, System.EventArgs e)
        {
            if ((BindingContext as ModeSoudurePageModel).ExposureOn)
            {
                ExposureButton.Style = (Style)Application.Current.Resources["ExposureButtonOff"];
            }
            else
            {
                ExposureButton.Style = (Style)Application.Current.Resources["ExposureButtonOn"];
            }
            (BindingContext as ModeSoudurePageModel).ExposureOn = !(BindingContext as ModeSoudurePageModel).ExposureOn;
        }
    }
}
