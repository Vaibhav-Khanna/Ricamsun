using System;
using Java.IO;
using Java.Lang;
using Android.Hardware.Camera2;
using WeldingMask.Droid.Abstractions;
using WeldingMask.Droid.Renderers;

namespace WeldingMask.Droid.Listeners
{
    public class CameraCaptureListener : CameraCaptureSession.CaptureCallback
    {
        public Camera2BasicFragment Owner { get; set; }
        public File File { get; set; }

        public override void OnCaptureCompleted(CameraCaptureSession session, CaptureRequest request, TotalCaptureResult result)
        {
            Process(result);
        }

        public override void OnCaptureProgressed(CameraCaptureSession session, CaptureRequest request, CaptureResult partialResult)
        {
            Process(partialResult);
        }

        private void Process(CaptureResult result)
        {
            Owner?.CaptureStillPicture();
        }
    }
}
