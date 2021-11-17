using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
    public class ValueScorer : Score
    {
        private int _dieValue;
        public ValueScorer(Dice dice, int dieValue) : base(dice)
        {
            _dieValue = dieValue;
        }

        public override int Get()
        {
            return CountForValue(_dieValue) * _dieValue;
        }

        private int CountForValue(int value)
        {
            var count = 0;
            foreach (var die in _dice)
                if (die == value)
                    count++;

            return count;
        }

    }

    public abstract class Score
    {
        protected Dice _dice;
        public Score(Dice dice)
        {
            _dice = dice;
        }

        public abstract int Get();
    }

    public class Dice : IEnumerable<int>
    {
        private readonly List<int> _dice;

        public Dice(int die1, int die2, int die3, int die4, int die5)
        {
            _dice = new List<int> () {die1, die2, die3, die4, die5};
        }

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

        public List<int> GetDescendingGroups(int groupSize)
        {
            var groups = new List<int>();
            var numberDiceWithValue = CountOccurrences();

            for (var dieValue = 6; dieValue >= 1; dieValue--)
                if (numberDiceWithValue[dieValue] >= groupSize)
                    groups.Add(dieValue);

            return groups;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return _dice.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Yatzy
    {
        private readonly Dice _dice;

        public Yatzy(Dice dice)
        {
            _dice = dice;
        }

        public int GetScoreChance()
        {
            return _dice.GetSum();
        }

        public int GetScoreYams()
        {
            foreach (var count in _dice.CountOccurrences())
                if (count == 5)
                    return 50;
            return 0;
        }

        public int GetScoreOnes()
        {
            return new ValueScorer(_dice, 1).Get();
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
            var pairs = _dice.GetDescendingGroups(numberElementsInGroup);
            return pairs.Take(numberGroups).Sum(x => numberElementsInGroup * x);
        }

        private int GetScoreStraight(int lowerDieValue,
            int higherDieValue)
        {
            var sum = 0;

            for (var dieValue = lowerDieValue; dieValue <= higherDieValue; dieValue++)
            {
                if (_dice.CountOccurrences()[dieValue] != 1)
                    return 0;

                sum += dieValue;
            }

            return sum;
        }

        private bool IsApplicable(int numberElementsInGroup,
            int minimumNumberGroups)
        {
            var groupsByDescendingValue = _dice.GetDescendingGroups(numberElementsInGroup);
            return groupsByDescendingValue.Count() >= minimumNumberGroups;
        }
    }
}