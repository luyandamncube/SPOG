using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Identity.Client;

using Foundation;
using UIKit;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.iOS;
using Xamarin.Forms.GoogleMaps.iOS;

namespace SPOG.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            ImageCircleRenderer.Init();

            //Google Maps config
            var platformConfig = new PlatformConfig
            {
                ImageFactory = new CachingImageFactory()
            };

            Xamarin.FormsGoogleMaps.Init("AIzaSyDMggkmREZaQbC-QXdsUJLmPBdDh16Ud9w", platformConfig);
            LoadApplication(new App());
            //App.AuthUiParent = new UIParent();
            App.ParentWindow = null;
            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            return AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url);
        }
    }
}
