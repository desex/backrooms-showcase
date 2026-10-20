using Db.BerserkMode;
using Db.SoundFxBase;
using DG.Tweening;
using Ecs.Action.Commands.Input;
using Ecs.Utils;
using Ecs.Utils.ItemCollectables;
using Game.Services.CollectablesItem;
using Game.Services.Player;
using Game.Services.Render;
using Game.Services.Sound;
using Game.Utils.Collisions;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 943, nameof(EFeatures.Move))]
    public class BerserkModeSystem : ForEachCommandUpdateSystem<BerserkModeCommand>
    {
        private readonly IAccelerationFactorService _accelerationFactorService;
        private readonly IBerserkModeParameters _berserkModeParameters;
        private readonly ICollectiblesStorage _collectiblesStorage;
        private readonly ICollisionService _collisionService;
        private readonly GameContext _game;
        private readonly ISoundService _soundService;
        private readonly ISpeedEffectService _speedEffectService;

        private Tween _tween;

        public BerserkModeSystem(
            ICollectiblesStorage collectiblesStorage,
            ISpeedEffectService speedEffectService,
            IBerserkModeParameters berserkModeParameters,
            ISoundService soundService,
            ICommandBuffer commandBuffer,
            GameContext game,
            ICollisionService collisionService,
            IAccelerationFactorService accelerationFactorService
        ) : base(commandBuffer)
        {
            _collectiblesStorage = collectiblesStorage;
            _speedEffectService = speedEffectService;
            _berserkModeParameters = berserkModeParameters;
            _soundService = soundService;
            _game = game;
            _collisionService = collisionService;
            _accelerationFactorService = accelerationFactorService;
        }

        protected override void Execute(ref BerserkModeCommand command)
        {
            var player = _game.PlayerEntity;

            var tutorialEntity = _game.TutorialEntity;
            var isTutorialActive = tutorialEntity.IsInTutorial;

            if (isTutorialActive)
                return;

            if (player.IsDash)
                return;

            if (player.HasPassObstacleMovePoints)
                return;

            if (command.Enabled)
                Start(player);
            else
                Stop(player);
        }

        private void Start(GameEntity player)
        {
            var price = _berserkModeParameters.AlmondWaterPrice;
            var hasAlmondWaster = _collectiblesStorage.HasCollectibleItem(ECollectableType.AlmondWater, price);

            if (!hasAlmondWaster)
                return;

            _collectiblesStorage.RemoveCollectibleItem(ECollectableType.AlmondWater, price);
            player.IsBerserkMode = true;
            player.IsSpeedBoost = true;
            _accelerationFactorService.InterpolateAccelerationFactor();

            var speed = _berserkModeParameters.BerserkModeSpeedMultiplier;
            _soundService.PlaySound(ESoundFxName.BerserkStart);
            player.ReplaceBerserkSpeedMultiplier(speed);
            _collisionService.DisablePlayerCollisions();
            player.ReplaceBerserkModeTimer(0f);
            _speedEffectService.EnableSpeedEffect();
        }

        private void Stop(GameEntity player)
        {
            player.IsBerserkMode = false;
            player.IsSpeedBoost = false;

            var speed = _berserkModeParameters.DefaultSpeedMultiplier;
            player.ReplaceBerserkSpeedMultiplier(speed);

            _collisionService.EnablePlayerCollisions();

            if (player.HasBerserkModeTimer)
                player.RemoveBerserkModeTimer();

            _soundService.StopSound(ESoundFxName.BerserkStart);
            _soundService.PlaySound(ESoundFxName.BerserkFinish);
            _speedEffectService.DisableSpeedEffect();
        }
    }
}