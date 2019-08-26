using Microsoft.Graph;
using Microsoft.Identity.Client;
using SPOG;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SPOGModels
{
    public class UserModel
    {
        public static User data { get; set; }
        public static ImageSource imageSource { get; set; }
        public static async Task<Stream> GetUserPhoto()
        {
            // Get the user's profile photo
            var photo = await App.GraphClient.Me.Photo.Content.Request().GetAsync();
            if (photo == null)
            {
                // Fallback on a placeholder image
                photo = Assembly.GetExecutingAssembly().GetManifestResourceStream("SPOG.no-profile-pic.png");
            }

            return photo;
        }

    }
}
