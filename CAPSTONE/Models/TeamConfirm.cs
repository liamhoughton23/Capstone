using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CAPSTONE.Models
{
    public class TeamConfirm
    {
            [Key]
            public int Key { get; set; }

            //[Required]
            //[DataType(DataType.Password)]
            //[Display(Name = "TeamCode")]
            public string Code { get; set; }
    }
}