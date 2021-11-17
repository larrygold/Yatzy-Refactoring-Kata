namespace Yatzy
{
    public abstract class Score
    {
        protected Dice _dice;
        public Score(Dice dice)
        {
            _dice = dice;
        }

        public abstract int Get();
    }
}