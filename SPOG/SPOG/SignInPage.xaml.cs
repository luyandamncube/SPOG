﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPOG
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        private bool isRunning = false;
        public bool IsRunning 
        {
            get { return isRunning; }
            set 
            {
                isRunning = value;
                slSignIn.IsVisible = !value;
                spinner.IsVisible = value;
                spinner.IsRunning = value;
            }
        }

        public SignInPage ()
        {
            InitializeComponent ();
        }

        protected override async void OnAppearing()
        {
            AuthenticationResult result = null;
            IEnumerable<IAccount> accounts = await App.PCA.GetAccountsAsync();
            if (!App.PendingAuth)
            {
                try
                {

                    // Try to *silently* get a token
                    // Silent here means without prompting the user to login.
                    // This will only work if we have a previously cached token
                    IAccount firstAccount = accounts.FirstOrDefault();
                    result = await App.PCA.AcquireTokenSilent(App.AppScopes, firstAccount)
                                              .ExecuteAsync();

                    // Since we're already logged in, proceed to main page
                    await Navigation.PushModalAsync(new NavigationPage(new MainPage()), true);
                }
                catch (MsalUiRequiredException) { }
            }
        }

        async void SignIn(object sender, EventArgs e)
        {
            try
            {
                IsRunning = true;
                App.PendingAuth = true;
                //var result = await App.PCA.AcquireTokenAsync(App.AppScopes, App.AuthUiParent);
                var result = await App.PCA.AcquireTokenInteractive(App.AppScopes)
                                                      .WithParentActivityOrWindow(App.ParentWindow)
                                                      .ExecuteAsync();
                IsRunning = false;
                App.PendingAuth = false;
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()), true);
            }
            catch (MsalException ex)
            {
                IsRunning = false;
                App.PendingAuth = false;
                await DisplayAlert("Signin Error", ex.Message, "Dismiss");
            }
        }
    }
}