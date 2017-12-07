using System;
using Android.Content.Res;
using Android.Graphics;
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
                Control.SetThumb(Resources.GetDrawable(Resource.Drawable.oval));
                Control.ProgressDrawable.SetColorFilter(
                       new PorterDuffColorFilter(
                        Xamarin.Forms.Color.FromHex("#f8e71c").ToAndroid(),
                       PorterDuff.Mode.SrcIn));

                Control.ProgressBackgroundTintList
           = ColorStateList.ValueOf(
                           Xamarin.Forms.Color.FromHex("#f8e71c").ToAndroid());
                Control.ProgressBackgroundTintMode
                       = PorterDuff.Mode.SrcOver;

                Control.ProgressTintList
         = ColorStateList.ValueOf(
                         Xamarin.Forms.Color.FromHex("#f8e71c").ToAndroid());
                Control.ProgressBackgroundTintMode
                       = PorterDuff.Mode.SrcIn;
            }
            //Control.SetThumbImage(UIImage.FromFile("oval.png"), UIControlState.Normal);
            //Control.MaximumTrackTintColor = UIColor.FromRGB(248, 231, 28);
            //Control.MinimumTrackTintColor = UIColor.FromRGB(248, 231, 28);
        }

    }
}
