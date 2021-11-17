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
            return _dice.CountForValue(_dieValue) * _dieValue;
        }
    }
}