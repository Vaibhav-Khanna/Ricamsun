using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using WeldingMask.PageModels.Base;
using Xamarin.Forms;

namespace WeldingMask.PageModels
{
    public class ModesPageModel : BasePageModel
    {
        public ModesPageModel()
        {
        }

        public ICommand SoudureCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var result = await CheckPermissions();

                    if(result)
                    await CoreMethods.PushPageModel<ModeSoudurePageModel>(null);
                });
            }
        }

        public ICommand EclipseCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var result = await CheckPermissions();

                    if (result)
                    await CoreMethods.PushPageModel<ModeEclipsePageModel>(null);
                });
            }
        }

        private async Task<bool> CheckPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
               
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        await CoreMethods.DisplayAlert("Accès à la caméra", "L'application nécessite l'accès à la caméra", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Camera))
                        status = results[Permission.Camera];
                }

                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
                else
                {
                    var result = await CoreMethods.DisplayAlert("Accès à la caméra", "Vous avez refusé l'accès à la caméra. Nous ne pouvons continuer.", "Settings","Maybe Later");

                    if(result)
                    Plugin.Permissions.CrossPermissions.Current.OpenAppSettings();

                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
