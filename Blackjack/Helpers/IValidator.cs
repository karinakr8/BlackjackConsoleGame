namespace Blackjack.Helpers
{
    public interface IValidator
    {
        public bool IsValidNickname(string nickname);
        public bool IsValidMove(string playMove, int choiceCount);
        public bool IsValidChipsCount(string playMove);
        public bool IsValidAceValue(string aceValue);
    }
}