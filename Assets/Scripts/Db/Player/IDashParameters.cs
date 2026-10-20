using Db.Player.Impl;

namespace Db.Player
{
    public interface IDashParameters
    {
        DashVo DashVo { get; }
        DashInHoleVo DashInHoleVo { get; }
    }
}