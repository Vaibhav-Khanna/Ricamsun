using System;
using WeldingMask.Custom;
using WeldingMask.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(CustomSlider),typeof(CustomSliderRenderer))]
namespace WeldingMask.Droid.Renderers
{
    public class CustomSliderRenderer : SliderRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Slider> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //Control.SetThumb(Resource.Drawable.oval);
                //Control.SetThumbImage(UIImage.FromFile("oval.png"), UIControlState.Normal);
                //Control.MaximumTrackTintColor = UIColor.FromRGB(248, 231, 28);
                //Control.MinimumTrackTintColor = UIColor.FromRGB(248, 231, 28);
            }
        }
    }
}
