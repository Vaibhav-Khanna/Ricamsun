using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using Plugin.CurrentActivity;
using WeldingMask.Droid.Renderers;
using Android.Net;
using System.Globalization;
using System.IO;
using Android.Speech;
using Xamarin.Forms;
using WeldingMask.Services;

namespace WeldingMask.Droid
{
    [Activity(Label = "RicamSun", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity,Android.Media.MediaScannerConnection.IOnScanCompletedListener, ISpeechToText
    {

        private readonly int VOICE = 10;


        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {

            if (requestCode == VOICE)
            {
                if (resultCode == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                   
                    if (matches.Count != 0)
                    {
                        string textInput = matches[0];

                        MessagingCenter.Send<ISpeechToText, string>(this, "STT", textInput);
                    }
                    else
                    {
                        MessagingCenter.Send<ISpeechToText, string>(this, "STT", "No input");
                    }

                }
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnScanCompleted(string path, Android.Net.Uri uri)
        {
            
        }

        public static Android.Net.Uri GetOutputMediaFile(Context context, string subdir, string name)
        {
            subdir = subdir ?? string.Empty;

            if (string.IsNullOrWhiteSpace(name))
            {
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
               
                name = "IMG_" + timestamp + ".jpg";               
            }

            var mediaType =  Android.OS.Environment.DirectoryPictures;
            var directory = Android.OS.Environment.GetExternalStoragePublicDirectory(mediaType);
        
            using (var mediaStorageDir = new Java.IO.File(directory, subdir))
            {
                if (!mediaStorageDir.Exists())
                {
                    if (!mediaStorageDir.Mkdirs())
                    {
                        // permission not given for outpiut
                        return null;
                    }                           
                }
                return Android.Net.Uri.FromFile(new Java.IO.File(GetUniquePath(mediaStorageDir.Path, name, true)));
            }
        }

        private static string GetUniquePath(string folder, string name, bool isPhoto)
        {
            var ext = Path.GetExtension(name);
            if (ext == string.Empty)
                ext = ((isPhoto) ? ".jpg" : ".mp4");

            name = Path.GetFileNameWithoutExtension(name);

            var nname = name + ext;
            var i = 1;
            while (File.Exists(Path.Combine(folder, nname)))
                nname = name + "_" + (i++) + ext;

            return Path.Combine(folder, nname);
        }

        public void StartSpeechToText()
        {
            throw new NotImplementedException();
        }

        public void StopSpeechToText()
        {
            throw new NotImplementedException();
        }
    }
}
