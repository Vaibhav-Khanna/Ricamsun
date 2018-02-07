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

        public string Text { get; set; } = "En aucun cas il ne faut observer le soleil ou effectuer des soudures à l’oeil nu." + Environment.NewLine + Environment.NewLine  + "Pour le soleil, il est conseillé de limiter à quelques minutes les observations et d’effectuer un temps de pause entre deux observations afin de reposer les yeux." + Environment.NewLine+ Environment.NewLine + "Si vous constater une diminution de l’acuité visuelle, consulter rapidement un médecin." + Environment.NewLine+ Environment.NewLine + "La société RICAMSUN décline toutes responsabilités en cas de dommages corporels ou matériels." + Environment.NewLine + Environment.NewLine+ "In no case you should observe the sun or perform welds without eyes protections." + Environment.NewLine + Environment.NewLine+ "For the sun, we advise to limit to a few minutes of observation. Do not forget to rest your eyes for a moment in between two observations." + Environment.NewLine+ Environment.NewLine + "If you notice a decrease in your visual acuity, consult a doctor quickly." + Environment.NewLine + Environment.NewLine+ "The RICAMSUN company declines any responsibility in case of injury or material damage.";


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
