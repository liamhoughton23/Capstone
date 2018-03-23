using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class SubmitDefense
    {
        [Key]
        public int GameID { get; set; }
        public int PlayerID { get; set; }
        [ForeignKey("PlayerID")]
        public virtual Player Player { get; set; }

        public int Positions { get; set; }

        public int Attempts { get; set; }

        public int Errors { get; set; }

        public int InningsPlayed { get; set; }

        public int PutOuts { get; set; }

        public int Assists { get; set; }

    }
}