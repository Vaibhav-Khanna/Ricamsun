using System;
using System.Windows.Input;
using WeldingMask.PageModels.Base;
using Xamarin.Forms;

namespace WeldingMask.PageModels
{
    public class DisclaimerPageModel : BasePageModel
    {
        public DisclaimerPageModel()
        {
        }
        public ICommand ContinueCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<ModesPageModel>(null);
                });
            }
        }
    }
}
