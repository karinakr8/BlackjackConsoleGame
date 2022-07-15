namespace Blackjack.DbContexts
{
    public class DealerDbo
    {
        public double Wager { get; set; }
        public List<string> Cards { get; set; }
        public int Points { get; set; }

        public DealerDbo(double wager, List<string> cards, int points)
        {
            Wager = wager;
            Cards = cards;
            Points = points;
        }
    }
}
