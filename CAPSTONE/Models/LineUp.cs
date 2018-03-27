using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class LineUp
    {
        [Key]
        public int ID { get; set; }

        public int Position { get; set; }

        public int PlayerID { get; set; }
        [ForeignKey("PlayerID")]
        public virtual Player Player { get; set; }
   




        //public int LeadOffPosition { get; set; }

        //public string SecondHitter { get; set; }
        //[ForeignKey("FirstName")]
        //public virtual Player PlayerTwo { get; set; }

        //public int TwoHitPosition { get; set; }

        //public string ThirdHitter { get; set; }
        //[ForeignKey("FirstName")]
        //public virtual Player PlayerThree { get; set; }

        //public int ThreePosition { get; set; }

        //public string FourHitter { get; set; }
        //[ForeignKey("FirstName")]
        //public virtual Player PlayerFour { get; set; }

        //public int Fourposition { get; set; }

        //public string FiveHitter { get; set; }
        //[ForeignKey("FirstName")]
        //public virtual Player PlayerFive { get; set; }

        //public int FivePosition { get; set; }

        //public string SixHitter { get; set; }
        //[ForeignKey("FirstName")]
        //public virtual Player PlayerSix { get; set; }

        //public int SixPosition { get; set; }

        //public string SevenHitter { get; set; }
        //[ForeignKey("FirstName")]
        //public virtual Player PlayerSeven { get; set; }

        //public int SevenPosition { get; set; }

        //public string EightHitter { get; set; }
        //[ForeignKey("FirstName")]
        //public virtual Player PlayerEight { get; set; }

        //public int EightPosition { get; set; }

        //public string NineHitter { get; set; }
        //[ForeignKey("FirstName")]
        //public virtual Player PlayerNine { get; set; }

        //public int NinePosition { get; set; }
    }
}