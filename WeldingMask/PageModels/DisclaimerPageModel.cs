using System;
using System.Windows.Input;
using FreshMvvm;
using WeldingMask.PageModels.Base;
using Xamarin.Forms;

namespace WeldingMask.PageModels
{
    public class DisclaimerPageModel : BasePageModel
    {
        
        private bool _shouldValidate;
        public bool ShouldValidate
        {
            get { return _shouldValidate; }
            set
            {
                _shouldValidate = value;
                RaisePropertyChanged();
            }
        }

       
        public ICommand ContinueCommand
        {
            get
            {
                return new Command( async() =>
                {
                    if (ShouldValidate)
                    {

                        if (App.Current.Properties.ContainsKey("DisclaimerAgree"))
                            App.Current.Properties["DisclaimerAgree"] = "true";
                        else
                            App.Current.Properties.Add("DisclaimerAgree", "true");

                        await App.Current.SavePropertiesAsync();

                        Device.BeginInvokeOnMainThread( () => 
                        {
                            var page = FreshPageModelResolver.ResolvePageModel<ModesPageModel>();
                            App.Current.MainPage = new FreshNavigationContainer(page) { BarTextColor = Color.White, BarBackgroundColor = Color.Black };
                        });
                    }
                });
            }
        }

        private bool _termsAccepted;
        public bool TermsAccepted
        {
            get { return _termsAccepted; }
            set
            {
                _termsAccepted = value;
                RaisePropertyChanged();
            }
        }

    }
}
