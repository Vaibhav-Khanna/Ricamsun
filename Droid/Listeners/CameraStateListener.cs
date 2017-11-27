using System;
using Android.App;
using Android.Hardware.Camera2;
using WeldingMask.Droid.Renderers;

namespace WeldingMask.Droid.Listeners
{
    public class CameraStateListener : CameraDevice.StateCallback
    {
        public CameraViewRenderer owner;

        public override void OnOpened(CameraDevice camera)
        {
            // This method is called when the camera is opened.  We start camera preview here.
            owner.mCameraOpenCloseLock.Release();
            owner.mCameraDevice = camera;
            owner.CreateCameraPreviewSession();
        }

        public override void OnDisconnected(CameraDevice camera)
        {
            owner.mCameraOpenCloseLock.Release();
            camera.Close();
            owner.mCameraDevice = null;
        }

        public override void OnError(CameraDevice camera, CameraError error)
        {
            owner.mCameraOpenCloseLock.Release();
            camera.Close();
            owner.mCameraDevice = null;
            if (owner == null)
                return;

            Activity activity = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity;

            if (activity != null)
            {
                activity.Finish();
            }

        }
    }
}
