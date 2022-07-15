using Blackjack.DbContexts;
using Blackjack.Services;

namespace Blackjack
{
    public class Game
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
        // 1 chip = 1 EUR
        const double chipWorth = 1;
        const int dealerSplit = 3;
        const int playerSplit = 2;
        private int availableMoves = 3;
        private readonly PlayerService _service = new PlayerService();
        private readonly GameTracker _gameTracker = new GameTracker();
        private readonly Calculator _calculator = new Calculator();

        public Game()
        {
            Run();
        }

        public void Run()
        {
            PlayerDbo player = Intro();

            var firstCard = _gameTracker.DrawRandomCard();
            var dealerWager = player.Chips[0] / (double)dealerSplit * (double)playerSplit * (double)chipWorth;
            var dealerPoints = firstCard.Value != 1 ? firstCard.Value : _gameTracker.GenerateDealerAceValue(firstCard.Value);

            DealerDbo dealer = new DealerDbo(dealerWager, new List<string>() { firstCard.Key }, dealerPoints);
            
            InfoDisplayer.DisplayInfo(player, dealer);

            GameLoop(player, dealer);

            Run();
        }

        private void GameLoop(PlayerDbo player, DealerDbo dealer)
        {
            var gameOver = false;
            var handGameOver = false;

            for (int i = 0; !gameOver; i++)
            {
                Console.WriteLine($"playing with hand {i + 1}/{player.Hands}...");
                handGameOver = false;

                while (!handGameOver && !gameOver)
                {
                    var move = _service.GetPlayerMove(availableMoves);

                    Console.WriteLine("#####################################################");

                    switch (move)
                    {
                        case 1:
                            _gameTracker.DrawCard(player, dealer, i, ref handGameOver, ref gameOver, ref availableMoves);

                            break;
                        case 2:
                            if (!player.Hands.Equals(i + 1))// if not last hand
                            {
                                _gameTracker.HandStopPlaying(player, dealer);
                                handGameOver = true;
                            }
                            else // if last hand
                            {
                                _gameTracker.DealerGame(dealer);

                                _gameTracker.HandStopPlaying(player, dealer);

                                gameOver = true;
                            }

                            break;
                        case 3:
                            _calculator.CalculateDouble(player, dealer, i);

                            InfoDisplayer.DisplayInfo(player, dealer);

                            break;

                        case 4:
                            // only when first 2 cards are same
                            _calculator.CalculateSplit(player, dealer, i, chipWorth);

                            InfoDisplayer.DisplayInfo(player, dealer);

                            Console.WriteLine($"playing with hand {i + 1}/{player.Hands}...");

                            break;
                        default:
                            return;
                    }
                }
            }
        }

        private PlayerDbo Intro()
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("----------------- B L A C K J A C K -----------------");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine($"-----------------  1 CHIP = {chipWorth} EUR   -----------------");
            Console.WriteLine($"-----------------      {dealerSplit} : {playerSplit}        -----------------");
            Console.WriteLine("-----------------------------------------------------");

            var nickname = _service.GetPlayerNickname();
            var chips = _service.GetPlayerChips();

            double wager = chips * chipWorth;

            Console.WriteLine("--------------------- B E G I N ---------------------");
            Console.WriteLine("----------------- B L A C K J A C K -----------------");
            Console.WriteLine($"||| {nickname} |||");

            var firstCard = _gameTracker.DrawRandomCard();

            var hands = 1;
            var randomCard = new List<string>() { firstCard.Key };
            var points = firstCard.Value;

            if (firstCard.Value == 1)
            {
                Console.WriteLine("Your first card is Ace.");
                points = _gameTracker.ChoosePlayerAceValue(points);
            }

            PlayerDbo newPlayer = new PlayerDbo(nickname, new List<int>() { chips }, new List<double>() { wager }, new List<List<string>>() { randomCard }, new List<int>() { points }, hands);

            return newPlayer;
        }
    }
}
