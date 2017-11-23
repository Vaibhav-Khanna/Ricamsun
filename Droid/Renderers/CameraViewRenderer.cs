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

[assembly: ExportRenderer(typeof(CameraView), typeof(CameraViewRenderer))]
namespace WeldingMask.Droid.Renderers
{
	/*
     * Display Camera Stream: http://developer.xamarin.com/recipes/android/other_ux/textureview/display_a_stream_from_the_camera/
     * Camera Rotation: http://stackoverflow.com/questions/3841122/android-camera-preview-is-sideways
     */
    public class CameraViewRenderer : ViewRenderer<CameraView,Android.Views.View>, TextureView.ISurfaceTextureListener
	{
		global::Android.Hardware.Camera camera;		

		Activity activity;
		CameraFacing cameraType;
		TextureView textureView;
		SurfaceTexture surfaceTexture;
		global::Android.Views.View view;

		bool flashOn;

		byte[] imageBytes;

        public CameraViewRenderer()
		{

		}

        protected override void OnElementChanged(ElementChangedEventArgs<CameraView> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || Element == null)
				return;

			try
			{
				activity = this.Context as Activity;

                view = activity.LayoutInflater.Inflate(Resource.Layout.CameraLayout, this, false);
				cameraType = CameraFacing.Back;

                textureView = view.FindViewById<TextureView>(Resource.Id.textureView);
				textureView.SurfaceTextureListener = this;
                				
                SetNativeControl(view);
			}
			catch (Exception)
			{
				
			}
		}

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			base.OnLayout(changed, l, t, r, b);

			var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
			var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

			view.Measure(msw, msh);
			view.Layout(0, 0, r - l, b - t);
		}

		public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
		{
            try
            {
                camera = global::Android.Hardware.Camera.Open((int)cameraType);

                var param = camera.GetParameters();

                param.FocusMode = (Android.Hardware.Camera.Parameters.FocusModeContinuousPicture);
                param.AutoExposureLock = true;
                param.WhiteBalance = (Android.Hardware.Camera.Parameters.WhiteBalanceAuto);
                camera.SetParameters(param);

                textureView.LayoutParameters = new FrameLayout.LayoutParams(width, height);
                surfaceTexture = surface;

                camera.SetPreviewTexture(surface);
                PrepareAndStartCamera();
            }catch(Exception)
            {
                CrossPermissions.Current.OpenAppSettings();
            }
		}

		public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
		{
			camera.StopPreview();
			camera.Release();

			return true;
		}

		public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
		{
			PrepareAndStartCamera();
		}

		public void OnSurfaceTextureUpdated(SurfaceTexture surface)
		{

		}

		private void PrepareAndStartCamera()
		{
			camera.StopPreview();

			var display = activity.WindowManager.DefaultDisplay;

			if (display.Rotation == SurfaceOrientation.Rotation0)
			{
				camera.SetDisplayOrientation(90);
			}

			if (display.Rotation == SurfaceOrientation.Rotation270)
			{
				camera.SetDisplayOrientation(180);
			}

			camera.StartPreview();
		}

		

	}
}