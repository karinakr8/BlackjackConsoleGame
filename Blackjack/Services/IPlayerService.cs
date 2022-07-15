namespace Blackjack.Services
{
    public interface IPlayerService
    {
        public string GetPlayerNickname();
        public int GetPlayerChips();
        public int GetPlayerMove(int choiceCount);
        public int GetAceValue();
    }
}
