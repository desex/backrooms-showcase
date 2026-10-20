namespace Db.Obstacles
{
    public interface IObstacleParametersBase
    {
        float ReactivationDelay { get; }
        
        float ReadyTriggerOffset { get; }
        float ReadyTriggerLength { get; }
        
        float FastMoveTriggerOffset { get; }
        float FastMoveTriggerLength { get; }
    }
}