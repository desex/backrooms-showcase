namespace Game.Services.UiVisibility
{
    public interface IUiVisibilityService
    {
        void ChangeChaserUiVisibility(bool state);
        void ChangeOtherUiVisibility(bool state);
    }
}