using System;
using System.Collections.Generic;
using Game.DaoData.Tutorial;
using Game.Utils.Tutorial;
using PdUtils.Dao;
using PdUtils.Interfaces;

namespace Game.Services.Tutorial.Impl
{
    public class TutorialService : ITutorialService, IUiInitializable
    {
        private readonly IDao<TutorialData> _tutorialDao;

        private readonly Dictionary<ETutorialState, bool> _tutorialStatesConditions =
            new Dictionary<ETutorialState, bool>();

        public TutorialService(IDao<TutorialData> tutorialDao)
        {
            _tutorialDao = tutorialDao;
        }

        public IReadOnlyDictionary<ETutorialState, bool> TutorialStatesConditions => _tutorialStatesConditions;

        public void FinishTutorial(ETutorialState finishedState)
        {
            // Более показать не могу
        }

        public void Reset()
        {
            // Более показать не могу
        }

        public void PassAllTutorialsExceptComeback()
        {
            // Более показать не могу
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        private void Save()
        {
            // Более показать не могу
        }
    }
}