using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class SchedFund
    {
        [Key]
        public int ID { get; set; }

        public DayOfWeek Date { get; set; }

        public string Description { get; set; }

        
    }
}