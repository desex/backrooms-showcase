using Game.Utils;
using Game.Utils.SerializeCollection.Impl.Dictionary;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles.FeedbackBeforeModule
{
    public class ManualControlFeedbackBeforeModule : AFeedbackBefore
    {
        [SerializeField]
        private BoxCollider _fastActionTrigger;

        [Space]
        [SerializeField]
        private bool _isNeedSides;

        [ShowIf(nameof(_isNeedSides))]
        [SerializeField]
        [BoxGroup]
        private BoxCollider _leftSideTrigger, _rightSideTrigger;

        [Space(25)]
        [SerializeField]
        private DictionaryVfxRootContainers _vfxSideContainers;

        private readonly DictionaryParticleSystemCollection _vfxSystemCollectionSide = new();

        private ParticleSystem.MinMaxGradient _activeColor;
        private ESide _activeSide = ESide.Default;

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnSideTriggerEnter(Collider other, ESide side)
        {
            // Более показать не могу
        }

        private void StopSide(ESide side)
        {
            // Более показать не могу
        }

        protected override void Play()
        {
            // Более показать не могу
        }

        protected override void Stop()
            {
            // Более показать не могу
        }

        private void StopAll()
        {
            // Более показать не могу
        }

        protected override void SetColor(ParticleSystem.MinMaxGradient color)
        {
            // Более показать не могу
        }

        private void OnSideChanged(ESide side)
        {
            // Более показать не могу
        }
    }
}