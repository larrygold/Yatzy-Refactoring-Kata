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
}