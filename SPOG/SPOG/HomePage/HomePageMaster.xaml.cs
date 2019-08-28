using SPOG.Views;
using SPOGModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPOG.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageMaster : ContentPage
    {
        public ListView ListView;

        public HomePageMaster()
        {
            InitializeComponent();

            BindingContext = new HomePageMasterViewModel();
            ListView = MenuItemsListView;
            avatar.Source = UserModel.imageSource;
            //lblUserEmail.Text = UserModel.data.Mail;
            lblUserName.Text = "Hi " + UserModel.data.GivenName + "!";
        }
        async void OnUserTapped(object sender, EventArgs e)
        {
            var signout = await DisplayAlert("Sign out?", "Do you want to sign out?", "Yes", "No");
            if (signout)
            {
                SignOut();
            }
        }
        async void OnPhotoTapped(object sender, EventArgs e)
        {
            //await DisplayAlert("Photo tapped", "TODO: Implement this!", "Got it");
            await Navigation.PushModalAsync(new ProfilePage(), true);
        }
        async void SignOut()
        {
            var accounts = await App.PCA.GetAccountsAsync();
            foreach (var account in accounts)
            {
                await App.PCA.RemoveAsync(account);
            }

            // Show the signin UI
            await Navigation.PushModalAsync(new SignInPage(), true);
        }
        class HomePageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<HomePageMasterMenuItem> MenuItems { get; set; }

            public HomePageMasterViewModel()
            {
                MenuItems = new ObservableCollection<HomePageMasterMenuItem>(new[]
                {
                    new HomePageMasterMenuItem { Id = 0, Title = "Calendar", TargetType=typeof(CalendarPage) },
                    new HomePageMasterMenuItem { Id = 1, Title = "Email Sender" , TargetType=typeof(EmailPage)},
                    new HomePageMasterMenuItem { Id = 2, Title = "About", TargetType=typeof(AboutPage)},

                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}