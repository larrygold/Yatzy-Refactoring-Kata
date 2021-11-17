using System.Linq;

namespace Yatzy
{
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
            return new ValueScorer(_dice, 2).Get();
        }

        public int GetScoreThrees()
        {
            return new ValueScorer(_dice, 3).Get();
        }

        public int GetScoreFours()
        {
            return new ValueScorer(_dice, 4).Get();
        }

        public int GetScoreFives()
        {
            return new ValueScorer(_dice, 5).Get();
        }

        public int GetScoreSixes()
        {
            return new ValueScorer(_dice, 6).Get();
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