using System;
using System.Collections.Generic;
using Ecs.Utils;
using Ecs.Utils.Revive;
using Game.Utils.Revive;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Modules.Revive
{
    public partial class ReviveModule : AObjectViewModule
    {
        [PropertySpace(10)]
        [Title("Types")]
        [SerializeField]
        private EReviveType _reviveType = EReviveType.Teleport;
        
        [ShowIf(nameof(IsTeleportReviveTypeSelected))]
        [PropertySpace(10)]
        [Title("Points")]
        [SerializeField]
        private Transform _respawnPoint;

        [ShowIf(nameof(IsHoleReviveTypeSelected))]
        [PropertySpace(6)]
        [SerializeField]
        private List<ReviveMovePointData> _points;
        
        private GameEntity _self;
        private IDisposable _disposable;
        private bool _isInitialized;

        public bool IsTeleportReviveTypeSelected() {
            // Более показать не могу
            return default;
        }
        public bool IsHoleReviveTypeSelected() {
            // Более показать не могу
            return default;
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }


        public override void Activate()
        {
            // Более показать не могу
        }

        private void OnDisable()
        {
            // Более показать не могу
        }

        private void UpdateReviveData(GameEntity self)
        {
            // Более показать не могу
        }
    }
}