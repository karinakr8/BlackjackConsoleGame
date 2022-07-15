namespace Blackjack.DbContexts
{
    public class PlayerDbo
    {
        public string Nickname { get; set; }
        public List<int> Chips { get; set; }
        public List<double> Wager { get; set; }
        public List<List<string>> Cards { get; set; }
        public List<int> Points { get; set; }
        public int Hands{ get; set; }

        public PlayerDbo(string nickname, List<int> chips, List<double> wager, List<List<string>> cards, List<int> points, int hands)
        {
            Nickname = nickname;
            Chips = chips;
            Wager = wager;
            Cards = cards;
            Points = points;
            Hands = hands;
        }
    }
}
