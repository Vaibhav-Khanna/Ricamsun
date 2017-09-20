using System;
using Plugin.Permissions;
using WeldingMask.PageModels;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace WeldingMask.Pages
{
    public class PermissionsPage : ContentPage
    {
        public PermissionsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            AskPermission();
        }

        async void AskPermission()
        {
            var context = BindingContext as PermissionsPageModel;

            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Camera);


            if(status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    await context.CoreMethods.PushPageModel<CameraPageModel>(null, false, false);
                    context.CoreMethods.RemoveFromNavigation();
                }
                else if (Device.RuntimePlatform == Device.Android)
                    RequestPermission();              
            }
            else
            {
                RequestPermission();
            }

        }

        async void RequestPermission()
        {
            var context = BindingContext as PermissionsPageModel;

			var request_status = await CrossPermissions.Current.RequestPermissionsAsync((new List<Plugin.Permissions.Abstractions.Permission>() { Plugin.Permissions.Abstractions.Permission.Camera }).ToArray());

            if (request_status.First().Value == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
			{
                await context.CoreMethods.PushPageModel<CameraPageModel>(null, false, false);
				context.CoreMethods.RemoveFromNavigation();
			}
			else
			{
				var dresult = await DisplayAlert("Sorry", "The app wont continue until you enable camera usage permission. Would you like to enable it.", "Yes", "Quit");

				if (dresult)
				{
					CrossPermissions.Current.OpenAppSettings();
				}
				else
				{
					throw new Exception();
				}
			}
        }

    }
}

