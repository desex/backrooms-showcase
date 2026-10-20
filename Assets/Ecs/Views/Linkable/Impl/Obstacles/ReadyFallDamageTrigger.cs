using Game.Utils;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Obstacles
{
    public class ReadyFallDamageTrigger : MonoBehaviour
    {
        [Inject] private readonly ICommandBuffer _commandBuffer;
        
        private void OnTriggerEnter(Collider other)
        {
            // Более показать не могу
        }
    }
}