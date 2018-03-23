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
            decimal battingAverage = (decimal)totalHits / (decimal)atBats;
            return battingAverage; 
        }

        public decimal SluggingPercengateCalculator(int totalBases, int officialAtBats)
        {
            decimal sluggingPercentage = (decimal)totalBases / (decimal)officialAtBats;
            return sluggingPercentage;
        }

        public decimal OnBasePercentageCalculator(int hits, int walks, int HBP, int onBaseInter, int onBaseDroppedThirdSt, int onBaseFC, int officialAtBats, int sacrifices)
        {
            decimal onBasePercentage = (decimal)hits + (decimal)walks + (decimal)HBP + (decimal)onBaseInter + (decimal)onBaseDroppedThirdSt + (decimal)onBaseInter + (decimal)onBaseFC / (decimal)officialAtBats + (decimal)hits + (decimal)walks + (decimal)HBP + (decimal)onBaseInter + (decimal)onBaseDroppedThirdSt + (decimal)onBaseFC + (decimal)sacrifices;
            decimal rounded = (decimal)(Math.Round((decimal)onBasePercentage, 3));
            return rounded;
        }

        public int TotalBasesCalc(int singles, int doubles, int triples, int homeRuns)       
        {
            int totalBases = 1 * singles + 2 * doubles + 3 * triples + 4 * homeRuns;
            return totalBases;
        }

        public decimal BaseOnBallsPercentage(int walks, int totalPlateAppearances)
        {
            decimal percentage = walks / totalPlateAppearances;
            decimal rounded = (decimal)(Math.Round((decimal)percentage, 3));
            return rounded;
        }

        public decimal StolenBasePercentage(int stolenBases, int stolenBaseAttempts)
        {
            if (stolenBaseAttempts != 0)
            {
                decimal percentage = stolenBases / stolenBaseAttempts;
                decimal rounded = (decimal)(Math.Round((decimal)percentage, 3));
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
            decimal runCreated = (hits + BB) * totalBases / atBats + BB;
            decimal rounded = (decimal)(Math.Round((decimal)runCreated, 3));
            return rounded;
        }

        public decimal StrikeOutPercentage(int officialAtBats, int strikeOuts)
        {
            decimal percentage = officialAtBats / strikeOuts;
            decimal rounded = (decimal)(Math.Round((decimal)percentage, 3));
            return rounded;
        }

        
    }
}