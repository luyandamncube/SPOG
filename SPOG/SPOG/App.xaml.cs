using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using System.Net.Http.Headers;

using Xamarin.Forms;

namespace SPOG
{
    public partial class App : Application
    {
        public static IPublicClientApplication PCA = null;
        public static string AppId = "3bddfad4-aff8-48e7-96c4-4c4fa23b3263";
        public static string[] AppScopes = { "User.Read", "Mail.Read", "Mail.Send", "Files.ReadWrite", "People.Read" };
        public static object ParentWindow { get; set; }
        public static bool PendingAuth = false;

        public static GraphServiceClient GraphClient;

        public App ()
        {

            InitializeComponent();
            PCA = PublicClientApplicationBuilder.Create(AppId)
                .WithRedirectUri($"msal{AppId}://auth")
                .Build();

            GraphClient = new GraphServiceClient(new DelegateAuthenticationProvider(
                async (request) =>
                {
                    IEnumerable<IAccount> accounts = await PCA.GetAccountsAsync();
                    // Get token silently from MSAL
                    IAccount firstAccount = accounts.FirstOrDefault();
                    var authResult = await PCA.AcquireTokenSilent(AppScopes, firstAccount)
                                              .ExecuteAsync();
                    // Add the access token to the "Authorization" header
                    request.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
                }
            ));

            MainPage = new SignInPage();
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}
