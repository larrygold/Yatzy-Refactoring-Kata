namespace Yatzy
{
    public class ChanceScorer : Score
    {
        protected Dice _dice;
        public ChanceScorer(Dice dice) : base(dice)
        {
            _dice = dice;
        }

        public override int Get()
        {
            return _dice.GetSum();
        }
    }
}