using Db.UI.Checkpoint.Impl;

namespace Db.UI.Checkpoint
{
    public interface ICheckpointViewParameters
    { 
        CheckpointUiAnimationVo AnimationVo { get; }
        string Text { get; }
    }
}