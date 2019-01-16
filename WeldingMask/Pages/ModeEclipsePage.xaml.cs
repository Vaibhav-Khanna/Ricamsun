using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using WeldingMask.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Application = Xamarin.Forms.Application;

namespace WeldingMask.Pages
{
	public partial class ModeEclipsePage : ContentPage
	{

        private ModeEclipsePageModel context;

		public ModeEclipsePage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent();

            //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            if (Device.RuntimePlatform==Device.Android)
            {
                controls.Opacity = 1;
            }

        }       

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            context = BindingContext as ModeEclipsePageModel;

            if(context!=null)
            {
                context.PropertyChanged -= Context_PropertyChanged;
                context.PropertyChanged += Context_PropertyChanged;
            }
           
        } 

        void Context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (context == null)
                return;

            if(e.PropertyName == "ShieldOn")
            {
                if (context.ShieldOn)
                {
                    btnShield.Source = "btnstart.png";

                    ExposureLabel.Opacity = 1;
                    FocusLabel.Opacity = 1;
                    ExposureButton.Opacity = 1;
                    FocusButton.Opacity = 1;
                }
                else
                {
                    btnShield.Source = "btnstop.png";                     
                    ExposureLabel.Opacity = 0.3;
                    FocusLabel.Opacity = 0.3;
                    ExposureButton.Opacity = 0.3;
                    FocusButton.Opacity = 0.3;
                }
            }
            if(e.PropertyName =="FocusOn")
            {
                if (context.FocusOn)
                {
                    FocusButton.Style = (Style)Application.Current.Resources["FocusButtonOn"];
                    FocusLabel.Style = (Style)Application.Current.Resources["LabelOn"];
                }
                else
                {
                    FocusButton.Style = (Style)Application.Current.Resources["FocusButtonOff"];
                    FocusLabel.Style = (Style)Application.Current.Resources["LabelOff"];
                }
            }

            if(e.PropertyName == "ExposureOn")
            {
                if (context.ExposureOn)
                {
                    ExposureButton.Style = (Style)Application.Current.Resources["ExposureButtonOn"];
                    ExposureLabel.Style = (Style)Application.Current.Resources["LabelOn"];
                }
                else
                {
                    ExposureButton.Style = (Style)Application.Current.Resources["ExposureButtonOff"];
                    ExposureLabel.Style = (Style)Application.Current.Resources["LabelOff"];
                }
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

        public void TakePhoto(object sender, System.EventArgs e)
        {
            cameraView.TakePhoto();
        }
    }
}
