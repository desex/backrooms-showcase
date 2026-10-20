using System.Collections.Generic;
using Db.SoundFxBase;
using Ecs.Utils;
using FMODUnity;
using Game.Services.Sound;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Sounds
{
    public partial class MonsterSoundDistanceModule : AObjectViewModule
    {
        [SerializeField] private Transform _soundTransform;
        [SerializeField] private List<Vector3> _soundPoints;
        
        [MinMaxSlider(1, 15, true)] 
        [OnValueChanged(nameof(OnSoundRadiusChanged))]
        [SerializeField] 
        private Vector2 _minMaxSoundRadius = new(1, 10);
        
        private GameEntity _self;
        private ICommandBuffer _commandBuffer;
        private ISoundService _soundService;
        private StudioEventEmitter _emitter;

        [Inject]
        private void Construct(
            ICommandBuffer commandBuffer,
            ISoundService soundService
        )
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnMovementStateChanged(GameEntity entity, EMovementState value)
        {
            // Более показать не могу
        }

        private void InitEmitter()
        {
            // Более показать не могу
        }

        private void OnSoundRadiusChanged(Vector2 value)
        {
            // Более показать не могу
        }
    }
}