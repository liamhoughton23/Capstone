using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPSTONE.HelperClasses
{
    public class DefenseHelpers
    {
       public decimal FPCT(int PO, int assists, int errors)
        {
            decimal percentage = (PO + assists) / (PO + assists + errors);
            return percentage;
        }

        public int TotalChances(int assists, int putOuts, int errors)
        {
            int total = assists + putOuts + errors;
            return total;
        }
    }
}