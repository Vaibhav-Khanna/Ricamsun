using System;
using System.Collections.Generic;
using WeldingMask.PageModels;
using Xamarin.Forms;

namespace WeldingMask.Pages
{
	public partial class ModeEclipsePage : ContentPage
	{
		public ModeEclipsePage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

        }
        
		void Shield_Tapped(object sender, System.EventArgs e)
		{
			if ((BindingContext as ModeEclipsePageModel).ShieldOn)
			{
				ShieldButton.Style = (Style)Application.Current.Resources["ShieldButtonOff"];
                PageContent.BackgroundColor = Color.White;
                CloseButton.Source = "closeblack.png";
            }
			else
			{
				ShieldButton.Style = (Style)Application.Current.Resources["ShieldButtonOn"];
                PageContent.BackgroundColor = Color.Black;
                CloseButton.Source = "close.png";
            }
			(BindingContext as ModeEclipsePageModel).ShieldOn = !(BindingContext as ModeEclipsePageModel).ShieldOn;

		}
		void Focus_Tapped(object sender, System.EventArgs e)
		{
			if ((BindingContext as ModeEclipsePageModel).FocusOn)
			{
				FocusButton.Style = (Style)Application.Current.Resources["FocusButtonOff"];
			}
			else
			{
				FocusButton.Style = (Style)Application.Current.Resources["FocusButtonOn"];
			}
			(BindingContext as ModeEclipsePageModel).FocusOn = !(BindingContext as ModeEclipsePageModel).FocusOn;
		}

		void Exposure_Tapped(object sender, System.EventArgs e)
		{
			if ((BindingContext as ModeEclipsePageModel).ExposureOn)
			{
				ExposureButton.Style = (Style)Application.Current.Resources["ExposureButtonOff"];
			}
			else
			{
				ExposureButton.Style = (Style)Application.Current.Resources["ExposureButtonOn"];
			}
			(BindingContext as ModeEclipsePageModel).ExposureOn = !(BindingContext as ModeEclipsePageModel).ExposureOn;
		}
	}
}
