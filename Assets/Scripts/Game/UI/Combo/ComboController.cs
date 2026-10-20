using Ecs.Utils.ItemCollectables;
using Game.Services.CollectablesItem;
using Game.Services.ComboWords;
using Game.Services.Score;
using PdUtils.Interfaces;
using SimpleUi.Abstracts;
using R3;

namespace Game.UI.Combo
{
    public class ComboController : UiController<ComboView>, IUiInitializable
    {
        private readonly IScoreService _scoreService;
        private readonly IComboWordsService _comboWordsService;
        private readonly ICollectiblesStorage _collectiblesStorage;

        public ComboController(
            IScoreService scoreService, 
            IComboWordsService comboWordsService,
            ICollectiblesStorage collectiblesStorage
        )
        {
            _scoreService = scoreService;
            _comboWordsService = comboWordsService;
            _collectiblesStorage = collectiblesStorage;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        private void OnHasCombo(bool hasCombo)
        {
            // Более показать не могу
        }
        
        private void ShowAlmondWaterForCombo()
        {
            // Более показать не могу
        }

        private void UpdateComboCounterText(int newComboValue)
        {
            // Более показать не могу
        }
        
        private void UpdateComboWordText(string comboWordValue)
        {
            // Более показать не могу
        }

        private void UpdateNormalizedLeftTime(float normalizedLeftTime)
        {
            // Более показать не могу
        }
    }
}