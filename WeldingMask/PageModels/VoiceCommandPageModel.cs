using System;
using WeldingMask.PageModels.Base;
using Xamarin.Forms;

namespace WeldingMask.PageModels
{
    public class VoiceCommandPageModel : BasePageModel
    {
        public VoiceCommandPageModel()
        {
        }

        public Command BackCommand => new Command(async() =>
        {
            await CoreMethods.PopPageModel();
        });
    }
}
