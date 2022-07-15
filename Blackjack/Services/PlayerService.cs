using Blackjack.Helpers;

namespace Blackjack.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly Validator _validator = new Validator();
        public PlayerService()
        {

        }

        public string GetPlayerNickname()
        {
            Console.Write("type your nickname to play: ");

            var nickname = Console.ReadLine();
            var isValid = _validator.IsValidNickname(nickname);

            if (!isValid)
            {
                Console.WriteLine("Invalid nickname. Nickname must contain letters and dashes only.");
                return GetPlayerNickname();
            }

            return nickname;
        }

        public int GetPlayerChips()
        {
            Console.Write("your chips: ");

            var chips = Console.ReadLine();
            var isValid = _validator.IsValidChipsCount(chips);

            if (!isValid)
            {
                Console.WriteLine("Invalid chip quantity. Use digits only.");
                return GetPlayerChips();                   
            }

            return int.Parse(chips);
        }

        public int GetPlayerMove(int choiceCount)
        {
            Console.Write("your move: ");

            var move = Console.ReadLine();
            var isValid = _validator.IsValidMove(move, choiceCount);

            if (!isValid)
            {
                Console.WriteLine($"Invalid move. Only use digits that are not greater than {choiceCount}. " +
                                        $"Also, you can split only when you have a pair of 2 cards of equal value.");
                return GetPlayerMove(choiceCount);
            }

            return int.Parse(move);
        }

        public int GetAceValue()
        {
            Console.Write("choose Ace value (11 or 1): ");

            var aceValue = Console.ReadLine();
            var isValid = _validator.IsValidAceValue(aceValue);

            if (!isValid)
            {
                Console.WriteLine($"Invalid Ace value. It can only be 11 or 1.");
                return GetAceValue();
            }

            return int.Parse(aceValue);
        }
    }
}
