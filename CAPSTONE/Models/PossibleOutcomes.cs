using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class PossibleOutcomes
    {
        [Key]
        public int Play { get; set; }

        public int Hit { get; set; }

        public int Single { get; set; }

        public int Double { get; set; }

        public int Triple { get; set; }

        public int HR { get; set; }

        public int BB { get; set; }

        public int HBP { get; set; }

        public int FC { get; set; }

        public int WildPitch { get; set; }

        public int CatchersInterference { get; set; }

        public int PassedBall { get; set; }

        public int GRD { get; set; }

        public int Error { get; set; }



    }
}