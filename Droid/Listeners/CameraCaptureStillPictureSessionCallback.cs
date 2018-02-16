using System;
using Android.Content;
using Android.Hardware.Camera2;
using Android.Provider;
using Java.IO;
using WeldingMask.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using static Android.Provider.MediaStore;

namespace WeldingMask.Droid.Listeners
{
    public class CameraCaptureStillPictureSessionCallback : CameraCaptureSession.CaptureCallback
    {
        private static readonly string TAG = "CameraCaptureStillPictureSessionCallback";

        public Camera2BasicFragment Owner { get; set; }

        public CameraCaptureStillPictureSessionCallback(Camera2BasicFragment owner)
        {
            Owner = owner;
        }

        public override void OnCaptureCompleted(CameraCaptureSession session, CaptureRequest request, TotalCaptureResult result)
        {
            Owner.ShowToast("Saved: " + Owner.mFile);
          
            addImageToGallery(Owner.mFile, Owner.Context,Owner);

            Owner.UnlockFocus();
        }

        public static void addImageToGallery(File filePath, Context context, Camera2BasicFragment owner)
        {
            try
            {
                var fileName = System.IO.Path.GetFileName(filePath.Path);
               
                var publicUri = MainActivity.GetOutputMediaFile(context, null, null );

                if (publicUri == null)
                {
                    owner.ShowToast("File could not be saved. Please grant permission to write to external storage.");
                    return;
                }

                using (System.IO.Stream input = System.IO.File.OpenRead(filePath.Path))
               
                using (System.IO.Stream output = System.IO.File.Create(publicUri.Path))
                    input.CopyTo(output);

                var f = new Java.IO.File(publicUri.Path);

                Android.Media.MediaScannerConnection.ScanFile(context, new[] { f.AbsolutePath }, null, context as MainActivity );

                ContentValues values = new ContentValues();
                values.Put(MediaStore.Images.Media.InterfaceConsts.Title, System.IO.Path.GetFileNameWithoutExtension(f.AbsolutePath));
                values.Put(MediaStore.Images.Media.InterfaceConsts.Description, string.Empty);
                values.Put(MediaStore.Images.Media.InterfaceConsts.DateTaken, Java.Lang.JavaSystem.CurrentTimeMillis());
                values.Put(MediaStore.Images.ImageColumns.BucketId, f.ToString().ToLowerInvariant().GetHashCode());
                values.Put(MediaStore.Images.ImageColumns.BucketDisplayName, f.Name.ToLowerInvariant());
                values.Put("_data", f.AbsolutePath);

                var cr = context.ContentResolver;
                var uri = cr.Insert(MediaStore.Images.Media.ExternalContentUri, values);


                var contentUri = uri;
                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile, contentUri);
                mediaScanIntent.SetData(contentUri);
                context.SendBroadcast(mediaScanIntent);

            }
            catch (Exception)
            {
               // Console.WriteLine("Unable to save to scan file: " + ex1);
            }

        }

    }
}
