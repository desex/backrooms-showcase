namespace Db.MainMenu
{
    public interface IMainMenuAnimationParameters
    {
        CassetteAnimationParameters Cassette { get; }
        MonitorAnimationParameters Monitor { get; }
    }
}