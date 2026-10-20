using Game.Utils;
using Game.Utils.Movement;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Movement
{
    public class UpSurfaceMoveTriggerView : MonoBehaviour
    {
        [Inject] private ICommandBuffer _commandBuffer;
        
        [SerializeField] private ESlideTriggerType triggerType;

        private void OnTriggerEnter(Collider other)
        {
            // Более показать не могу
        }
    }
}