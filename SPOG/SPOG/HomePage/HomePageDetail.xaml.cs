using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPOGModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPOG.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageDetail : ContentPage
    {
        public ImageSource source = UserModel.imageSource;
        public HomePageDetail()
        {
            InitializeComponent();
            
        }
    }
}