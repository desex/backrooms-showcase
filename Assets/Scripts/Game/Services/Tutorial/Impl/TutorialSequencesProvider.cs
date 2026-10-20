using System.Collections.Generic;
using Db.Tutorial;
using Game.Utils.Tutorial;

namespace Game.Services.Tutorial.Impl
{
    public class TutorialSequencesProvider : ITutorialSequencesProvider
    {
        private readonly Queue<TutorialSequenceData> _sequencesQueue = new Queue<TutorialSequenceData>();
        private readonly ITutorialParameters _tutorialParameters;

        public TutorialSequencesProvider(ITutorialParameters tutorialParameters)
        {
            _tutorialParameters = tutorialParameters;
        }

        public IReadOnlyCollection<TutorialSequenceData> SequencesQueue => _sequencesQueue;

        public void Initialize()
        {
            // Более показать не могу
        }

        public bool TryGetNextTutorialSequence(out TutorialSequenceData sequenceData)
        {
            // Более показать не могу
            sequenceData = default;
            return default;
        }
    }
}