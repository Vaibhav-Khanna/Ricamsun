using System;
using System.Windows.Input;
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
                    await CoreMethods.PushPageModel<ModeEclipsePageModel>(null);
                });
            }
        }
    }
}
