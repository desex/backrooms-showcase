using Animancer;
using Db.SoundFxBase;
using Ecs.Utils;
using Game.Services.Sound;
using Game.Utils.Animancer;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class CharacterDeathFromMonsterAnimancerModule : AObjectViewModule
    {
        [SerializeField] private AnimancerComponent _animancer;
        [SerializeField] private MotionClipTransition _monsterHit;
        [SerializeField] private GameObject _monsterHand;
        
        private GameContext _game;
        private ISoundService _soundService;

        [Inject]
        private void Construct(
            GameContext game,
            ISoundService soundService
        )
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnHide(GameEntity entity, EGameState value)
        {
            // Более показать не могу
        }
        
        private void OnPlay(GameEntity entity)
        {
            // Более показать не могу
        }
    }
}