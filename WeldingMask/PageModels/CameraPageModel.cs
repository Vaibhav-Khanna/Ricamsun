using System;
using WeldingMask.PageModels.Base;
using Xamarin.Forms;

namespace WeldingMask.PageModels
{
    public class CameraPageModel : BasePageModel
    {
       
        public Command ScreenTap => new Command(() =>
       {
           
       });


        bool control_visible = true;
        public bool IsControlVisible
        {
            get { return control_visible; }
            set
            {
                control_visible = value;


                RaisePropertyChanged();
            }
        }


        double opacity;
        public double FilterOpacity
        {
            get { return opacity; }
            set
            {
                opacity = value;

                RaisePropertyChanged();
            }
        }




        public override void Init(object initData)
        {
            base.Init(initData);

            FilterOpacity = 0.2;
           
        }


    }
}
