using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class DefenseStats
    {
        public int ID { get; set; }
        [ForeignKey("Player")]
        public virtual Player PlayerID { get; set; }

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