using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.Identity.Client;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.Droid;

using Plugin.CurrentActivity;
using SPOG.Services;
using Xamarin.Forms.GoogleMaps.Android;

namespace SPOG.Droid
{
    [Activity(Label = "SPOG", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            CrossCurrentActivity.Current.Init(this, bundle);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            //Google Maps config
            var platformConfig = new PlatformConfig
            {
                BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            };
            Xamarin.FormsGoogleMaps.Init(this, bundle, platformConfig); // initialize for Xamarin.Forms.GoogleMaps
            SetActionBar(toolbar);
            Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            LoadApplication(new App());
            App.ParentWindow = this;
            //Change Status bar color/theme to "Light"
            var statusBarStyleManager = DependencyService.Get<IStatusBarStyleManager>();
            statusBarStyleManager.SetLightTheme();
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.home, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}

