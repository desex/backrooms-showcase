using Libs.Analytics;

namespace Game.Signals
{
    public class SignalTutorialStepStart : ISignalAnalytic
    {
        public readonly bool AddCommonParameters = true;

        public readonly string TutorialStepName;

        public SignalTutorialStepStart(string tutorialStepName)
        {
            TutorialStepName = tutorialStepName;
        }
    }
}