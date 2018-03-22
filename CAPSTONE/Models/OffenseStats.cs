using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class OffenseStats
    {
        [Key]
        public int Key { get; set; }

        public int Player { get; set; }
        [ForeignKey("Player")]
        public virtual Player PlayerID { get; set; }

        public int TotalPlateAppearances { get; set; }

        public int OfficialAtBats { get; set; }

        public int TotalHits { get; set; }

        public float BA { get; set; }

        public float SLG { get; set; }

        public float TotalBases { get; set; }

        public float OBP { get; set; }

        public float BOBP { get; set; }

        public float SBP { get; set; }

        public float SOR { get; set; }

        public float RunsCreated { get; set; }


        //public int AtBats { get; set; }

        //public int Runs { get; set; }

        //public int Hits { get; set; }

        //public int FirstBase { get; set; }

        //public int SecondBase { get; set; }

        //public int ThirdBase { get; set; }

        //public int HR { get; set; }

        //public int RBI { get; set; }

        //public int Walks { get; set; }

        //public int StrikeOuts { get; set; }

        //public int StolenBases { get; set; }

        //public int CaughtStolenBases { get; set; }

        //public float BA { get; set; }

        //public float OBP { get; set; }

        //public float SLG { get; set; }

        //public float OPS { get; set; }

        //public float RC { get; set; }
    }
}