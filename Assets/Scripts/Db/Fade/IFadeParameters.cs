using Db.Fade.Impl;

namespace Db.Fade
{
    public interface IFadeParameters
    {
        FadeVo GetFadeVo(EFadeType fadeType);
    }
}