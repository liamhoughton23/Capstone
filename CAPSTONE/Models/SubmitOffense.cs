using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class SubmitOffense
    {
        [Key]
        public int Game { get; set; }

        public int PlayerID { get; set; }
        [ForeignKey("PlayerID")]
        public virtual Player Player { get; set; }

        public int CoachID { get; set; }

        public int PlateAppearances { get; set; }

        public int Singles { get; set; }

        public int Doubles { get; set; }

        public int Triples { get; set; }

        public int HRs { get; set; }

        public int TotalBases { get; set; }

        public int Walks { get; set; }

        public int HBP { get; set; }

        public int Scrifices { get; set; }

        public int OnByFeildersChoice { get; set; }

        public int OnByInterference { get; set; }

        public int DroppedThirdStrike { get; set; }

        public int StolenBases { get; set; }

        public int StolenBaseAttempts{ get; set; }

        public int SO { get; set; }

        public int OtherBattingOuts { get; set; }

        public int RBIs { get; set; }

        public int RunsScored { get; set; }



    }
}