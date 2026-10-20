using System;
using Db.Score;
using Ecs.Managers;
using Ecs.Utils;
using Game.DaoData;
using Game.Utils.TimerWithTicks;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using PdUtils.Dao;
using R3;
using Zenject;

namespace Game.Services.Score.Impl
{
    public class ScoreService : IScoreService, IInitializable, IDisposable
    {
        private readonly ReactiveCommand _canGiveWaterForCombo = new ReactiveCommand();
        private readonly ReactiveProperty<int> _combo = new ReactiveProperty<int>(0);
        private readonly ICommandBuffer _commandBuffer;
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private readonly ReactiveProperty<int> _currentScore = new ReactiveProperty<int>(0);
        private readonly GameContext _game;

        private readonly ReactiveProperty<bool> _hasCombo = new ReactiveProperty<bool>(false);
        private readonly IDao<LevelScoresData> _levelScoresDao;
        private readonly ReactiveProperty<float> _multiplier;
        private readonly ReactiveProperty<int> _passedDistanceInMeters = new ReactiveProperty<int>(0);
        private readonly IScoreParameters _scoreParameters;
        private readonly ReactiveProperty<int> _successActionsCount = new ReactiveProperty<int>();
        private readonly TimerWithTicks _timerWithTicks;
        private bool _hasMultiplier;
        private int _lastCombo;

        private int _lastHandledPassedDistanceInMeters;
        private LevelScoresData _levelScoresData;
        private int? _totalScore;

        public ScoreService(
            IScoreParameters scoreParameters,
            TimerWithTicks timerWithTicks,
            IDao<LevelScoresData> levelScoresDao,
            ICommandBuffer commandBuffer,
            GameContext game
        )
        {
            _scoreParameters = scoreParameters;
            _timerWithTicks = timerWithTicks;
            _levelScoresDao = levelScoresDao;
            _multiplier = new ReactiveProperty<float>(_scoreParameters.DefaultMultiplier);
            _commandBuffer = commandBuffer;
            _game = game;
        }

        public void Dispose()
        {
            // Более показать не могу
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        public ReadOnlyReactiveProperty<float> NormalizedLeftTime => _timerWithTicks.NormalizedLeftTime;
        public ReadOnlyReactiveProperty<int> Combo => _combo;
        public ReadOnlyReactiveProperty<bool> HasCombo => _hasCombo;
        public ReadOnlyReactiveProperty<int> CurrentScore => _currentScore;
        public ReadOnlyReactiveProperty<int> PassedDistanceInMeters => _passedDistanceInMeters;
        public ReadOnlyReactiveProperty<int> SuccessActionsCount => _successActionsCount;
        public Observable<Unit> CanGiveWaterForCombo => _canGiveWaterForCombo;

        public int BestScore => _levelScoresData.BestScore;
        public int BestPassedDistance => _levelScoresData.BestPassedDistance;

        public void HandleSuccessAction(EScoreActionType actionType, Uid characterUid)
        {
            // Более показать не могу
        }

        public void ResetComboAndMultiplier()
        {
            // Более показать не могу
        }

        public void UpdateBestResults()
        {
            // Более показать не могу
        }

        private void OnEntityCreated(IContext context, IEntity entity)
        {
            // Более показать не могу
        }

        private void OnPassedDistance(GameEntity entity, float value)
        {
            // Более показать не могу
        }

        private void OnPassedDistanceInMeters(int passedDistance)
        {
            // Более показать не могу
        }

        private void IncreaseComboAndMultiplier()
        {
            // Более показать не могу
        }

        private void OnSuccessActionsCount(int successActionsCount)
        {
            // Более показать не могу
        }

        private void TryAddAlmondWaterForCombo()
        {
            // Более показать не могу
        }
    }
}