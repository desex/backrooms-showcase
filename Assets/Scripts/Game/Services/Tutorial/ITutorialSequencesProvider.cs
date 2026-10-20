using System.Collections.Generic;
using Game.Utils.Tutorial;

namespace Game.Services.Tutorial
{
    public interface ITutorialSequencesProvider
    {
        public IReadOnlyCollection<TutorialSequenceData> SequencesQueue { get; }
        bool TryGetNextTutorialSequence(out TutorialSequenceData sequenceData);
        void Initialize();
    }
}