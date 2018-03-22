using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class SubmitPitching
    {
        [Key]
        public int GameID { get; set; }

        public int Player { get; set; }
        [ForeignKey("Player")]
        public virtual Player PlayerID { get; set; }

        public int OpponentOfficialAtBats { get; set; }

        public int OpponentHits { get; set; }

        public int EarnedRunsScored { get; set; }

        public int InningsPitched { get; set; }

        public int StrikeOuts { get; set; }

        public int HomeRuns { get; set; }

        public int Walks { get; set; }

        public int BattersHBP { get; set; }

        public int PickOffAttempts { get; set; }

        public int PickOffs { get; set; }

    }
}