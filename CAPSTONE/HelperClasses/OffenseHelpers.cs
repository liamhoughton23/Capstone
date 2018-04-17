using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPSTONE.HelperClasses
{
    public class OffenseHelpers
    {
        public int Appearances(int plateAppearances)
        {
            int totalPlateAppearances = plateAppearances;
            return totalPlateAppearances;
        }

        public int OfficialAtBatsCalculator(int totalAppearances, int walks, int HBP, int sacrifices)
        {
            int officialAtBats = totalAppearances - walks - HBP - sacrifices;
            return officialAtBats;
        }

        public int TotalHitsCalulator(int singles, int doubles, int triples, int homeRuns)
        {
            int totalHits = singles + doubles + triples + homeRuns;
            return totalHits;
        }

        public decimal BattingAverageCalculator(int totalHits, int atBats)
        {
            decimal battingAverage = (decimal)totalHits / atBats;
            decimal rounded = Math.Round(battingAverage, 3);
            return rounded; 
        }

        public decimal SluggingPercengateCalculator(int totalBases, int officialAtBats)
        {
                decimal sluggingPercentage = (decimal)totalBases / officialAtBats;
                decimal rounded = Math.Round(sluggingPercentage, 3);
                return rounded;
        }

        public decimal OnBasePercentageCalculator(int hits, int walks, int HBP, int onBaseInter, int onBaseDroppedThirdSt, int onBaseFC, int officialAtBats, int sacrifices)
        {
            try
            {
                decimal onBasePercentage = (hits + (decimal)walks + HBP + onBaseInter + onBaseDroppedThirdSt + onBaseInter + onBaseFC) / (officialAtBats + hits + walks + HBP + onBaseInter + onBaseDroppedThirdSt + onBaseFC + sacrifices);
                decimal rounded = Math.Round(onBasePercentage, 3);
                return rounded;
            }
            catch
            {
                return 0;
            }
        }

        public int TotalBasesCalc(int singles, int doubles, int triples, int homeRuns)       
        {
            int totalBases = (1 * singles) + (2 * doubles) + (3 * triples) + (4 * homeRuns);
            return totalBases;
        }

        public decimal BaseOnBallsPercentage(int walks, int totalPlateAppearances)
        {
            decimal percentage = (decimal)walks / totalPlateAppearances;
            decimal rounded = Math.Round(percentage, 3);
            return rounded;
        }

        public decimal StolenBasePercentage(int stolenBases, int stolenBaseAttempts)
        {
            if (stolenBaseAttempts != 0)
            {
                decimal percentage = (decimal)stolenBaseAttempts / stolenBases;
                decimal rounded = Math.Round(percentage, 3);
                return rounded;
            }
            else
            {
                decimal noStolenBases = 0;
                return noStolenBases;
            }
            
        }

        public decimal RunsCreatedCalcuator(int hits, int BB, int totalBases, int atBats)
        {
            decimal runCreated = (decimal)(hits + BB) * totalBases / atBats + BB;
            decimal rounded = Math.Round(runCreated, 3);
            return rounded;
        }

        public decimal StrikeOutPercentage(int officialAtBats, int strikeOuts)
        {
            decimal percentage = (decimal)strikeOuts / officialAtBats;
            decimal rounded = Math.Round(percentage, 3);
            return rounded;
        }

        
    }
}