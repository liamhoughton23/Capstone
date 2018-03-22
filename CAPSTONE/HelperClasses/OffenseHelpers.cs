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

        public float BattingAverageCalculator(int totalHits, int atBats)
        {
            float battingAverage = totalHits / atBats;
            return battingAverage; 
        }

        public float SluggingPercengateCalculator(int totalBases, int officialAtBats)
        {
            float sluggingPercentage = totalBases / officialAtBats;
            return sluggingPercentage;
        }

        public float OnBasePercentageCalculator(int hits, int walks, int HBP, int onBaseInter, int onBaseDroppedThirdSt, int onBaseFC, int officialAtBats, int sacrifices)
        {
            float onBasePercentage = (hits + walks + HBP + onBaseInter + onBaseDroppedThirdSt + onBaseInter + onBaseFC) / (officialAtBats + hits + walks + HBP + onBaseInter + onBaseDroppedThirdSt + onBaseFC + sacrifices);
            float rounded = (float)(Math.Round((double)onBasePercentage, 3));
            return rounded;
        }

        public int TotalBasesCalc(int singles, int doubles, int triples, int homeRuns)       
        {
            int totalBases = 1 * singles + 2 * doubles + 3 * triples + 4 * homeRuns;
            return totalBases;
        }

        public float BaseOnBallsPercentage(int walks, int totalPlateAppearances)
        {
            float percentage = walks / totalPlateAppearances;
            float rounded = (float)(Math.Round((double)percentage, 3));
            return rounded;
        }

        public float StolenBasePercentage(int stolenBases, int stolenBaseAttempts)
        {
            float percentage = stolenBases / stolenBaseAttempts;
            float rounded = (float)(Math.Round((double)percentage, 3));
            return rounded;
        }

        public float RunsCreatedCalcuator(int hits, int BB, int totalBases, int atBats)
        {
            float runCreated = (hits + BB) * totalBases / atBats + BB;
            float rounded = (float)(Math.Round((double)runCreated, 3));
            return rounded;
        }

        public float StrikeOutPercentage(int officialAtBats, int strikeOuts)
        {
            float percentage = officialAtBats / strikeOuts;
            float rounded = (float)(Math.Round((double)percentage, 3));
            return rounded;
        }

        
    }
}