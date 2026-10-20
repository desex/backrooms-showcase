using Ecs.Utils;
using Game.Services.Effects;
using Game.Utils;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules
{
    public class UnderWaterModule : AObjectViewModule
    {
        [SerializeField] private Collider _enterTrigger;
        [SerializeField] private Transform _waterStartPosition;

        private GameContext _game;
        private ICommandBuffer _commandBuffer;
        private IUnderWaterRendererService _underWaterRendererService;
        private bool _wasTriggered;
        
        [Inject]
        private void Construct(
            GameContext gameContext,
            ICommandBuffer commandBuffer,
            IUnderWaterRendererService underWaterRendererService
        )
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        public override void Activate()
        {
            // Более показать не могу
        }

        private void OnEntered(Collider other)
        {
            // Более показать не могу
        }
        
        private void OnExit(Collider other)
        {
            // Более показать не могу
        }
    }
}