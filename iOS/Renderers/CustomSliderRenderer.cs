using System;
using UIKit;
using WeldingMask.Custom;
using WeldingMask.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(CustomSlider),typeof(CustomSliderRenderer))]
namespace WeldingMask.iOS.Renderers
{
    public class CustomSliderRenderer : SliderRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Slider> e)
        {
            base.OnElementChanged(e);
            if(Control!=null)
            {
                Control.SetThumbImage(UIImage.FromFile("oval.png"),UIControlState.Normal);
                Control.MaximumTrackTintColor = UIColor.FromRGB(248,231,28);
                Control.MinimumTrackTintColor = UIColor.FromRGB(248, 231, 28);
            }
        }
    }
}
