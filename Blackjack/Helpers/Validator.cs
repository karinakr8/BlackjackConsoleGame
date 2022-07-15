using System.Text.RegularExpressions;

namespace Blackjack.Helpers
{
    public class Validator : IValidator
    {
        public bool IsValidChipsCount(string playerChipsCount)
        {
            return int.TryParse(playerChipsCount, out int number) && number > 0;
        }

        public bool IsValidMove(string playerMove, int choiceCount)
        {
            return int.TryParse(playerMove, out int number) && number <= choiceCount && number > 0;
        }

        public bool IsValidNickname(string playerNickname)
        {
            Regex nicknameRegex = new Regex(@"^[a-zA-Z-]+$");

            return nicknameRegex.IsMatch(playerNickname);
        }

        public bool IsValidAceValue(string aceValue)
        {
            return int.TryParse(aceValue, out int number) && (number == 1 || number == 11);
        }
    }
}
