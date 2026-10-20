using System;
using System.Collections.Generic;
using Db.Score;
using Game.Services.Score;
using Game.Utils.Obstacles;
using PdUtils.RandomProvider;
using R3;
using Zenject;
using static System.String;

namespace Game.Services.ComboWords.Impl
{
    public class ComboWordsService : IComboWordsService, IInitializable
    {
        private readonly ReactiveProperty<string> _comboWord = new ReactiveProperty<string>();

        private readonly Queue<EComboWordType> _comboWordTypesQueue = new Queue<EComboWordType>();
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private readonly IRandomProvider _randomProvider;
        private readonly IScoreParameters _scoreParameters;
        private readonly IScoreService _scoreService;

        public ComboWordsService(
            IScoreParameters scoreParameters,
            IRandomProvider randomProvider,
            IScoreService scoreService
        )
        {
            _scoreParameters = scoreParameters;
            _randomProvider = randomProvider;
            _scoreService = scoreService;
        }

        public ReadOnlyReactiveProperty<string> ComboWord => _comboWord;

        public void ApplyComboType(EComboWordType comboWordType)
        {
            // Более показать не могу
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        private void OnSuccessActionsCount(int successActionsCount)
        {
            // Более показать не могу
        }

        private void GenerateNextComboWord()
        {
            // Более показать не могу
        }

        private string GetRandomWord(IReadOnlyList<string> words)
        {
            // Более показать не могу
            return default;
        }

        private int GetRandomIndexWord(int length)
        {
            // Более показать не могу
            return default;
        }
    }
}