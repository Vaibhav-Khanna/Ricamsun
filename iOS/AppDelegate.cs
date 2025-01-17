﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace WeldingMask.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            InvokeOnMainThread(() =>
            {
                UIApplication.SharedApplication.IdleTimerDisabled = true;
            });

            return base.FinishedLaunching(app, options);
        }


    }
}
