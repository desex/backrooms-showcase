using Ecs.Managers;
using Ecs.Utils;
using R3;

namespace Game.Services.Score
{
    public interface IScoreService
    {
        ReadOnlyReactiveProperty<float> NormalizedLeftTime { get; }
        ReadOnlyReactiveProperty<int> Combo { get; }
        ReadOnlyReactiveProperty<bool> HasCombo { get; }
        ReadOnlyReactiveProperty<int> CurrentScore { get; }
        ReadOnlyReactiveProperty<int> PassedDistanceInMeters { get; }
        int BestScore { get; } // Do not remove GD request
        int BestPassedDistance { get; }
        ReadOnlyReactiveProperty<int> SuccessActionsCount { get; }
        Observable<Unit> CanGiveWaterForCombo { get; }

        void HandleSuccessAction(EScoreActionType actionType, Uid characterUid);
        void ResetComboAndMultiplier();
        void UpdateBestResults();
    }
}