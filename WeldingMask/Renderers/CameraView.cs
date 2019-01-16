using System;
using Xamarin.Forms;

namespace WeldingMask.Renderers
{
    public class CameraView : View
    {
      
        public static readonly BindableProperty ExposureEnableProperty = BindableProperty.Create("ExposureEnable", typeof(bool), typeof(CameraView), false);
        public bool ExposureEnable
        {
            get { return (bool)GetValue(ExposureEnableProperty); }
            set { SetValue(ExposureEnableProperty, value); }
        }

        public static readonly BindableProperty FocusEnableProperty = BindableProperty.Create("FocusEnable", typeof(bool), typeof(CameraView), false);

        public bool FocusEnable
        {
            get { return (bool)GetValue(FocusEnableProperty); }
            set { SetValue(FocusEnableProperty, value); }
        }

        public static readonly BindableProperty FocusValueProperty = BindableProperty.Create("FocusValue", typeof(int), typeof(CameraView), 50);
        public int FocusValue
        {
            get { return (int)GetValue(FocusValueProperty); }
            set { SetValue(FocusValueProperty, value); }
        }

        public static readonly BindableProperty ExposureValueProperty = BindableProperty.Create("ExposureValue", typeof(int), typeof(CameraView), 50);
        public int ExposureValue
        {
            get { return (int)GetValue(ExposureValueProperty); }
            set { SetValue(ExposureValueProperty, value); }
        }

        public static readonly BindableProperty ZoomValueProperty = BindableProperty.Create("ZoomValue", typeof(int), typeof(CameraView), 0 );
        public double ZoomValue
        {
            get { return (int)GetValue(ZoomValueProperty); }
            set { SetValue(ZoomValueProperty, value); }
        }

        public delegate void EventHandler();

        public event EventHandler CapturePhoto;

        public void TakePhoto()
        {
            CapturePhoto?.Invoke();   
        }

    }
}
