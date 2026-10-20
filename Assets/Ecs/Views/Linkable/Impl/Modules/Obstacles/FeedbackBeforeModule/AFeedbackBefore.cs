using Db.PassObstacleFeedback;
using Ecs.Utils;
using Game.Utils;
using Game.Utils.Lights;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles.FeedbackBeforeModule
{
    public abstract class AFeedbackBefore : AObjectViewModule
    {
#if UNITY_EDITOR
        [Header("GD General Settings")]
        [SerializeField] private Vector3 _vfxEnabledPosition = Vector3.zero;
        [SerializeField] private Vector3 _vfxEnabledSize = Vector3.zero;
#endif
        [SerializeField] private BoxCollider _vfxTrigger;
        [SerializeField] private BoxCollider _readyTrigger;
       
        [SerializeField] protected bool IsAutoChangeColor = true;

        private IPassObstacleFeedbackParameters _сolorSettings;
        private ColorType _colorType;
        protected GameContext Game;
        private ColorParameters _colors;

        [Inject]
        private void Construct(GameContext game, IPassObstacleFeedbackParameters parameters)
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        protected abstract void Play();
        protected abstract void Stop();
        protected abstract void SetColor(ParticleSystem.MinMaxGradient color);

        private void OnVfxTriggerExit(Collider other)
        {
            // Более показать не могу
        }

        private void OnVfxTriggerEntered(Collider other)
        {
            // Более показать не могу
        }

        private void OnSetCurrentColor(GameEntity entity, ELightEvent value)
        {
            // Более показать не могу
        }

        #region don'tRemoveGDRequest
        
        private void OnReadyTriggerEntered(Collider other)
        {
            // Более показать не могу
        }

        protected void OnSetFastActionColor()
        {
            // Более показать не могу
        }

        private enum ColorType
        {
            Default,
            Ready,
            FastAction,
        }
        #endregion
        
        #region Editor_Logic

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Более показать не могу
        }
#endif

        #endregion
    }
}