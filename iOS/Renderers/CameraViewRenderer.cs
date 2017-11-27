using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using AVFoundation;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;
using Foundation;
using WeldingMask.Pages;
using WeldingMask.iOS.Renderers;
using System.Collections.Generic;
using WeldingMask.Renderers;
using Photos;
using PhotosUI;
using MaterialControls;

[assembly: ExportRenderer(typeof(CameraView), typeof(CameraViewRenderer))]
namespace WeldingMask.iOS.Renderers
{
    public class CameraViewRenderer : ViewRenderer<CameraView,UIView>
    {

        AVCaptureSession captureSession;
        AVCaptureDeviceInput captureDeviceInput;
        AVCaptureStillImageOutput stillImageOutput;
        AVCaptureDevice device;
        UIView liveCameraStream;
        float maxExposure;
        float minExposure;
           

        protected async override void OnElementChanged(ElementChangedEventArgs<CameraView> e)
        {
            base.OnElementChanged(e);
			
            if (e.OldElement != null || this.Element == null)
				return;
			
            SetupUserInterface();

			await AuthorizeCameraUse();
			
            SetupLiveCameraStream();

            Element.CapturePhoto -= Element_CapturePhoto;
            Element.CapturePhoto += Element_CapturePhoto;

            if (Element.ExposureEnable)
                AdjustExposure(Element.ExposureValue);
            else
                SetAutoExposure();

            if (Element.FocusEnable)
                AdjustFocus(Element.FocusValue);
            else
                SetAutoFocus();

        }

        async void Element_CapturePhoto()
        {
            await CapturePhoto();
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Element == null || Control == null)
                return;

            if (e.PropertyName == CameraView.FocusValueProperty.PropertyName){

                AdjustFocus(Element.FocusValue);
            }

            if(e.PropertyName == CameraView.ExposureValueProperty.PropertyName)
            {
                AdjustExposure(Element.ExposureValue);
            }

            if(e.PropertyName == CameraView.ExposureEnableProperty.PropertyName)
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

        }

        private void SetupUserInterface()
        {
            liveCameraStream = new UIView();
            SetNativeControl(liveCameraStream);
        }


        public AVCaptureDevice GetCameraForOrientation(AVCaptureDevicePosition orientation)
        {
            var devices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);

            foreach (var devi in devices)
            {
                if (devi.Position == orientation)
                {
                    return devi;
                }
            }
            return null;
        }


        public async Task CapturePhoto()
        {           
            var videoConnection = stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);
            var sampleBuffer = await stillImageOutput.CaptureStillImageTaskAsync(videoConnection);

   
            var jpegImageAsNsData = AVCaptureStillImageOutput.JpegStillToNSData(sampleBuffer);


            var image = new UIImage(jpegImageAsNsData);
           
            image.SaveToPhotosAlbum(null);

            var toast = new MDToast("Une photo est disponible dans votre bibliothèque", 2);
            toast.Show();
        }

        public void SetupLiveCameraStream()
        {
            captureSession = new AVCaptureSession();

            var videoPreviewLayer = new AVCaptureVideoPreviewLayer(captureSession)
            {
                Frame = liveCameraStream.Bounds
            };
            liveCameraStream.Layer.AddSublayer(videoPreviewLayer);

            var captureDevice = AVCaptureDevice.GetDefaultDevice(AVMediaType.Video);
            device = captureDevice;
            maxExposure = device.MaxExposureTargetBias;
            minExposure = device.MinExposureTargetBias;
            ConfigureCameraForDevice(captureDevice);
            captureDeviceInput = AVCaptureDeviceInput.FromDevice(captureDevice);

            var dictionary = new NSMutableDictionary();
            dictionary[AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.JPEG);
            stillImageOutput = new AVCaptureStillImageOutput()
            {
                OutputSettings = new NSDictionary()
            };

            captureSession.AddOutput(stillImageOutput);
            captureSession.AddInput(captureDeviceInput);
            captureSession.StartRunning();
        }
     

        public void ConfigureCameraForDevice(AVCaptureDevice device)
        {
            var error = new NSError();
           
            if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
            {
                device.LockForConfiguration(out error);
                device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
				device.UnlockForConfiguration();
            }
            if (device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
            {
                device.LockForConfiguration(out error);
                device.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
                device.UnlockForConfiguration();
            }
           	 
            AdjustExposure(50);
        }


        public void SetAutoFocus()
        {
            var error = new NSError();

            if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
            {
                device.LockForConfiguration(out error);
                device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
                device.UnlockForConfiguration();
            }
        }

        public void SetAutoExposure()
        {            
            var error = new NSError();

            if (device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
            {
                device.LockForConfiguration(out error);
                device.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
                device.UnlockForConfiguration();
            }

            AdjustExposure(50);
        }


        public async void AdjustFocus(int F)
        {
            if (F < 0 || F > 100)
                return;

            var error = new NSError();

            if (device.LockingFocusWithCustomLensPositionSupported)
            {
                device.LockForConfiguration(out error);
                await device.SetFocusModeLockedAsync((float)F/100);
                device.UnlockForConfiguration();
            }
        }


        public async void AdjustExposure(int E)
        {
            if (E < 0 || E > 100)
                return;
            
            var error = new NSError();

            float range = Math.Abs(maxExposure - minExposure);

            float factor = ((float)E / 100);

            var targetExposure = factor * range  + minExposure;

            if (device.LockingFocusWithCustomLensPositionSupported)
            {
                device.LockForConfiguration(out error);
                await device.SetExposureTargetBiasAsync((targetExposure));
                device.UnlockForConfiguration();
            }
        }


        public async Task AuthorizeCameraUse()
        {
            var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);

            if (authorizationStatus != AVAuthorizationStatus.Authorized)
            {
                await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
            }
            else
            {
                await Task.Delay(1000);
            }
        }
       
      
    }

}
