using System;
using Android.App;
using Android.Content;
using Android.Speech;
using Plugin.CurrentActivity;
using WeldingMask.Droid.Services;
using WeldingMask.Services;

[assembly: Xamarin.Forms.Dependency(typeof(SpeechToTextImplementation))]
namespace WeldingMask.Droid.Services
{
    public class SpeechToTextImplementation : ISpeechToText
    {
        private readonly int VOICE = 10;

        private Activity _activity;

        public SpeechToTextImplementation()
        {
            _activity = CrossCurrentActivity.Current.Activity;

        }

        public void StartSpeechToText()
        {
            StartRecordingAndRecognizing();
        }

        private void StartRecordingAndRecognizing()
        {
            string rec = global::Android.Content.PM.PackageManager.FeatureMicrophone;

            if (rec == "android.hardware.microphone")
            {
                var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
               
                voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 5000);
                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 5000);
                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
                voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
                voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);

                _activity.StartActivityForResult(voiceIntent, VOICE);
            }
        }


        public void StopSpeechToText()
        {

        }
    }

}
