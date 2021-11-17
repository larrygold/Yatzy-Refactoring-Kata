using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
    public class StraightScorer : Score
    {
        private readonly int _lowerDieValue;
        private readonly int _higherDieValue;

        public StraightScorer(Dice dice, int lowerDieValue, int higherDieValue) : base(dice)
        {
            _lowerDieValue = lowerDieValue;
            _higherDieValue = higherDieValue;
        }

        public override int Get()
        {
            var sum = 0;

            for (var dieValue = _lowerDieValue; dieValue <= _higherDieValue; dieValue++)
            {
                if (_dice.CountOccurrences()[dieValue] != 1)
                    return 0;

                sum += dieValue;
            }

            return sum;
        }
    }
}