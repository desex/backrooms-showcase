using UnityEngine;

namespace Db.Transitions
{
    public interface ITransitionSettingsBase
    {
        float MaxPipeTransitionTime { get; }
        float MaxSlidingInPipeDuration { get; }
        int FramesWithoutMeshes { get; }
        int FramesBeforeSlide { get; }
        Vector3 PlayerAfterTransitionStartPosition { get; }
        Vector3 PlayerSlidingPosition { get; }
    }
}