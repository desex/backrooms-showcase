using Libs.Analytics;

namespace Game.Signals
{
    public class SignalTutorialFinish : ISignalAnalytic
    {
        public readonly bool AddCommonParameters = true;

        public readonly int RestartCount;
        public readonly int StepMovementRestart;
        public readonly int StepTurnsRestart;
        public readonly int StepSlideRestart;
        public readonly int StepJumpRestart;
        public readonly int StepDoubleJumpRestart;
        public readonly int StepComebackRestart;
        public readonly int StepDashRestart;
        public readonly int StepTrainingRestart;
        public readonly int TimeSpentSec;
        
        public readonly bool ComebackStepPassed;
        
        public SignalTutorialFinish(
            int restartCount,
            int stepMovementRestart,
            int stepTurnsRestart,
            int stepSlideRestart,
            int stepJumpRestart,
            int stepDoubleJumpRestart,
            int stepComebackRestart,
            int stepDashRestart,
            int stepTrainingRestart,
            bool comebackStepPassed,
            int timeSpentSec
        )
        {
            RestartCount = restartCount;
            StepMovementRestart = stepMovementRestart;
            StepTurnsRestart = stepTurnsRestart;
            StepSlideRestart = stepSlideRestart;
            StepJumpRestart = stepJumpRestart;
            StepDoubleJumpRestart = stepDoubleJumpRestart;
            StepComebackRestart = stepComebackRestart;
            StepDashRestart = stepDashRestart;
            StepTrainingRestart = stepTrainingRestart;
            ComebackStepPassed = comebackStepPassed;
            TimeSpentSec = timeSpentSec;
        }
    }
}