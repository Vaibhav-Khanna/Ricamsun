using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.SpeechRecognition;
using WeldingMask.PageModels.Base;
using Xamarin.Forms;
using System.Reactive.Linq;

namespace WeldingMask.PageModels
{
    public class ModesPageModel : BasePageModel
    {
        public ModesPageModel()
        {
           
        }

        public Command ContinueCommand => new Command(() =>
        {
            Device.OpenUri(new Uri("http://ricamsun.com/cgu/"));
        });


        public ICommand SoudureCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var result = await CheckPermissionCamera();

                    var granted = await CrossSpeechRecognition.Current.RequestPermission();

                    if (result && granted == SpeechRecognizerStatus.Available)
                    await CoreMethods.PushPageModel<ModeSoudurePageModel>(null);
                });
            }
        }

        public Command VoiceCommand => new Command(async() =>
        {
           await CoreMethods.PushPageModel<VoiceCommandPageModel>();
        });

        public ICommand EclipseCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var result_C = await CheckPermissionCamera();
                    var result_P = await CheckPermissionPhoto();
                    var granted = await CrossSpeechRecognition.Current.RequestPermission();

                    if (result_C && result_P && granted == SpeechRecognizerStatus.Available)
                    await CoreMethods.PushPageModel<ModeEclipsePageModel>(null);
                });
            }
        }

        private async Task<bool> CheckPermissionCamera()
        {
            try
            {                
                //var Is_Available = Plugin.Media.CrossMedia.Current.IsCameraAvailable;

                //if (!Is_Available)
                //{
                //    await CoreMethods.DisplayAlert("Caméra", "Camera is not available on this device", "OK");
                //    return false;
                //}

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
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<bool> CheckPermissionPhoto()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);

                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Photos))
                    {
                        await CoreMethods.DisplayAlert("Accès à la photos", "L'application nécessite l'accès à la photos", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Photos);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Photos))
                        status = results[Permission.Photos];
                }

                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
                else
                {
                    var result = await CoreMethods.DisplayAlert("Accès à la photos", "Vous avez refusé l'accès à la photos. Nous ne pouvons continuer.", "Settings", "Maybe Later");

                    if (result)
                        Plugin.Permissions.CrossPermissions.Current.OpenAppSettings();

                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
