using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.SimpleAdWrapper.Emulator.Settings.Impl
{
    [CreateAssetMenu(fileName = nameof(AdStatesPatternBase), menuName = "Monetization/" + nameof(AdStatesPatternBase))]
    public class AdStatesPatternBase : ScriptableObject, IAdStatesPatternBase
    {
        public AdStatesPattern[] patterns;
        
        public AdStatesPattern GetPattern(string placeSettingsPattern)
        {
            foreach (var pattern in patterns)
            {
                if (pattern.name == placeSettingsPattern)
                    return pattern;
            }

            return default;
        }
    }

    [Serializable]
    public struct AdStatesPattern
    {
        public string name;

        public State[] states;

        [Serializable]
        public struct State
        {
            public float delayBefore;
            public float delayAfter;
            public EAdState adState;

            [ShowIf(nameof(adState), EAdState.Close)]
            public bool finishedWithReward;
            
            [ShowIf("@this.adState == EAdState.Fail || this.adState == EAdState.FailToLoad || this.adState == EAdState.FailToPlay")]
            public string failReason;
        }
    }
}