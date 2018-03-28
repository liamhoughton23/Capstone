﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPSTONE.HelperClasses
{
    public class PitchingHelpers
    {
        public decimal ERAcalc(int earnedRuns, int InningsPitched)
        {
            decimal era = (decimal)(earnedRuns * 9) / InningsPitched;
            decimal rounded = Math.Round(era, 3);
            return rounded;
        }

        public decimal OpponentBA(int hits, int officialAB)
        {
            decimal BA = (decimal)hits / officialAB;
            decimal rounded = Math.Round(BA, 3);
            return rounded;
        }

        public decimal SimpleDivision(int officialAB, int walks)
        {
            decimal percentage = (decimal)officialAB / walks;
            decimal rounded = Math.Round(percentage, 3);
            return rounded;
        }

        public decimal WHIP(int walks, int hits, int IP)
        {
            decimal percentage = (decimal)(walks + hits) / IP;
            decimal rounded = Math.Round(percentage, 3);
            return rounded;
        }
    }
}