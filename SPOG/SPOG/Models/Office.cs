using System;
using System.Collections.Generic;
using System.Text;

namespace SPOG.Models
{
    public class Position
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
    public class Office
    {
        public string name { get; set; }
        public string type { get; set; }
        public string street { get; set; }
        public string location { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string tel { get; set; }
        public string postalCode { get; set; }
        public string key { get; set; }
        
    }

    public class OfficeList
    {
        public List<Office> offices { get; set; }
    }
}
