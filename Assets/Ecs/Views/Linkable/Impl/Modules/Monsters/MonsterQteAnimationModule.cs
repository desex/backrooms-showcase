using Animancer;
using Db.QTE;
using Ecs.Utils;
using Game.Services.General;
using Game.Utils.Animancer;
using Game.Utils.QTE;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Monsters
{
    public class MonsterQteAnimationModule : AObjectViewModule
    {
        [SerializeField] private AnimancerComponent _animancer;
        
        [Space(10)]
        [Header("Animation Qte")]
        [Header("Animation Chaser")]
        [SerializeField] private MotionClipTransition _chaserCatchStart;
        [SerializeField] private MotionClipTransition _chaserCatchLoop;
        [SerializeField] private MotionClipTransition _chaserCatchPunch;
        [SerializeField] private MotionClipTransition _chaserCatchPullingOut;
        [SerializeField] private MotionClipTransition _chaserQteLose;
        
        [Header("Animation Player")]
        [SerializeField] private MotionClipTransition _playerHandCatchStart;
        [SerializeField] private MotionClipTransition _playerHandCatchLoop;
        [SerializeField] private MotionClipTransition _playerHandCatchPunch;
        [SerializeField] private MotionClipTransition _playerHandCatchPullingOut;
        [SerializeField] private MotionClipTransition _playerQteLose;
        
        private ICommandBuffer _commandBuffer;
        private GameContext _game;
        private bool _needPunchAgain;
        private IQteParameters _qteParameters;

        private bool IsInChaserQte
        {
            get
            {
                var player = _game.PlayerEntity;
                return player.HasQteType && player.QteType.Value == EQteType.Chaser;
            }
        }

        [Inject]
        private void Construct(
            GameContext game,
            ICommandBuffer commandBuffer,
            IQteParameters qteParameters
        )
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnCatchAnimationEnd()
        {
            // Более показать не могу
        }
        
        private void OnPunchAnimationEnd()
        {
            // Более показать не могу
        }

        private void PlayCatchLoopAnimation()
        {
            // Более показать не могу
        }

        private void OnInQte(GameEntity entity, EQteType value)
        {
            // Более показать не могу
        }

        private void OnQteLeftProgress(GameEntity entity, int value)
        {
            // Более показать не могу
        }

        private void PlayPunchAnimation()
        {
            // Более показать не могу
        }

        private void OnQteEnd(GameEntity entity, EQteResult value)
        {
            // Более показать не могу
        }
        
        private void OnEscapeAnimationEnd()
        {
            // Более показать не могу
        }
    }
}