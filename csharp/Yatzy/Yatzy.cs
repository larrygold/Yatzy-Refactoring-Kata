using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
    public class Dice
    {
        private int[] _dice;

        public Dice(int die1, int die2, int die3, int die4, int die5)
        {
            Die1 = die1;
            Die2 = die2;
            Die3 = die3;
            Die4 = die4;
            Die5 = die5;
            _dice = new[] {Die1, Die2, Die3, Die4, Die5};
        }

        public int Die1 { get; private set; }
        public int Die2 { get; private set; }
        public int Die3 { get; private set; }
        public int Die4 { get; private set; }
        public int Die5 { get; private set; }

        public int GetSum()
        {
            return _dice.Sum();
        }

        public int[] CountOccurrences()
        {
            var counts = new int[7];
            foreach (var die in _dice)
                counts[die]++;
            return counts;
        }

        public int CountForValue(int value)
        {
            var count = 0;
            foreach (var die in _dice)
                if (die == value)
                    count++;

            return count;
        }


    }

    public class Yatzy
    {
        private readonly int[] _diceArr;
        private Dice _dice;
        private readonly int[] _numberDiceWithValue;

        public Yatzy(Dice dice)
        {
            _diceArr = new[] {dice.Die1, dice.Die2, dice.Die3, dice.Die4, dice.Die5};
            _dice = dice;
            _numberDiceWithValue = _dice.CountOccurrences();
        }

        public int GetScoreChance()
        {
            return _dice.GetSum();
        }

        public int GetScoreYams()
        {
            foreach (var count in _numberDiceWithValue)
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

        public int GetScoreThreeOfAKind()
        {
            return GetScoreGroupsOf(3, 1);
        }

        public int GetScoreFourOfAKind()
        {
            return GetScoreGroupsOf(4, 1);
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
        }

        private int GetScoreForValue(int value)
        {
            return _dice.CountForValue(value) * value;
        }

        private int GetScoreGroupsOf(int numberElementsInGroup, int numberGroups)
        {
            var pairs = GetDescendingGroupsOf(numberElementsInGroup);
            return pairs.Take(numberGroups).Sum(x => numberElementsInGroup * x);
        }

        private List<int> GetDescendingGroupsOf(int numberElementsInGroup)
        {
            var groups = new List<int>();

            for (var dieValue = 6; dieValue >= 1; dieValue--)
                if (_numberDiceWithValue[dieValue] >= numberElementsInGroup)
                    groups.Add(dieValue);

            return groups;
        }

        private int GetScoreStraight(int lowerDieValue,
            int higherDieValue)
        {
            var sum = 0;

            for (var dieValue = lowerDieValue; dieValue <= higherDieValue; dieValue++)
            {
                if (_numberDiceWithValue[dieValue] != 1)
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

    }
}