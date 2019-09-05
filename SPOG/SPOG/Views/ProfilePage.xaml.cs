using SPOGModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace SPOG.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {

            InitializeComponent();
            var Location = UserModel.data.OfficeLocation;
            map.InitialCameraUpdate = CameraUpdateFactory.NewCameraPosition(
              new CameraPosition(
              new Position(-26.041274, 28.022734),  // latlng
              15d, // zoom
              0d, // rotation
              0d)); // tilt
            //Add pin for user location
            Pin userOfficeLocation = new Pin()
            {
                Type = PinType.Place,
                Label = "Britehouse",
                Address = Location,
                Position = new Position(-26.041365, 28.022372)
            };
            map.Pins.Add(userOfficeLocation);

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

