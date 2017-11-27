using System;
using Android.Hardware.Camera2;

namespace WeldingMask.Droid.Abstractions
{
    public interface ICameraViewRenderer
    {
        void CaptureStillPicture();

        void RunPrecaptureSequence();

        void OpenCamera(int width, int height);

        void ConfigureTransform(int widht, int height);

        void ShowToast(string text);

        void SetAutoFlash(CaptureRequest.Builder request);

        void UnlockFocus();

        void CreateCameraPreviewSession();
    }
}
