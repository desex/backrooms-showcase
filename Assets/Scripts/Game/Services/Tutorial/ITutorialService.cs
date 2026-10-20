using System.Collections.Generic;
using Game.Utils.Tutorial;

namespace Game.Services.Tutorial
{
    public interface ITutorialService
    {
        IReadOnlyDictionary<ETutorialState, bool> TutorialStatesConditions { get; }
        void FinishTutorial(ETutorialState finishedState);
        void Reset();
        void PassAllTutorialsExceptComeback();
    }
}