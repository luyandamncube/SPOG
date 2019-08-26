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
            lblUserEmail.Text = UserModel.data.Mail;
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
            await DisplayAlert("Photo tapped", "TODO: Implement this!", "Got it");
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
                    new HomePageMasterMenuItem { Id = 0, Title = "New Page", TargetType=typeof(NewPage) },
                    new HomePageMasterMenuItem { Id = 1, Title = "Email Sender" , TargetType=typeof(MainPage)},
                    new HomePageMasterMenuItem { Id = 2, Title = "Page 3" },
                    new HomePageMasterMenuItem { Id = 3, Title = "Page 4" },
                    new HomePageMasterMenuItem { Id = 4, Title = "About" },
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