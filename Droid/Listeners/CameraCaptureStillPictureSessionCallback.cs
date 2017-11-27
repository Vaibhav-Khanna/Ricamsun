using System;
using Android.Hardware.Camera2;
using WeldingMask.Droid.Renderers;

namespace WeldingMask.Droid.Listeners
{
    public class CameraCaptureStillPictureSessionCallback : CameraCaptureSession.CaptureCallback
    {
        private static readonly string TAG = "CameraCaptureStillPictureSessionCallback";

        public CameraViewRenderer Owner { get; set; }

        public CameraCaptureStillPictureSessionCallback(CameraViewRenderer owner)
        {
            Owner = owner;
        }

        public override void OnCaptureCompleted(CameraCaptureSession session, CaptureRequest request, TotalCaptureResult result)
        {
            Owner.ShowToast("Saved: " + Owner.mFile);

            Owner.UnlockFocus();
        }
    }
}
