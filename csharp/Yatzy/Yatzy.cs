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

        public int GetScoreChance()
        {
            var dice = CreateDiceArray();
            return dice.Sum();
        }

        public int GetScoreYams()
        {
            var numberDiceWithValue = CountDiceOccurrences();
            foreach (var count in numberDiceWithValue)
                if (count == 5)
                    return 50;
            return 0;
        }

        public int GetScoreOnes()
        {
            return GetScoreForValue(1);
        }

        public int GetScoreTwos()
        {
            return GetScoreForValue(2);
        }

        public int GetScoreThrees()
        {
            return GetScoreForValue(3);
        }

        public int GetScoreFours()
        {
            return GetScoreForValue(4);
        }

        public int GetScoreFives()
        {
            return GetScoreForValue(5);
        }

        public int GetScoreSixes()
        {
            return GetScoreForValue(6);
        }

        public int GetScoreOnePair()
        {
            return GetScoreGroupsOf(2, 1);
        }

        public int GetScoreTwoPairs()
        {
            if (IsApplicable(2, 2))
                return GetScoreGroupsOf(2, 2);

            return 0;
        }

        public int GetScoreFourOfAKind()
        {
            return GetScoreGroupsOf(4, 1);
        }

        public int GetScoreThreeOfAKind()
        {
            return GetScoreGroupsOf(3, 1);
        }

        public int GetScoreSmallStraight()
        {
            return GetScoreStraight(1, 5);
        }

        public int GetScoreLargeStraight()
        {
            return GetScoreStraight(2, 6);
        }

        public int GetScoreFullHouse()
        {
            if (IsApplicable(2, 1)
                && IsApplicable(3, 1)
            )
                return GetScoreOnePair() + GetScoreThreeOfAKind();

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

        private int[] CountDiceOccurrences()
        {
            var counts = new int[7];
            foreach (var die in _dice)
                counts[die]++;
            return counts;
        }

        private int[] CreateDiceArray()
        {
            var dice = new[] {_dice[0], _dice[1], _dice[2], _dice[3], _dice[4]};
            return dice;
        }

        private int CountDiceWithValue(int value)
        {
            var count = 0;
            foreach (var die in _dice)
                if (die == value)
                    count++;

            return count;
        }

        private int GetScoreForValue(int value)
        {
            var dice = CreateDiceArray();
            return CountDiceWithValue(value) * value;
        }

        private int GetScoreGroupsOf(int numberElementsInGroup, int numberGroups)
        {
            var dice = CreateDiceArray();
            var numberDiceWithValue = CountDiceOccurrences();
            return GetScoreGroupsOf(numberDiceWithValue, numberElementsInGroup, numberGroups);
        }

        private int GetScoreGroupsOf(int[] numberDiceWithValue, int numberElementsInGroup, int numberGroups)
        {
            var pairs = GetDescendingGroupsOf(numberDiceWithValue, numberElementsInGroup);
            return pairs.Take(numberGroups).Sum(x => numberElementsInGroup * x);
        }

        private List<int> GetDescendingGroupsOf(int[] numberDiceWithValue, int numberElementsInGroup)
        {
            var groups = new List<int>();

            for (var dieValue = 6; dieValue >= 1; dieValue--)
                if (numberDiceWithValue[dieValue] >= numberElementsInGroup)
                    groups.Add(dieValue);

            return groups;
        }

        private int GetScoreStraight(int lowerDieValue,
            int higherDieValue)
        {
            var dice = CreateDiceArray();
            var numberDiceWithValue = CountDiceOccurrences();
            var sum = 0;

            for (var dieValue = lowerDieValue; dieValue <= higherDieValue; dieValue++)
            {
                if (numberDiceWithValue[dieValue] != 1)
                    return 0;

                sum += dieValue;
            }

            return sum;
        }

        private bool IsApplicable(int numberElementsInGroup,
            int minimumNumberGroups)
        {
            var groupsByDescendingValue = GetDescendingGroupsOf(numberElementsInGroup);
            return groupsByDescendingValue.Count() >= minimumNumberGroups;
        }

        private List<int> GetDescendingGroupsOf(int numberElementsInGroup)
        {
            var dice = CreateDiceArray();
            var numberDiceWithValue = CountDiceOccurrences();
            var pairsByDescendingValue = GetDescendingGroupsOf(numberDiceWithValue, numberElementsInGroup);
            return pairsByDescendingValue;
        }
    }
}