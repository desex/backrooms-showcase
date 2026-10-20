namespace Ecs.Views.Linkable
{
    public interface IFastActionTrigger
    {
        float ReadyTriggerOffset { set; }
        float ReadyTriggerLength { set; }
        
        float FastMoveTriggerOffset { set; }
        float FastMoveTriggerLength { set; }
    }
}