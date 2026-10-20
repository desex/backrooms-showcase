using Ecs.Utils;
using Game.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Obstacles
{
    public class TutorialHoleModule : AObjectViewModule
    {
        [SerializeField] private Collider _holeCollider;
        
        private GameContext _game;

        [Inject]
        private void Construct(GameContext game)
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }
        
        private void OnTriggerEnter(Collider other)
        {
            // Более показать не могу
        }
    }
}