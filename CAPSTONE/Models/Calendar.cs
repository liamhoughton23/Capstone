using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class Calendar
    {
        [Key]
        public int EventID { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Color { get; set; }

        public bool IsFullDay { get; set; }
        


    }
}