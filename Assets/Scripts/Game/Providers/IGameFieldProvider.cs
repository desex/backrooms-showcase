using Game.Utils;

namespace Game.Providers
{
    public interface IGameFieldProvider
    {
        GameField GameField { get; set; }
    }
}