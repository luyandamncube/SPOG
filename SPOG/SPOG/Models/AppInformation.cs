using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace SPOG.Models
{
    public class AppInformation
    {
        public string AppName { get { return AppInfo.Name; } }
        public string AppBuild { get { return AppInfo.BuildString; } }
        public string PackageName { get { return AppInfo.PackageName; } }
        public string AppVersion { get { return AppInfo.VersionString; } }
    }
}
