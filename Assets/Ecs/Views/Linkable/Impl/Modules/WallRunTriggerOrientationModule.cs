using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Chunk;
using Ecs.Views.Linkable.Impl.Movement;
using Game.Utils.Movement;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using R3;

namespace Ecs.Views.Linkable.Impl.Modules
{
    public class WallRunTriggerOrientationModule : AObjectViewModule
    {
        [SerializeField] private WallRunTriggerView _wallRunTriggerView;
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            var chunkWallView = (ChunkWallView)objectView;
            chunkWallView.IsMirrored.Subscribe(OnMirroredStateChange).AddTo(unsubscribe);
        }
        
        private void OnMirroredStateChange(bool state)
        {
            _wallRunTriggerView.SetTiltType(state ? ETiltType.Right : ETiltType.Left);
        }
    }
}