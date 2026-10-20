using System.Collections.Generic;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles.FeedbackBeforeModule
{
    public class PassObstacleFeedbackBeforeModule : AFeedbackBefore
    {
        [Header("Enable auto resize by vectors")]
        [SerializeField] private bool _isAutoScaling;
        
        [ShowIf(nameof(_isAutoScaling))]
        [SerializeField] private BoxCollider _targetAutoScaling;
        
        [SerializeField] private List<ParticleSystem> _vfxSystems;
        [SerializeField] private List<GameObject> _vfxSystemContainers;
        [SerializeField] private List<ParticleSystem> _sideParticles;
        [SerializeField] private float _rateOverTimeMultiplier = 200;

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }
        
        protected override void Play()
        {
            // Более показать не могу
        }

        protected override void SetColor(ParticleSystem.MinMaxGradient color)
        {
            // Более показать не могу
        }

        private void UpdateSize()
        {
            // Более показать не могу
        }
        
        protected override void Stop()
        {
            // Более показать не могу
        }
    }
}