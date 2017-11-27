using System;
using Android.Views;
using WeldingMask.Droid.Abstractions;

namespace WeldingMask.Droid.Listeners
{
    
    public class Camera2BasicSurfaceTextureListener : Java.Lang.Object, TextureView.ISurfaceTextureListener
    {
        public ICameraViewRenderer Owner { get; set; }

        public Camera2BasicSurfaceTextureListener(ICameraViewRenderer owner)
        {
            Owner = owner;
        }

        public void OnSurfaceTextureAvailable(Android.Graphics.SurfaceTexture surface, int width, int height)
        {
            Owner.OpenCamera(width, height);
        }

        public bool OnSurfaceTextureDestroyed(Android.Graphics.SurfaceTexture surface)
        {
            return true;
        }

        public void OnSurfaceTextureSizeChanged(Android.Graphics.SurfaceTexture surface, int width, int height)
        {
            Owner.ConfigureTransform(width, height);
        }

        public void OnSurfaceTextureUpdated(Android.Graphics.SurfaceTexture surface)
        {

        }
    }
}
