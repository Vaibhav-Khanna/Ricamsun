using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Windows.Input;
using WeldingMask.PageModels.Base;
using Xamarin.Forms;

namespace WeldingMask.PageModels
{
    public class ModeEclipsePageModel : BasePageModel
    {
        public ModeEclipsePageModel()
        {
            ShieldOn = true;
			FocusOn = false;

            checkPermissions();
        }

        private bool _exposureOn;
        public bool ExposureOn
        {
            get { return _exposureOn; }
            set
            {
                _exposureOn = value;
                RaisePropertyChanged();
            }
        }

        private bool _shieldOn;
        public bool ShieldOn
        {
            get { return _shieldOn; }
            set
            {
                _shieldOn = value;
                RaisePropertyChanged();
            }
        }

        private bool _focusOn;
        public bool FocusOn
        {
            get { return _focusOn; }
            set
            {
                _focusOn = value;
                RaisePropertyChanged();
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PopPageModel();
                });
            }
        }


        private async void checkPermissions()
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
                    
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await CoreMethods.DisplayAlert("Accès à la caméra", "Vous avez refusé l'accès à la caméra. Nous ne pouvons continuer.", "OK");
                    await CoreMethods.PopPageModel();
                }
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}
