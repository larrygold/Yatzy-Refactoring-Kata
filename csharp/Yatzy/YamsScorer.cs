namespace Yatzy
{
    public class YamsScorer : Score
    {
        protected Dice _dice;
        public YamsScorer(Dice dice) : base(dice)
        {
            _dice = dice;
        }

        public override int Get()
        {
            foreach (var count in _dice.CountOccurrences())
                if (count == 5)
                    return 50;
            return 0;
        }
    }
}