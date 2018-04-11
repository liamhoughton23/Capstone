using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class ContactPlayer
    {
        [Key]
        public int Key { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public int PlayerID { get; set; }
        [ForeignKey("PlayerID")]
        public virtual Player Player { get; set; }

        public string PhoneNumber { get; set; }
    }
}