﻿using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.SpeechRecognition;
using System;
using System.Diagnostics;
using System.Windows.Input;
using WeldingMask.PageModels.Base;
using WeldingMask.Pages;
using WeldingMask.Services;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using WeldingMask.PageModels;
using System.Reactive;

namespace WeldingMask.PageModels
{
    public class ModeEclipsePageModel : BasePageModel
    {

        IDisposable listener;
        IDisposable an;
        bool IsVisible = true;

        public ModeEclipsePageModel()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                if (Device.RuntimePlatform == Device.iOS && CrossSpeechRecognition.Current.IsSupported)
                    listener = CrossSpeechRecognition.Current.ListenUntilPause().Subscribe(ExecuteVoiceCommand, OnError);
                else if (CrossSpeechRecognition.Current.IsSupported)
                    listener = CrossSpeechRecognition.Current.ContinuousDictation().Subscribe(ExecuteVoiceCommand, OnError);


                an = CrossSpeechRecognition.Current.WhenListeningStatusChanged().Subscribe(async isListening =>
                {
                    if (isListening)
                    {
                        SpeechText = "Listening...";

                    }
                    else
                    {
                        SpeechText = "Getting ready...";

                        if (IsVisible)
                        {
                            listener?.Dispose();

                            await Task.Delay(2000);

                            if (Device.RuntimePlatform == Device.iOS && CrossSpeechRecognition.Current.IsSupported)
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    listener = CrossSpeechRecognition.Current.ListenUntilPause().Subscribe(ExecuteVoiceCommand, OnError);
                                });
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    listener = CrossSpeechRecognition.Current.ContinuousDictation().Subscribe(ExecuteVoiceCommand, OnError);
                                });
                            }
                        }
                    }

                });
            }
        }

        private void OnError(Exception obj)   
        {
           
        }

        void ExecuteVoiceCommand(string phrase)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(phrase))
                {
                    SpeechText = $"''{phrase}''";

                    if (phrase.ToLower() == "go")
                    {
                        ShieldOn = true;

                        ExposureTap.Execute(null);
                    }
                    else if (phrase.ToLower() == "stop")
                    {
                        ShieldOn = false;
                    }
                    else if (phrase.ToLower().Trim() == "bright")
                    {
                        exposurevalue += 25;

                        RaisePropertyChanged("ExposureValue");

                        if (exposurevalue > 100)
                            ExposureValue = 100;
                    }
                    else if (phrase.ToLower().Trim() == "dark")
                    {
                        exposurevalue -= 25;

                        RaisePropertyChanged("ExposureValue");

                        if (exposurevalue < 0)
                            ExposureValue = 0;
                    }
                    else if (phrase.ToLower().Trim() == "photo")
                    {
                        (this.CurrentPage as ModeEclipsePage)?.TakePhoto(null, null);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

       

        public Command ShieldTap => new Command(() =>
       {
           ShieldOn = !ShieldOn;

           ExposureTap.Execute(null);
       });


        public Command FocusTap => new Command(() =>
        {
            if (ShieldOn)
                FocusOn = !FocusOn;
        });

        public Command ExposureTap => new Command(() =>
        {
            if (ShieldOn)
                ExposureOn = !ExposureOn;
        });

        string _speechtext = "Listening...";
        public string SpeechText { get { return _speechtext; } set { _speechtext = value; RaisePropertyChanged(); } }


        private int slidervalue;
        public int SliderValue
        {
            get { return slidervalue; }
            set
            {
                slidervalue = value;

                if (FocusOn)
                {
                    FocusValue = slidervalue;
                }
                else if (ExposureOn)
                {
                    //if (slidervalue >= 50)
                    //{
                    //    OverOpacity = 0;
                    //}
                    //else
                    //{
                    //    double factor = ((double)2 * slidervalue / 100);
                    //    OverOpacity = factor * (-0.7) + 0.7;
                    //}

                    ExposureValue = slidervalue;
                }

                RaisePropertyChanged();
            }
        }

        private int zoomvalue = 0;
        public int ZoomValue
        {
            get { return zoomvalue; }
            set
            {
                zoomvalue = value;

                RaisePropertyChanged();
            }
        }

        //double overopacity = 0;
        //public double OverOpacity
        //{
        //    get { return overopacity; }
        //    set
        //    {
        //        overopacity = value;
        //        RaisePropertyChanged();
        //    }
        //}

        private int focusvalue = 80;
        public int FocusValue
        {
            get { return focusvalue; }
            set
            {
                focusvalue = value;
                RaisePropertyChanged();
            }
        }

        private int exposurevalue = 25;
        public int ExposureValue
        {
            get { return exposurevalue; }
            set
            {
                exposurevalue = value;
                RaisePropertyChanged();
            }
        }

        private bool enableSlider;
        public bool EnableSlider
        {
            get { return enableSlider; }
            set
            {
                enableSlider = value;
                RaisePropertyChanged();
            }
        }

        private bool _exposureOn;
        public bool ExposureOn
        {
            get { return _exposureOn; }
            set
            {
                _exposureOn = value;

                if (_exposureOn)
                {
                    FocusOn = false;
                    EnableSlider = true;
                    SliderValue = ExposureValue;
                }
                else
                {
                    EnableSlider = false;
                }

                RaisePropertyChanged();
            }
        }

        private bool _shieldOn;
        public bool ShieldOn
        {
            get { return _shieldOn; }
            set
            {
                _shieldOn = value;
                RaisePropertyChanged();

                if (!_shieldOn)
                {
                    EnableSlider = false;
                    FocusOn = false;
                    ExposureOn = false;
                }
            }
        }

        private bool _focusOn;
        public bool FocusOn
        {
            get { return _focusOn; }
            set
            {
                _focusOn = value;

                if (_focusOn)
                {
                    ExposureOn = false;
                    EnableSlider = true;
                    SliderValue = FocusValue;
                }
                else
                {
                    EnableSlider = false;
                }

                RaisePropertyChanged();
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PopPageModel();
                });
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            ShieldOn = false;
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            IsVisible = false;
            listener?.Dispose();
            an?.Dispose();

            base.ViewIsDisappearing(sender, e);
        }
    }

   
}
