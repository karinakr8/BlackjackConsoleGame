using Blackjack.DbContexts;
using Blackjack.Services;

namespace Blackjack
{
    public class GameTracker
    {
        private Dictionary<string, int> deck = new Dictionary<string, int>(){
                                                            {"ClubsAce", 1},
                                                            {"Clubs2", 2},
                                                            {"Clubs3", 3},
                                                            {"Clubs4", 4},
                                                            {"Clubs5", 5},
                                                            {"Clubs6", 6},
                                                            {"Clubs7", 7},
                                                            {"Clubs8", 8},
                                                            {"Clubs9", 9},
                                                            {"Clubs10", 10},
                                                            {"ClubsJ", 10},
                                                            {"ClubsQ", 10},
                                                            {"ClubsK", 10},
                                                            {"DiamondsAce", 1},
                                                            {"Diamonds2", 2},
                                                            {"Diamonds3", 3},
                                                            {"Diamonds4", 4},
                                                            {"Diamonds5", 5},
                                                            {"Diamonds6", 6},
                                                            {"Diamonds7", 7},
                                                            {"Diamonds8", 8},
                                                            {"Diamonds9", 9},
                                                            {"Diamonds10", 10},
                                                            {"DiamondsJ", 10},
                                                            {"DiamondsQ", 10},
                                                            {"DiamondsK", 10},
                                                            {"HeartsAce", 1},
                                                            {"Hearts2", 2},
                                                            {"Hearts3", 3},
                                                            {"Hearts4", 4},
                                                            {"Hearts5", 5},
                                                            {"Hearts6", 6},
                                                            {"Hearts7", 7},
                                                            {"Hearts8", 8},
                                                            {"Hearts9", 9},
                                                            {"Hearts10", 10},
                                                            {"HeartsJ", 10},
                                                            {"HeartsQ", 10},
                                                            {"HeartsK", 10},
                                                            {"SpadesAce", 1},
                                                            {"Spades2", 2},
                                                            {"Spades3", 3},
                                                            {"Spades4", 4},
                                                            {"Spades5", 5},
                                                            {"Spades6", 6},
                                                            {"Spades7", 7},
                                                            {"Spades8", 8},
                                                            {"Spades9", 9},
                                                            {"Spades10", 10},
                                                            {"SpadesJ", 10},
                                                            {"SpadesQ", 10},
                                                            {"SpadesK", 10} };

        private readonly PlayerService _service = new PlayerService();
        private readonly Calculator _calculator = new Calculator();

        public GameTracker()
        {

        }

        public void DrawCard(PlayerDbo player, DealerDbo dealer, int hand, ref bool handGameOver, ref bool gameOver, ref int availableMoves)
        {
            AddCardToPlayer(player, hand);

            if (player.Cards[hand].Count().Equals(2) && player.Cards[hand].Distinct().Count().Equals(1) && player.Chips[hand] > 1)
                availableMoves = 4;
            else
                availableMoves = 3;

            if (player.Points[hand] > 21)
            {
                handGameOver = true;
                if (player.Hands.Equals(hand + 1))
                {
                    gameOver = true;

                    DealerGame(dealer);
                                        
                    InfoDisplayer.DisplayInfo(player, dealer);

                    Console.WriteLine($"\n\nYOUR WINNINGS: {_calculator.CalculatePlayerResult(player, dealer)} EUR\n");

                    return;
                }
            }

            InfoDisplayer.DisplayInfo(player, dealer);
        }

        private void AddCardToPlayer(PlayerDbo player, int hand)
        {
            var card = DrawRandomCard();

            player.Cards[hand].Add(card.Key);

            if (card.Value == 1)
                player.Points[hand] += ChoosePlayerAceValue(player.Points[hand]);
            else
                player.Points[hand] += card.Value;
        }

        private int GenerateDealerMove(DealerDbo dealer)
        {
            return dealer.Points < 17 ? 1 : 2;
        }

        public void HandStopPlaying(PlayerDbo player, DealerDbo dealer)
        {
            InfoDisplayer.DisplayInfo(player, dealer);

            Console.WriteLine($"\n\nYOUR WINNINGS: {_calculator.CalculatePlayerResult(player, dealer)} EUR\n");

        }

        public void DealerGame(DealerDbo dealer)
        {
            while (true)
            {
                if (GenerateDealerMove(dealer).Equals(1))
                {
                    var card = DrawRandomCard();

                    dealer.Cards.Add(card.Key);

                    if (card.Value == 1)
                        dealer.Points += GenerateDealerAceValue(dealer.Points);
                    else
                        dealer.Points += card.Value;
                }
                else
                {
                    return;
                }
            }
        }

        public KeyValuePair<string, int> DrawRandomCard()
        {
            Random random = new Random();
            int index = random.Next(deck.Count);

            KeyValuePair<string, int> cardValuePair = deck.ElementAt(index);

            return cardValuePair;
        }

        public int ChoosePlayerAceValue(int playerPoints)
        {
            var aceValue = playerPoints > 10 ? 1 : _service.GetAceValue();

            return aceValue;
        }

        public int GenerateDealerAceValue(int dealerPoints)
        {
            var aceValue = dealerPoints > 10 ? 1 : 11;

            return aceValue;
        }
    }
}
