using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Sound.Structures;
using FMODUnity;
using Game.Services.Sound;
using Game.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Sound
{
    public class InteractiveSoundModule : AObjectViewModule
    {
        [SerializeField] private InteractiveSoundInfo _interactiveSoundInfo;

        private ISoundService _soundService;
        private StudioEventEmitter _emitter;

        [Inject]
        private void Construct(ISoundService soundService)
        {
            // Более показать не могу
        }

        public void Awake()
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void Initialize()
        {
            // Более показать не могу
        }

        private void OnSoundTriggerEnter(Collider other)
        {
            // Более показать не могу
        }

        private void OnSoundTriggerExit(Collider other)
        {
            // Более показать не могу
        }

        private void PlaySound()
        {
            // Более показать не могу
        }

        private void StopSound()
        {
            // Более показать не могу
        }

        private void MuteSound()
        {
            // Более показать не могу
        }

        private void UnMuteSound()
        {
            // Более показать не могу
        }
    }
}