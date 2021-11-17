using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
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
}