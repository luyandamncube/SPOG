using Newtonsoft.Json;
using SPOG.Models;
using SPOGModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace SPOG.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public OfficeList ObjList = new OfficeList();
        public string Location { get; set; } = null;
        public double LocationLatitude { get; set; } = 0;
        public double LocationLongitude { get; set; } = 0;
        public ProfilePage()
        {
            InitializeComponent();
            Location = UserModel.data.OfficeLocation;
            map.InitialCameraUpdate = CameraUpdateFactory.NewCameraPosition(
                new CameraPosition(
                new Xamarin.Forms.GoogleMaps.Position(-26.041274, 28.022734),  // latlng
                15d, // zoom
                0d, // rotation
                0d)); // tilt
        }

        public async Task getLocationAsync(string LocationName)
        {
            try
            {
                var locations = await Geocoding.GetLocationsAsync(LocationName);

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
                // pin for user location
                Pin userOfficeLocation = new Pin()
                {
                    Type = PinType.Place,
                    Label = "My Office",
                    Address = Location,
                    Position = new Xamarin.Forms.GoogleMaps.Position(location.Latitude, location.Longitude)
                };

                map.Pins.Add(userOfficeLocation);

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }
        }
        protected override async void OnAppearing()
        {
            GetJsonData(Location);
            await getLocationAsync(Location);
            var photoStream = await UserModel.GetUserPhoto();
            UserModel.imageSource = ImageSource.FromStream(() => photoStream);
            avatar.Source = UserModel.imageSource;
            lblJobTitle.Text = UserModel.data.JobTitle;
            lblUsername.Text = UserModel.data.DisplayName;
            lblEmail.Text = UserModel.data.Mail;
        }
        void GetJsonData(string Location)
        {
            string jsonFileName = "Data.offices.json";
            var assembly = typeof(ProfilePage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            using (var reader = new System.IO.StreamReader(stream))
            {
                var jsonString = reader.ReadToEnd();

                //Converting JSON Array Objects into generic list    
                ObjList = JsonConvert.DeserializeObject<OfficeList>(jsonString);
            }
            //Binding listview with json string     
            //listviewContacts.ItemsSource = ObjContactList.offices;
        }
    }
}

