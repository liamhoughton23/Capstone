using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class SubmitDefense
    {
        [Key]
        public int GameID { get; set; }

        public int Singles { get; set; }

        public int Doubles { get; set; }

        public int Triples { get; set; }

        public int HomeRuns { get; set; }

        public int Walks { get; set; }

        public int HBP { get; set; }

        public int Sacrifices { get; set; }

        public int FieldChoice { get; set; }

        public int Interference { get; set; }

        public int DroppedThirdStrike { get; set; }

        public int StolenBases { get; set; }

        public int SBattempts { get; set; }

        public int StrikeOuts { get; set; }

        public int OtherBattingOuts { get; set; }

        public int RBIs { get; set; }

        public int RunsScored { get; set; }





    }
}