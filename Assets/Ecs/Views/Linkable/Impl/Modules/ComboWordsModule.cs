using Ecs.Utils;
using Game.Services.ComboWords;
using Game.Services.Score;
using Game.Utils;
using Game.Utils.Obstacles;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules
{
    public class ComboWordsModule : AObjectViewModule
    {
        [SerializeField] private Collider _trigger;
        [SerializeField] private EComboWordType _comboWordType;
        
        private ILinkedEntityRepository _linkedEntityRepository;
        private IComboWordsService _comboWordsService;
        private IScoreService _scoreService;

        [Inject]
        private void Construct(
            IComboWordsService comboWordsService, 
            IScoreService scoreService,
            ILinkedEntityRepository linkedEntityRepository
        )
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnTriggerEntered(Collider other)
        {
            // Более показать не могу
        }
    }
}