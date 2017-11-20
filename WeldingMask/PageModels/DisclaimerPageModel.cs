using System;
using System.Windows.Input;
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
        public DisclaimerPageModel()
        {
        }
        public ICommand ContinueCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (ShouldValidate)
                        await CoreMethods.PushPageModel<ModesPageModel>(null);
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
