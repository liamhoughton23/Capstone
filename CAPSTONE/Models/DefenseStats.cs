﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class DefenseStats
    {
        [Key]
        public int Key { get; set; }

        public int PlayerID { get; set; }
        [ForeignKey("PlayerID")]
        public virtual Player Player { get; set; }

        public int Positions { get; set; }

        public int Games { get; set; }

        public int IP { get; set; }

        public int TC { get; set; }

        public int PO { get; set; }

        public int Assists { get; set; }

        public int Errors { get; set; }

        public int DoublePlays { get; set; }

        public float FPCT { get; set; }


    }
}