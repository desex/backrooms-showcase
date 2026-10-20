using Db.SoundFxBase;
using Ecs.Utils;
using Game.Services.Sound;
using Game.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules
{
    public class DashPlayerModule : AObjectViewModule
    {
        [Inject] private readonly ISoundService _soundService;

        [SerializeField] private GameObject _box;
        [SerializeField] private BoxCollider _boxTrigger;
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }
        
        private void OnBoxTriggerEntered(Collider trigger)
        {
            // Более показать не могу
        }
    }
}