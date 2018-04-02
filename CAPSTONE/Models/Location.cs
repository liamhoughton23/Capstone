using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }

        public string Locations { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Destination { get; set; }

        public string Latitude2 { get; set; }

        public string Longitude2 { get; set; }
    }

}