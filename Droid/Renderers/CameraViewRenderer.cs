using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Permissions;
using WeldingMask.Droid.Renderers;
using WeldingMask.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Hardware.Camera2;
using Java.Util.Concurrent;
using Java.Lang;
using System.Collections.Generic;
using Android.Media;
using Android.Util;
using WeldingMask.Droid.Abstractions;
using WeldingMask.Droid.Listeners;
using System.Threading.Tasks;
using Android.Hardware.Camera2.Params;
using Java.Util;
using Size = Android.Util.Size;
using Boolean = Java.Lang.Boolean;
using Math = Java.Lang.Math;
using Orientation = Android.Content.Res.Orientation;

[assembly: ExportRenderer(typeof(CameraView), typeof(CameraViewRenderer))]
namespace WeldingMask.Droid.Renderers
{
    /*
     * Display Camera Stream: http://developer.xamarin.com/recipes/android/other_ux/textureview/display_a_stream_from_the_camera/
     * Camera Rotation: http://stackoverflow.com/questions/3841122/android-camera-preview-is-sideways
     */
    public class CameraViewRenderer : ViewRenderer<CameraView, Android.Views.View>
    {
        Android.Views.View view;

        Camera2BasicFragment fragment;

        public CameraViewRenderer(Context context) : base(context)
        {

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (fragment != null)
            {
                //Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.FragmentManager.BeginTransaction().Remove(fragment).Commit();
                fragment.OnPause();
                fragment = null;
            }

        }

        protected override void OnElementChanged(ElementChangedEventArgs<CameraView> e)
        {
            base.OnElementChanged(e);

            try
            {
                if (e.OldElement != null || this.Element == null)
                {
                    if (fragment != null)
                        fragment.OnPause();
                    return;
                }

                Element.CapturePhoto -= Element_CapturePhoto;
                Element.CapturePhoto += Element_CapturePhoto;

                var activity = this.Context as Activity;

                view = activity.LayoutInflater.Inflate(Resource.Layout.activity_camera, this, false);

                SetNativeControl(view);

                Delay();

            }
            catch (System.Exception)
            {

            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Element == null || Control == null || fragment==null)
                return;

            if (e.PropertyName == CameraView.FocusValueProperty.PropertyName)
            {
                AdjustFocus(Element.FocusValue);
            }

            if (e.PropertyName == CameraView.ExposureValueProperty.PropertyName)
            {
                AdjustExposure(Element.ExposureValue);
            }

            if (e.PropertyName == CameraView.ExposureEnableProperty.PropertyName)
            {
                if (Element.ExposureEnable)
                    AdjustExposure(Element.ExposureValue);
                else
                    SetAutoExposure();
            }

            if (e.PropertyName == CameraView.FocusEnableProperty.PropertyName)
            {
                if (Element.FocusEnable)
                    AdjustFocus(Element.FocusValue);
                else
                    SetAutoFocus();
            }

            if (e.PropertyName == CameraView.ZoomValueProperty.PropertyName)
            {
                HandleZoom(Element.ZoomValue);
            }
        }

        void Element_CapturePhoto()
        {
            fragment?.TakePicture();
        }

        void AdjustFocus(int focusValue)
        {
            if (focusValue < 0 || focusValue > 100)
                return;
            
            fragment?.AdjustFocus(focusValue);
        }

        void AdjustExposure(int ExposureValue)
        {
            if (ExposureValue < 0 || ExposureValue > 100)
                return;
            
            fragment?.AdjustExposure(ExposureValue);
        }

        void SetAutoExposure()
        {
            fragment?.SetAutoExposure();
        }

        void SetAutoFocus()
        {
            fragment?.SetAutoFocus();
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

            if (view != null)
            {
                view.Measure(msw, msh);
                view.Layout(0, 0, r - l, b - t);
            }
        }

        void HandleZoom(double value)
        {
            fragment?.AdjustZoom(Convert.ToInt32(value));
        }

        async void Delay()
        {
            await Task.Delay(800).ConfigureAwait(false);

            if (fragment != null)
                fragment.Dispose();

            fragment = Camera2BasicFragment.NewInstance();
                      
            (this.Context as Activity).FragmentManager.BeginTransaction().Replace(Resource.Id.container, fragment).Commit();


            if (Element.ExposureEnable)
                AdjustExposure(Element.ExposureValue);
            else
                SetAutoExposure();


            if (Element.FocusEnable)
                AdjustFocus(Element.FocusValue);
            else
                SetAutoFocus();
        }
    }
}