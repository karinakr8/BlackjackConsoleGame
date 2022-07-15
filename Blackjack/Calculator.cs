using Blackjack.DbContexts;

namespace Blackjack
{
    public class Calculator
    {
        public Calculator()
        {

        }
        public double CalculatePlayerResult(PlayerDbo player, DealerDbo dealer)
        {
            double playerWinnings = 0;

            for (int i = 0; i < player.Hands; i++)
            {
                var dealerWins = (player.Points[i] < dealer.Points || player.Points[i] > 21) && dealer.Points <= 21;

                if (dealerWins)
                    playerWinnings -= player.Wager[i];
                else if (player.Points[i] == dealer.Points || (player.Points[i] >= 21 && dealer.Points >= 21))
                    playerWinnings += player.Wager[i];
                else
                    playerWinnings += player.Wager[i] + dealer.Wager;
            }

            return playerWinnings;
        }

        public void CalculateDouble(PlayerDbo player, DealerDbo dealer, int hand)
        {
            player.Wager[hand] *= 2;
            player.Chips[hand] *= 2;
            dealer.Wager *= 2;
        }

        public void CalculateSplit(PlayerDbo player, DealerDbo dealer, int hand, double chipWorth)
        {
            player.Chips.Add((int)player.Chips[hand]);

            player.Wager.Add((double)player.Chips[hand]);

            player.Cards[hand] = new List<string>() { player.Cards[hand][0] };
            player.Cards.Add(new List<string>() { player.Cards[hand][0] });

            player.Points[hand] = player.Points[hand] / 2;
            player.Points.Add(player.Points[hand]);

            player.Hands++;
        }
    }
}
