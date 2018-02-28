using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class SchedMeet
    {
        [Key]
        public int ID { get; set; }

        public DayOfWeek GameDay { get; set; }
    }
}