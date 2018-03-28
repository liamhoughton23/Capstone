using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPSTONE.HelperClasses
{
    public class DefenseHelpers
    {
       public decimal FPCT(decimal PO, decimal assists, decimal errors)
        {
            decimal percentage = (decimal)(PO + assists) / (PO + assists + errors);
            decimal rounded = Math.Round(percentage, 3);
            return rounded;
        }

        public int TotalChances(int assists, int putOuts, int errors)
        {
            int total = assists + putOuts + errors;
            return total;
        }
    }
}