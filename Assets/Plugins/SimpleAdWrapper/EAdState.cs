namespace Plugins.SimpleAdWrapper
{
    public enum EAdState
    {
        None,
        Preload,  // before any actions
        Load,     // load ad
        Loaded,   // ad loaded
        Show,     // ad show
        Clicked,  // ad clicked
        Finished, // ad finished. video
        Close,    // ad close
        Complete, // any actions completed
        Forbidden, 
        Expire,
        Fail,
        FailToPlay,
        FailToLoad,
    }
}