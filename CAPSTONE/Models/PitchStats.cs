using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class PitchStats
    {
        [Key]
        public int Key { get; set; }

        public int PlayerID { get; set; }
        [ForeignKey("PlayerID")]
        public virtual Player Player { get; set; }

        public decimal EarnedRunAvereage { get; set; }

        public decimal OpponentBattingAverage { get; set; }

        public decimal WHIP { get; set; }

        public decimal StrikeOuts { get; set; }

        public decimal StrikeOutPercentage { get; set; }

        public decimal PickOffPercentage { get; set; }

        public decimal HitBatterRatio { get; set; }

        public decimal WalksPerAtBat { get; set; }

        public decimal WalksPerInning { get; set; }

        public decimal HRratio { get; set; }

        public decimal StrikeOutRatio { get; set; }

        public decimal StrikeOutPerWalkRatio { get; set; }

        //public int Wins { get; set; }

        //public int Loses { get; set; }

        //public int ERA { get; set; }

        //public int Games { get; set; }

        //public int GamesStarted { get; set; }

        //public int Saves { get; set; }

        //public int SaveOpportunities { get; set; }

        //public int IP { get; set; }

        //public int HitsAllowed { get; set; }

        //public int RunsAllowed { get; set; }

        //public int HRAllowed { get; set; }

        //public int BB { get; set; }

        //public int SO { get; set; }

        //public float WHIP { get; set; }

    }
}