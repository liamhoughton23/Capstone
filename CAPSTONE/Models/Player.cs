using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class Player
    {
        [Key]
        public int PlayerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CoachID { get; set; }
        [ForeignKey("CoachID")]
        public  virtual Coach Coach { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public string PhoneNumber { get; set; }

        public string Code { get; set; }

        public string Email { get; set; }

        public bool Member { get; set; }



    }
}