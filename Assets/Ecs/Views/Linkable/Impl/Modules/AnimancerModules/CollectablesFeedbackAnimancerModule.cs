using System.Collections.Generic;
using Animancer;
using Db.SoundFxBase;
using Ecs.Utils;
using Ecs.Utils.ItemCollectables;
using Game.Services.Player;
using Game.Services.Sound;
using Game.Utils.Collectables;
using Game.Utils.Movement;
using Game.Utils.SerializeCollection.Impl.Dictionary;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class CollectablesFeedbackAnimancerModule : AObjectViewModule
    {
        private const int COLLECTABLES_LAYER = 1;
        private const int MAIN_LAYER = 0;
        
        [SerializeField] private DictionaryBaseCollectablesAnimationAndSoundData _data;

        private ISoundService _soundService;
        
        private AnimancerComponent _animancer;
        private GameEntity _self;
        private CompositeDisposable _animationEndDisposable = new();
        private CollectablesAnimationAndSoundData _animationData;
        private IPlayerMovementTypeService _playerMovementTypeService;
        private readonly Dictionary<ECollectableType, bool> _collectablePlayedFlags = new();
        private ICommandBuffer _commandBuffer;

        [Inject]
        private void Construct(
            ISoundService soundService,
            IPlayerMovementTypeService playerMovementTypeService,
            ICommandBuffer commandBuffer
        )
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnBodyMoveType(GameEntity entity, EBodyMoveType value)
        {
            // Более показать не могу
        }

        private void PlayAnimationAndSound(GameEntity entity, ECollectableType value)
        {
            // Более показать не могу
        }

        private void OnEndCatchAnimation()
        {
            // Более показать не могу
        }

        private void SetCollectablesLayerEnable(bool isCollectablesEnable)
        {
            // Более показать не могу
        }
    }
}