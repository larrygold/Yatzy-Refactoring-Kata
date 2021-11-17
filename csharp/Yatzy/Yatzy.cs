using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
    public class Yatzy
    {
        private readonly int[] _dice;

        public Yatzy(int d1, int d2, int d3, int d4, int d5)
        {
            _dice = new[] {d1, d2, d3, d4, d5};
        }

        public static int GetScoreChance(int d1, int d2, int d3, int d4, int d5)
        {
            var dice = CreateDiceArray(d1, d2, d3, d4, d5);
            return dice.Sum();
        }

        public static int GetScoreYams(params int[] dice)
        {
            var numberDiceWithValue = CountDiceOccurrences(dice);
            foreach (var count in numberDiceWithValue)
            {
                if (count == 5)
                    return 50;
            }
            return 0;
        }

        public static int GetScoreOnes(int d1, int d2, int d3, int d4, int d5)
        {
            return GetScoreForValue(d1, d2, d3, d4, d5, 1);
        }

        public static int GetScoreTwos(int d1, int d2, int d3, int d4, int d5)
        {
            return GetScoreForValue(d1, d2, d3, d4, d5, 2);
        }

        public static int GetScoreThrees(int d1, int d2, int d3, int d4, int d5)
        {
            return GetScoreForValue(d1, d2, d3, d4, d5, 3);
        }

        public int GetScoreFours()
        {
            return GetScoreForValue(_dice[0], _dice[1], _dice[2], _dice[3], _dice[4], 4);
        }

        public int GetScoreFives()
        {
            return GetScoreForValue(_dice[0], _dice[1], _dice[2], _dice[3], _dice[4], 5);
        }

        public int GetScoreSixes()
        {
            return GetScoreForValue(_dice[0], _dice[1], _dice[2], _dice[3], _dice[4], 6);
        }

        public static int GetScorePair(int d1, int d2, int d3, int d4, int d5)
        {
            return GetScorePairs(d1, d2, d3, d4, d5, 1);
        }

        public static int TwoPair(int d1, int d2, int d3, int d4, int d5)
        {
            return GetScorePairs(d1, d2, d3, d4, d5, 2);
        }

        private static int GetScorePairs(int[] numberDiceWithValue, int numberPairs)
        {
            var pairs = GetPairsDescending(numberDiceWithValue);
            return pairs.Take(numberPairs).Sum(x => 2 * x);
        }

        private static List<int> GetPairsDescending(int[] numberDiceWithValue)
        {
            var pairs = new List<int>();

            for (var dieValue = 6; dieValue >= 1; dieValue--)
            {
                if (numberDiceWithValue[dieValue] >= 2)
                {
                    pairs.Add(dieValue);
                }
            }

            return pairs;
        }

        public static int FourOfAKind(int _1, int _2, int d3, int d4, int d5)
        {
            int[] tallies;
            tallies = new int[6];
            tallies[_1 - 1]++;
            tallies[_2 - 1]++;
            tallies[d3 - 1]++;
            tallies[d4 - 1]++;
            tallies[d5 - 1]++;
            for (var i = 0; i < 6; i++)
                if (tallies[i] >= 4)
                    return (i + 1) * 4;
            return 0;
        }

        public static int ThreeOfAKind(int d1, int d2, int d3, int d4, int d5)
        {
            int[] t;
            t = new int[6];
            t[d1 - 1]++;
            t[d2 - 1]++;
            t[d3 - 1]++;
            t[d4 - 1]++;
            t[d5 - 1]++;
            for (var i = 0; i < 6; i++)
                if (t[i] >= 3)
                    return (i + 1) * 3;
            return 0;
        }

        public static int SmallStraight(int d1, int d2, int d3, int d4, int d5)
        {
            int[] tallies;
            tallies = new int[6];
            tallies[d1 - 1] += 1;
            tallies[d2 - 1] += 1;
            tallies[d3 - 1] += 1;
            tallies[d4 - 1] += 1;
            tallies[d5 - 1] += 1;
            if (tallies[0] == 1 &&
                tallies[1] == 1 &&
                tallies[2] == 1 &&
                tallies[3] == 1 &&
                tallies[4] == 1)
                return 15;
            return 0;
        }

        public static int LargeStraight(int d1, int d2, int d3, int d4, int d5)
        {
            int[] tallies;
            tallies = new int[6];
            tallies[d1 - 1] += 1;
            tallies[d2 - 1] += 1;
            tallies[d3 - 1] += 1;
            tallies[d4 - 1] += 1;
            tallies[d5 - 1] += 1;
            if (tallies[1] == 1 &&
                tallies[2] == 1 &&
                tallies[3] == 1 &&
                tallies[4] == 1
                && tallies[5] == 1)
                return 20;
            return 0;
        }

        public static int FullHouse(int d1, int d2, int d3, int d4, int d5)
        {
            int[] tallies;
            var _2 = false;
            int i;
            var _2_at = 0;
            var _3 = false;
            var _3_at = 0;


            tallies = new int[6];
            tallies[d1 - 1] += 1;
            tallies[d2 - 1] += 1;
            tallies[d3 - 1] += 1;
            tallies[d4 - 1] += 1;
            tallies[d5 - 1] += 1;

            for (i = 0; i != 6; i += 1)
                if (tallies[i] == 2)
                {
                    _2 = true;
                    _2_at = i + 1;
                }

            for (i = 0; i != 6; i += 1)
                if (tallies[i] == 3)
                {
                    _3 = true;
                    _3_at = i + 1;
                }

            if (_2 && _3)
                return _2_at * 2 + _3_at * 3;
            return 0;
        }

        private static int[] CountDiceOccurrences(int[] dice)
        {
            var counts = new int[7];
            foreach (var die in dice)
                counts[die]++;
            return counts;
        }

        private static int[] CreateDiceArray(int d1, int d2, int d3, int d4, int d5)
        {
            var dice = new[] {d1, d2, d3, d4, d5};
            return dice;
        }

        private static int CountDiceWithValue(int[] dice, int value)
        {
            var count = 0;
            foreach (var die in dice)
            {
                if (die == value)
                    count++;
            }

            return count;
        }

        private static int GetScoreForValue(int d1, int d2, int d3, int d4, int d5, int dieValue)
        {
            var dice = CreateDiceArray(d1, d2, d3, d4, d5);
            return CountDiceWithValue(dice, dieValue) * dieValue;
        }

        private static int GetScorePairs(int d1, int d2, int d3, int d4, int d5, int numberPairs)
        {
            var dice = CreateDiceArray(d1, d2, d3, d4, d5);
            var numberDiceWithValue = CountDiceOccurrences(dice);
            return GetScorePairs(numberDiceWithValue, numberPairs);
        }
    }
}