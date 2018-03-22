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

        public float EarnedRunAvereage { get; set; }

        public float OpponentBattingAverage { get; set; }

        public float WHIP { get; set; }

        public int StrikeOuts { get; set; }

        public float StrikeOutPercentage { get; set; }

        public float PickOffPercentage { get; set; }

        public float HitBatterRatio { get; set; }

        public float WalksPerAtBat { get; set; }

        public float WalksPerInning { get; set; }

        public float HRratio { get; set; }

        public float StrikeOutRatio { get; set; }

        public float StrikeOutPerWalkRatio { get; set; }

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