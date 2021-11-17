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
            return new YamsScorer(_dice).Get();
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
            return new GroupsScorer(_dice, 1, 2).Get();
        }

        public int GetScoreTwoPairs()
        {
            if (IsApplicable(2, 2))
                return new GroupsScorer(_dice, 2, 2).Get();

            return 0;
        }

        public int GetScoreThreeOfAKind()
        {
            return new GroupsScorer(_dice, 1, 3).Get();
        }

        public int GetScoreFourOfAKind()
        {
            return new GroupsScorer(_dice, 1, 4).Get();
        }

        public int GetScoreSmallStraight()
        {
            return new StraightScorer(_dice, 1, 5).Get();
        }

        public int GetScoreLargeStraight()
        {
            return new StraightScorer(_dice, 2, 6).Get();
        }

        public int GetScoreFullHouse()
        {
            if (IsApplicable(2, 1)
                && IsApplicable(3, 1)
            )
                return GetScoreOnePair() + GetScoreThreeOfAKind();

            return 0;
        }

        private bool IsApplicable(int numberElementsInGroup,
            int minimumNumberGroups)
        {
            var groupsByDescendingValue = _dice.GetDescendingGroups(numberElementsInGroup);
            return groupsByDescendingValue.Count() >= minimumNumberGroups;
        }
    }
}