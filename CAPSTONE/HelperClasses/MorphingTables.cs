using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPSTONE.HelperClasses
{
    public class MorphingTables
    {
        public int AddingStats(int gameSingles, int totalSingles)
        {
            int total = gameSingles + totalSingles;
            return total;
        }
        public int TotalDoubles(int gameDoubles, int totalDoubles)
        {
            int total = gameDoubles + totalDoubles;
            return total;
        }

        public int TotalTriples(int gameTriples, int totalTriples)
        {
            int total = gameTriples + totalTriples;
            return total;
        }

        public int TotalHRs(int gameHRs, int totalHRs)
        {
            int total = gameHRs + totalHRs;
            return total;
        }

        public int RealTotalBases(int gameBases, int totalBases)
        {
            int total = gameBases + totalBases;
            return total;
        }

        public int TotalWalks(int gameWalks, int totalWalks)
        {
            int total = gameWalks + totalWalks;
            return total;
        }

        public int TotalHBP(int gameHBP, int totalHBP)
        {
            int total = gameHBP + totalHBP;
            return total;
        }

        public int TotalSacrifices(int gameSacrifices, int totalSacrifices)
        {
            int total = gameSacrifices + totalSacrifices;
            return total;
        }
        public int TotalOnByFC(int gameFC, int totalFC)
        {
            int total = gameFC + totalFC;
            return total;
        }

        public int TotalOnByInter(int gameInter, int totalInter)
        {
            int total = gameInter + totalInter;
            return total;
        }

        public int TotalDroppedThirdStrike(int gameDrops, int totalDrops)
        {
            int total = gameDrops + totalDrops;
            return total;
        }

        public int TotalStolenBases(int gameStole, int totalStole)
        {
            int total = gameStole + totalStole;
            return total;

        }

        public int TotalAttemptedStolenBases(int attemptGame, int attemptTotal)
        {
            int total = attemptGame = attemptTotal;
            return total;
        }

        public int TotalStrikeOuts(int gameSO, int totalSO)
        {
            int total = gameSO + totalSO;
            return total;
        }

        public int OtherBattingOuts(int gameOther, int totalOther)
        {
            int total = gameOther + totalOther;
            return total;
        }

        public int TotalRBIs(int gameRBIs, int totalRBIs)
        {
            int total = gameRBIs + totalRBIs;
            return total;
        }

    }
}