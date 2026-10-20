using Db.QTE;
using Db.SoundFxBase;
using Ecs.Utils;
using Game.Models.Camera.Impl;
using Game.Services.Ghosts.Sound;
using Game.Services.QTE;
using Game.Services.Revive;
using Game.Services.Sound;
using Game.Utils;
using Game.Utils.Animancer;
using Game.Utils.Characters;
using Game.Utils.Obstacles;
using Game.Utils.QTE;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Obstacles.DynamicObstacles
{
    public class HandInteractionModule : AObjectViewModule
    {
        [SerializeField]
        private HandInteractionAnimationsHolder _handView;

        [SerializeField]
        private GameObject _hand;

        [SerializeField]
        private Transform _deathBone;

        [SerializeField]
        private Transform _handModel;

        [SerializeField]
        private Collider _handFallTrigger;

        [SerializeField]
        private Collider _deathTrigger;

        [Space(10)]
        [Header("Animation QTE")]
        [SerializeField]
        private bool _canStartQte = true;

        private PlayerCameraHolder _cameraHolder;
        private GameEntity _character;

        private ICommandBuffer _commandBuffer;

        private GameEntity _entity;
        private GameContext _game;
        private IGhostSoundDispatcher _ghostSoundDispatcher;
        private EHandState _handState;
        private ILinkedEntityRepository _linkedEntityRepository;
        private bool _needPunchAgain;
        private IReviveObstacleService _obstacleService;
        private Vector3 _playerInitialPosition;
        private Quaternion _playerInitialRotation;
        private IQteParameters _qteParameters;
        private IQteService _qteService;
        private ISoundService _soundService;

        [Inject]
        public void Construct(
            ICommandBuffer commandBuffer,
            GameContext game,
            PlayerCameraHolder cameraHolder,
            ISoundService soundService,
            IReviveObstacleService obstacleService,
            ILinkedEntityRepository linkedEntityRepository,
            IGhostSoundDispatcher ghostSoundDispatcher,
            IQteService qteService,
            IQteParameters qteParameters
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

        public override void Deactivate()
        {
            // Более показать не могу
        }

        public void OnPlayDeathFromHandAnimation()
        {
            // Более показать не могу
        }

        private void ShowHand(Collider other)
        {
            // Более показать не могу
        }

        public void OnHandAppeared()
        {
            // Более показать не могу
        }

        private void CatchPlayer(Collider other)
        {
            // Более показать не могу
        }

        private void OnQteLeftProgress(GameEntity entity, int value)
        {
            // Более показать не могу
        }

        public void OnPunchAnimationEnd()
        {
            // Более показать не могу
        }

        public void OnEndQteEscapeAnimation()
        {
            // Более показать не могу
        }

        private void OnQteEnd(GameEntity player, EQteResult qteResult)
        {
            // Более показать не могу
        }

        private void OnEndKillAnimation()
        {
            // Более показать не могу
        }

        private static void SetCharacterParent(GameEntity character, Transform parent)
        {
            // Более показать не могу
        }
    }
}