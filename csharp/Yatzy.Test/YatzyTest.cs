using Xunit;

namespace Yatzy.Test
{
    public class YatzyTest
    {
        [Fact]
        public void Chance_scores_sum_of_all_dice()
        {
            var expected = 15;
            var actual = new Yatzy(2, 3, 4, 5, 1).GetScoreChance(2, 3, 4, 5, 1);
            Assert.Equal(expected, actual);
            Assert.Equal(16, new Yatzy(3, 3, 4, 5, 1).GetScoreChance(3, 3, 4, 5, 1));
        }

        [Fact]
        public void Fact_1s()
        {
            Assert.True(new Yatzy(1, 2, 3, 4, 5).GetScoreOnes(1, 2, 3, 4, 5) == 1);
            Assert.Equal(2, new Yatzy(1, 2, 1, 4, 5).GetScoreOnes(1, 2, 1, 4, 5));
            Assert.Equal(0, new Yatzy(6, 2, 2, 4, 5).GetScoreOnes(6, 2, 2, 4, 5));
            Assert.Equal(4, new Yatzy(1, 2, 1, 1, 1).GetScoreOnes(1, 2, 1, 1, 1));
        }

        [Fact]
        public void Fact_2s()
        {
            Assert.Equal(4, new Yatzy(1, 2, 3, 2, 6).GetScoreTwos(1, 2, 3, 2, 6));
            Assert.Equal(10, new Yatzy(2, 2, 2, 2, 2).GetScoreTwos(2, 2, 2, 2, 2));
        }

        [Fact]
        public void Fact_threes()
        {
            Assert.Equal(6, new Yatzy(1, 2, 3, 2, 3).GetScoreThrees(1, 2, 3, 2, 3));
            Assert.Equal(12, new Yatzy(2, 3, 3, 3, 3).GetScoreThrees(2, 3, 3, 3, 3));
        }

        [Fact]
        public void fives()
        {
            Assert.Equal(10, new Yatzy(4, 4, 4, 5, 5).GetScoreFives());
            Assert.Equal(15, new Yatzy(4, 4, 5, 5, 5).GetScoreFives());
            Assert.Equal(20, new Yatzy(4, 5, 5, 5, 5).GetScoreFives());
        }

        [Fact]
        public void four_of_a_knd()
        {
            Assert.Equal(12, new Yatzy(3, 3, 3, 3, 5).GetScoreFourOfAKind(3, 3, 3, 3, 5));
            Assert.Equal(20, new Yatzy(5, 5, 5, 4, 5).GetScoreFourOfAKind(5, 5, 5, 4, 5));
            Assert.Equal(12, new Yatzy(3, 3, 3, 3, 3).GetScoreFourOfAKind(3, 3, 3, 3, 3));
        }

        [Fact]
        public void fours_Fact()
        {
            Assert.Equal(12, new Yatzy(4, 4, 4, 5, 5).GetScoreFours());
            Assert.Equal(8, new Yatzy(4, 4, 5, 5, 5).GetScoreFours());
            Assert.Equal(4, new Yatzy(4, 5, 5, 5, 5).GetScoreFours());
        }

        [Fact]
        public void fullHouse()
        {
            Assert.Equal(18, new Yatzy(6, 2, 2, 2, 6).GetScoreFullHouse(6, 2, 2, 2, 6));
            Assert.Equal(0, new Yatzy(2, 3, 4, 5, 6).GetScoreFullHouse(2, 3, 4, 5, 6));
        }

        [Fact]
        public void largeStraight()
        {
            Assert.Equal(20, new Yatzy(6, 2, 3, 4, 5).GetScoreLargeStraight(6, 2, 3, 4, 5));
            Assert.Equal(20, new Yatzy(2, 3, 4, 5, 6).GetScoreLargeStraight(2, 3, 4, 5, 6));
            Assert.Equal(0, new Yatzy(1, 2, 2, 4, 5).GetScoreLargeStraight(1, 2, 2, 4, 5));
        }

        [Fact]
        public void one_pair()
        {
            Assert.Equal(6, new Yatzy(3, 4, 3, 5, 6).GetScoreOnePair(3, 4, 3, 5, 6));
            Assert.Equal(10, new Yatzy(5, 3, 3, 3, 5).GetScoreOnePair(5, 3, 3, 3, 5));
            Assert.Equal(12, new Yatzy(5, 3, 6, 6, 5).GetScoreOnePair(5, 3, 6, 6, 5));
        }

        [Fact]
        public void sixes_Fact()
        {
            Assert.Equal(0, new Yatzy(4, 4, 4, 5, 5).GetScoreSixes());
            Assert.Equal(6, new Yatzy(4, 4, 6, 5, 5).GetScoreSixes());
            Assert.Equal(18, new Yatzy(6, 5, 6, 6, 5).GetScoreSixes());
        }

        [Fact]
        public void smallStraight()
        {
            Assert.Equal(15, new Yatzy(1, 2, 3, 4, 5).GetScoreSmallStraight(1, 2, 3, 4, 5));
            Assert.Equal(15, new Yatzy(2, 3, 4, 5, 1).GetScoreSmallStraight(2, 3, 4, 5, 1));
            Assert.Equal(0, new Yatzy(1, 2, 2, 4, 5).GetScoreSmallStraight(1, 2, 2, 4, 5));
        }

        [Fact]
        public void three_of_a_kind()
        {
            Assert.Equal(9, new Yatzy(3, 3, 3, 4, 5).GetScoreThreeOfAKind(3, 3, 3, 4, 5));
            Assert.Equal(15, new Yatzy(5, 3, 5, 4, 5).GetScoreThreeOfAKind(5, 3, 5, 4, 5));
            Assert.Equal(9, new Yatzy(3, 3, 3, 3, 5).GetScoreThreeOfAKind(3, 3, 3, 3, 5));
        }

        [Fact]
        public void two_Pair()
        {
            Assert.Equal(16, new Yatzy(3, 3, 5, 4, 5).GetScoreTwoPairs(3, 3, 5, 4, 5));
            Assert.Equal(16, new Yatzy(3, 3, 5, 5, 5).GetScoreTwoPairs(3, 3, 5, 5, 5));
        }

        [Fact]
        public void Yatzy_scores_50()
        {
            var expected = 50;
            var actual = new Yatzy(4, 4, 4, 4, 4).GetScoreYams(4, 4, 4, 4, 4);
            Assert.Equal(expected, actual);
            Assert.Equal(50, new Yatzy(6, 6, 6, 6, 6).GetScoreYams(6, 6, 6, 6, 6));
            Assert.Equal(0, new Yatzy(6, 6, 6, 6, 3).GetScoreYams(6, 6, 6, 6, 3));
        }
    }
}