using System.Collections.Generic;
using Db.GameLevelsSettings.Impls.Sequence;
using Db.Tutorial.StateVos;
using DG.Tweening;
using Game.Utils.Sequence;
using Game.Utils.Tutorial;
using UnityEngine;

namespace Db.Tutorial
{
    public interface ITutorialParameters
    {
        float FailTeleportForwardOffset { get; }

        Vector3 InitialChunkOffset { get; }
        SequenceParameters PreTutorialSequence { get; }
        IReadOnlyDictionary<ETutorialState, TutorialSequencesParameters> TutorialSequences { get; }
        IReadOnlyList<ETutorialState> StatesInOrder { get; }

        int FirstSequencesCount { get; }
        float PlayerAfkDelay { get; }

        float SlowdownTimeScale { get; }
        float SlowdownVerticalSpeed { get; }
        float SlowdownDuration { get; }
        Ease SlowdownEase { get; }
        
        float RevertSlowdownDuration { get; }
        Ease RevertSlowdownEase { get; }
        TutorialDoubleJumpParameters DoubleJumpParameters { get; }
        TutorialTiltingParameters TiltingParameters { get; }
        TutorialTurningParameters TurningParameters { get; }
        TutorialSlidingParameters SlidingParameters { get; }
        TutorialDashParameters DashParameters { get; }
        float TutorialDistanceInUnits { get; }
        float FirstTutorialDistanceThresholdInUnits { get; }
    }
}