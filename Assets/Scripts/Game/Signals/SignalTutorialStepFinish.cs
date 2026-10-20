using Libs.Analytics;

namespace Game.Signals
{
    public class SignalTutorialStepFinish : ISignalAnalytic
    {
        public readonly bool AddCommonParameters = true;

        public readonly string TutorialStepName;
        public readonly int TutorialStepRestartCount;

        public SignalTutorialStepFinish(
            string tutorialStepName, 
            int tutorialStepRestartCount
        )
        {
            TutorialStepName = tutorialStepName;
            TutorialStepRestartCount = tutorialStepRestartCount;
        }
    }
}