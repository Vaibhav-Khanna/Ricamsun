﻿using System;
using System.Collections.Generic;
using WeldingMask.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace WeldingMask.Pages
{
    public partial class DisclaimerPage : ContentPage
    {
        private string _grey = "#d9d9d9";
        private string _green = "#7ed321";
     
        public DisclaimerPage()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        private void TermsConditions_Tapped(object sender, EventArgs e)
        {
            (BindingContext as DisclaimerPageModel).TermsAccepted = !(BindingContext as DisclaimerPageModel).TermsAccepted;

            if((BindingContext as DisclaimerPageModel).TermsAccepted)
            {
                TermsConditions.Source = "check.png";
                Validate.BackgroundColor = Color.FromHex(_green);
                (BindingContext as DisclaimerPageModel).ShouldValidate = true;
            }
            else
            {
                TermsConditions.Source = "uncheck.png";
                Validate.BackgroundColor = Color.FromHex(_grey);
                (BindingContext as DisclaimerPageModel).ShouldValidate = false;
            }
        }
    }
}
