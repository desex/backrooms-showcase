using System;
using System.Collections.Generic;
using Db.SoundFxBase;
using Ecs.Utils;
using Ecs.Utils.ObstaclePass;
using Ecs.Views.Linkable.Impl.Obstacles.PassObstacle;
using FMODUnity;
using Game.Services.Sound;
using Game.Utils;
using Game.Utils.ColorsUtils;
using Game.Utils.Obstacles;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    public class MovePointReachedModule : AObjectViewModule
    {
        [SerializeField] private PassObstacleView _passObstacleView;

        [SerializeField] private ParticleSystem _vfx;

        [SerializeField] private List<DestructibleObstacleVo> _destructibleObstacleVo;

        [field: Space(2)]
        [SerializeField] private GameObject _animancerGameModel, _baseGameModel;
        
        [Space(2)]
        
        [Header("PASS OBJECT ANIMATIONS")]
        [SerializeField] private Transform _soundContainer;
        [SerializeField] private ESoundFxName _soundName;
        
        [Space(2)]
        
        [Header("MATERIAL CHANGE")]
        [SerializeField] private List<SkinnedMeshRenderer> _materialMeshRenderers;
        [FormerlySerializedAs("_targetMaterials")]
        [SerializeField] private List<Material> _targetCommonMaterials;
        [SerializeField] private List<Material> _targetJumpMaterials;
        [SerializeField] private List<Material> _baseMaterials;
        
        private GameContext _game;

        private GameEntity _self;
        private Dictionary<EPassObstacleType, PassObstacleMovePoint[]> _obstacleMovePoints;
        private Dictionary<EPassObstacleType, DestructibleObstacleVo> _destructibleObstacles;
        private StudioEventEmitter _emitter;

        [Inject]
        private void Construct(GameContext game, ISoundService soundService)
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
        
        private void OnObstacleMovePointReached(GameEntity entity, MovePointReachedData movePointReachedData)
        {
            // Более показать не могу
        }

        private void ActivatedModel(EPassObstacleType passObstacleType)
        {
            // Более показать не могу
        }

        private void PlayerEffect()
        {
            if (_vfx == null)
                throw new ArgumentNullException($"[{nameof(MovePointReachedModule)}] VFX is null");

            _vfx.gameObject.SetActive(true);

#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            DebugUtility.Log($"[{nameof(MovePointReachedModule)}] Play VFX", Color.yellow);
#endif
        }

        private void PlayAnimation(EPassObstacleType passObstacleType)
        {
            _emitter.Play();
            
            if (_destructibleObstacles.TryGetValue(passObstacleType, out var obstacle))
            {
                if (obstacle.AnimancerComponent && obstacle.MotionClipTransition != null)
                    obstacle.AnimancerComponent.Play(obstacle.MotionClipTransition);
            }

#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            DebugUtility.Log($"[{nameof(MovePointReachedModule)}] Play Animation", Color.yellow);
#endif
        }
    }
}