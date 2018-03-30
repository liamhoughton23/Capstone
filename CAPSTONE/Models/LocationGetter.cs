using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class LocationGetter
    {
        [Key]
        public int DestinationID { get; set; }

        public string Location { get; set; }

        public string Destination { get; set; }
    }

}