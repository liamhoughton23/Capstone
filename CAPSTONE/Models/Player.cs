﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class Player
    {
        [Key]
        public int PlayerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CoachID { get; set; }
        [ForeignKey("CoachID")]
        public  virtual Coach Coach { get; set; }






    }
}