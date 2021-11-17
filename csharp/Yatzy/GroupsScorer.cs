using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
    public class GroupsScorer : Score
    {
        private int _numberGroups;
        private int _numberElementsInGroup;

        public GroupsScorer(Dice dice, int numberGroups, int numberElementsInGroup) : base(dice)
        {
            _numberGroups = numberGroups;
            _numberElementsInGroup = numberElementsInGroup;
        }

        public override int Get()
        {
            var pairs = _dice.GetDescendingGroups(_numberElementsInGroup);
            return pairs.Take(_numberGroups).Sum(x => _numberElementsInGroup * x);
        }
    }
}