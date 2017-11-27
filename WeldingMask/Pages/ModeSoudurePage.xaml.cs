using System;
using System.Collections.Generic;
using WeldingMask.PageModels;
using Xamarin.Forms;

namespace WeldingMask.Pages
{
    public partial class ModeSoudurePage : ContentPage
    {
      
        private ModeSoudurePageModel context;

        public ModeSoudurePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            context = BindingContext as ModeSoudurePageModel;

            if (context != null)
            {
                context.PropertyChanged -= Context_PropertyChanged;
                context.PropertyChanged += Context_PropertyChanged;
            }

        }

        void Context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (context == null)
                return;

            if (e.PropertyName == "ShieldOn")
            {
                if (context.ShieldOn)
                    ShieldButton.Style = (Style)Application.Current.Resources["ShieldButtonOn"];
                else
                    ShieldButton.Style = (Style)Application.Current.Resources["ShieldButtonOff"];
            }
            if (e.PropertyName == "FocusOn")
            {
                if (context.FocusOn)
                    FocusButton.Style = (Style)Application.Current.Resources["FocusButtonOn"];
                else
                    FocusButton.Style = (Style)Application.Current.Resources["FocusButtonOff"];
            }

            if (e.PropertyName == "ExposureOn")
            {
                if (context.ExposureOn)
                    ExposureButton.Style = (Style)Application.Current.Resources["ExposureButtonOn"];
                else
                    ExposureButton.Style = (Style)Application.Current.Resources["ExposureButtonOff"];
            }
        }


        void Handle_Tapped(object sender, System.EventArgs e)
        {
            if (PageContent.BackgroundColor == Color.Transparent)
            {
                PageContent.BackgroundColor = Color.White;
                PageContent.Opacity = 0.9;
                controls.IsVisible = false;
            }
            else
            {
                PageContent.BackgroundColor = Color.Transparent;
                PageContent.Opacity = 1;
                controls.IsVisible = true;
            }
        }

        void Handle_Tapped1(object sender, System.EventArgs e)
        {

        }
       
    }
}
