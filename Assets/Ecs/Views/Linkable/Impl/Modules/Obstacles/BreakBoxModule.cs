using System.Collections.Generic;
using Core.Utils.TriggerCollision;
using Cysharp.Threading.Tasks;
using Db.SoundFxBase;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Services.Sound;
using Game.Utils;
using Game.Utils.Obstacles;
using Game.Utils.UniTaskExtensions;
using Game.Views.Obstacles;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    public class BreakBoxModule : AObjectViewModule
    {
        [SerializeField]
        private List<BreakBoxView> _boxViews;

        [SerializeField]
        private Transform _soundPoint; //TODO:  use this transform when will be added 3D sound

        private readonly Dictionary<int, BreakBoxView> _allBoxPushData = new Dictionary<int, BreakBoxView>();
        private readonly HashSet<int> _animatedTriggers = new HashSet<int>();
        private readonly Dictionary<int, BreakBoxView> _boxToView = new Dictionary<int, BreakBoxView>();
        private readonly Stack<int> _triggersUids = new Stack<int>();
        private readonly HashSet<EBreakBoxType> _usedTypes = new HashSet<EBreakBoxType>();

        private ICommandBuffer _commandBuffer;
        private bool _isAllBoxesBroken;

        private IReadOnlyDictionary<BreakBoxView, Vector3> _localPositions;
        private IPlayerBoxTracker _playerBoxTracker;
        private ISoundService _soundService;
        private bool _wasAnimatedTriggerAlreadyInvoked;

        [Inject]
        public void Construct(
            ICommandBuffer commandBuffer,
            ISoundService soundService,
            IPlayerBoxTracker playerBoxTracker
        )
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

        public override void Deactivate()
        {
            // Более показать не могу
        }

        private void OnBoxTriggerEnter(int triggerUid)
        {
            // Более показать не могу
        }

        private void OnNotAnimatedBoxTriggerEnter()
        {
            // Более показать не могу
        }

        private async UniTask BreakAllLeftBoxesByPhysics()
        {
            // Более показать не могу
        }
    }
}