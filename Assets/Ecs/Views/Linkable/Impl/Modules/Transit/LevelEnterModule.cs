using System;
using Db.TransitionChunk;
using Ecs.Utils;
using Game.Services.Level;
using Game.Utils;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Transit
{
    public class LevelEnterModule : AObjectViewModule
    {
        private static readonly int InputCubemap = Shader.PropertyToID("_Input_Cubemap");
        
        [SerializeField] private Collider _enterCollider;
        [SerializeField] private MeshRenderer _portalMeshRenderer;
        
        private MaterialPropertyBlock _materialPropertyBlock;
        private EGameLevelType _targetLevel;
        private bool _wasEnterHandled;
        
        private ICommandBuffer _commandBuffer;
        private ILevelService _levelService;
        private ITransitionChunkParameters _transitionChunkParameters;

        [Inject]
        private void Construct(
            ICommandBuffer commandBuffer,
            ILevelService levelService,
            ITransitionChunkParameters transitionChunkParameters
        )
        {
            // Более показать не могу
        }

        private void Awake()
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        public void OnEnable()
        {
            // Более показать не могу
        }

        public void OnDisable()
        {
            // Более показать не могу
        }
        
        private void ChooseLevel()
        {
            // Более показать не могу
        }

        private void AssignCubemaps()
        {
            // Более показать не могу
        }
        
        private void CreateNextLevel(Collider trigger)
        {
            // Более показать не могу
        }
    }
}