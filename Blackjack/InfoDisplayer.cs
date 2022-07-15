using Blackjack.DbContexts;

namespace Blackjack
{
    public static class InfoDisplayer
    {
        public static void DisplayInfo(PlayerDbo player, DealerDbo dealer)
        {
            Console.WriteLine("-----------------------------------------------------");
            for (int i = 0; i < player.Hands; i++)
            {
                Console.WriteLine($"H A N D {i + 1}");
                Console.WriteLine($"YOUR CHIPS: {player.Chips[i]}");
                Console.WriteLine($"YOUR MONEY: {player.Wager[i]}");
                Console.WriteLine($"YOUR SCORE: {player.Points[i]} \nYOUR CARDS:");
                DisplayCards(player.Cards[i]);
                Console.WriteLine($"\n");
            }

            Console.WriteLine($"HOUSE SCORE: {dealer.Points} \nHOUSE CARDS:");
            DisplayCards(dealer.Cards);

            if (dealer.Points > 11)
                return;

            Console.WriteLine("\n-----------------------------------------------------");
            Console.WriteLine("   YOUR MOVES:");
            Console.WriteLine("   > 1 - Hit");
            Console.WriteLine("   > 2 - Stand");
            Console.WriteLine("   > 3 - Double");
            Console.WriteLine("   > 4 - Split");
            Console.WriteLine("-----------------------------------------------------");
        }

        public static void DisplayCards(List<string> cards)
        {
            foreach (var card in cards)
                Console.Write($" {card}");
        }
    }
}
