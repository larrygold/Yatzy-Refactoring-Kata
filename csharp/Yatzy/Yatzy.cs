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

        public static int GetScoreOnePair(int d1, int d2, int d3, int d4, int d5)
        {
            if(IsApplicable(d1, d2, d3, d4, d5, 2, 1))
                return GetScoreGroupsOf(d1, d2, d3, d4, d5, 2, 1);
            return 0;
        }

        public static int GetScoreTwoPairs(int d1, int d2, int d3, int d4, int d5)
        {
            if (IsApplicable(d1, d2, d3, d4, d5, 2, 2))
                return GetScoreGroupsOf(d1, d2, d3, d4, d5, 2, 2);

            return 0;
        }

        public static int FourOfAKind(int d1, int d2, int d3, int d4, int d5)
        {
            if (IsApplicable(d1, d2, d3, d4, d5, 4, 1))
                return GetScoreGroupsOf(d1, d2, d3, d4, d5, 4, 1);

            return 0;
        }

        public static int ThreeOfAKind(int d1, int d2, int d3, int d4, int d5)
        {
            if (IsApplicable(d1, d2, d3, d4, d5, 3, 1))
                return GetScoreGroupsOf(d1, d2, d3, d4, d5, 3, 1);
            
            return 0;
        }

        public static int SmallStraight(int d1, int d2, int d3, int d4, int d5)
        {
            return GetScoreStraight(d1, d2, d3, d4, d5, 1, 5);
        }

        public static int LargeStraight(int d1, int d2, int d3, int d4, int d5)
        {
            return GetScoreStraight(d1, d2, d3, d4, d5, 2, 6);
        }

        public static int FullHouse(int d1, int d2, int d3, int d4, int d5)
        {
            if (IsApplicable(d1, d2, d3, d4, d5, 2, 1)
                && IsApplicable(d1, d2, d3, d4, d5, 3, 1)
                ) 
                return GetScoreOnePair(d1, d2, d3, d4, d5) + ThreeOfAKind(d1, d2, d3, d4, d5);

            return 0;

            /*
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
        */
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

        private static int GetScoreGroupsOf(int d1, int d2, int d3, int d4, int d5,
            int numberElementsInGroup, int numberGroups)
        {
            var dice = CreateDiceArray(d1, d2, d3, d4, d5);
            var numberDiceWithValue = CountDiceOccurrences(dice);
            return GetScoreGroupsOf(numberDiceWithValue, numberElementsInGroup, numberGroups);
        }

        private static int GetScoreGroupsOf(int[] numberDiceWithValue, int numberElementsInGroup, int numberGroups)
        {
            var pairs = GetDescendingGroupsOf(numberDiceWithValue, numberElementsInGroup);
            return pairs.Take(numberGroups).Sum(x => numberElementsInGroup * x);
        }

        private static List<int> GetDescendingGroupsOf(int[] numberDiceWithValue, int numberElementsInGroup)
        {
            var groups = new List<int>();

            for (var dieValue = 6; dieValue >= 1; dieValue--)
            {
                if (numberDiceWithValue[dieValue] >= numberElementsInGroup)
                {
                    groups.Add(dieValue);
                }
            }

            return groups;
        }

        private static int GetScoreStraight(int d1, int d2, int d3, int d4, int d5, int lowerDieValue, int higherDieValue)
        {
            var dice = CreateDiceArray(d1, d2, d3, d4, d5);
            var numberDiceWithValue = CountDiceOccurrences(dice);
            var sum = 0;

            for (var dieValue = lowerDieValue; dieValue <= higherDieValue; dieValue++)
            {
                if (numberDiceWithValue[dieValue] != 1)
                    return 0;

                sum += dieValue;
            }

            return sum;
        }

        private static bool IsApplicable(int d1, int d2, int d3, int d4, int d5, int numberElementsInGroup, int minimumNumberGroups)
        {
            var dice = CreateDiceArray(d1, d2, d3, d4, d5);
            var numberDiceWithValue = CountDiceOccurrences(dice);
            var pairsByDescendingValue = GetDescendingGroupsOf(numberDiceWithValue, numberElementsInGroup);
            return pairsByDescendingValue.Count() >= minimumNumberGroups;
        }
    }
}