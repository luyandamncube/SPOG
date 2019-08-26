using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SPOGModels;

namespace SPOG
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public bool isRunning  = false;
        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                slSignIn.IsVisible = !value;
                loader.IsVisible = value;
            }
        }
        public SignInPage ()
        {
            InitializeComponent ();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            //await DisplayAlert("Loading", App.isRunning, "Dismiss");

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
                    var user = await App.GraphClient.Me.Request()
                            .Select("displayName,mail,jobTitle,mobilePhone,officeLocation")
                            .GetAsync();
                    UserModel.data = user;
                    await DisplayAlert("User", UserModel.data.DisplayName, "Dismiss");
                    // Get the user's profile photo
                    var photoStream = await UserModel.GetUserPhoto();
                    UserModel.imageSource = ImageSource.FromStream(() => photoStream);

                    // Since we're already logged in, proceed to main page
                    await Navigation.PushModalAsync(new HomePage.HomePage(), true);
                }
                catch (MsalUiRequiredException ex) {
                    await DisplayAlert("Signin Error", ex.Message, "Dismiss");

                }
            }
        }

        async void SignIn(object sender, EventArgs e)
        {

            try
            {
                IsRunning = true;
                App.PendingAuth = true;
                var result = await App.PCA.AcquireTokenInteractive(App.AppScopes)
                                                      .WithParentActivityOrWindow(App.ParentWindow)
                                                      .ExecuteAsync();

                var user = await App.GraphClient.Me.Request()
                            .Select("displayName,mail,jobTitle,mobilePhone,officeLocation")
                            .GetAsync();
                UserModel.data = user;
                await DisplayAlert("User", UserModel.data.DisplayName, "Dismiss");

                // Get the user's profile photo
                var photoStream = await UserModel.GetUserPhoto();
                UserModel.imageSource = ImageSource.FromStream(() => photoStream);

                IsRunning = false;
                App.PendingAuth = false;
                await Navigation.PushModalAsync(new HomePage.HomePage(), true);
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