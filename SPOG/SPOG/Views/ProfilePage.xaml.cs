using SPOGModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPOG.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

        }
        protected override async void OnAppearing()
        {
            var photoStream = await UserModel.GetUserPhoto();
            UserModel.imageSource = ImageSource.FromStream(() => photoStream);
            avatar.Source = UserModel.imageSource;
            lblJobTitle.Text = UserModel.data.JobTitle;
            lblUsername.Text = UserModel.data.DisplayName;
            //lblOfficeLocation.Text = UserModel.data.OfficeLocation;
            lblEmail.Text = UserModel.data.Mail;
        }
    }
}

