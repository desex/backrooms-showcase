using Game.Utils.QTE;

namespace Game.Services.QTE
{
    public interface IQteService
    {
        bool TryInitQte(EQteType qteType);
    }
}