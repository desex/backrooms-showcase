using System;
using Db.UI.Collectables;
using DG.Tweening;
using Ecs.Utils.ItemCollectables;
using Game.Services.CollectablesItem;
using PdUtils.Interfaces;
using R3.Utils;
using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;
using R3;

namespace Game.UI.Collectables
{
    public class CollectablesController : UiController<CollectablesView>, IUiInitializable
    {
        private readonly ICollectiblesStorage _collectiblesStorage;
        private readonly ICollectablesViewParameters _collectablesViewParameters;
        
        public CollectablesController(
            ICollectiblesStorage collectiblesStorage,
            ICollectablesViewParameters collectablesViewParameters
        )
        {
            _collectiblesStorage = collectiblesStorage;
            _collectablesViewParameters = collectablesViewParameters;
        }

        public void Initialize()
        {
            // Более показать не могу
        }
        
        private void ChangedUIState(DictionaryReplaceEvent<ECollectableType, int> evt)
        {
            // Более показать не могу
        }

        private void AnimateWaterPickup()
        {
            // Более показать не могу
        }

        private void AnimateCollectables(
            CanvasGroup collectableView, 
            Transform textTransform
        )
        {
            // Более показать не могу
        }

        private void ChangeCollectablesCount(TMP_Text text, ECollectableType type)
        {
            // Более показать не могу
        }

        private static void Activate(bool isActive, Component view)
        {
            // Более показать не могу
        }
    }
}