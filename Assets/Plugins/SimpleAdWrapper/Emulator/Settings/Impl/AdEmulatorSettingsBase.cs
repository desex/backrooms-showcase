using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils.Drawers.Key;

namespace Plugins.SimpleAdWrapper.Emulator.Settings.Impl
{
    [CreateAssetMenu(fileName = nameof(AdEmulatorSettingsBase), menuName = "Monetization/" + nameof(AdEmulatorSettingsBase))]
    public class AdEmulatorSettingsBase : ScriptableObject, IAdEmulatorSettingsBase
    {
        public bool useEmulator = true;

        [BoxGroup("Load")]
        public AdPreloadPattern preloadPattern;

        [Space]
        public bool useSinglePatternForAllPlaces;

        [ShowIf(nameof(useSinglePatternForAllPlaces))]
        public string patternForAllPlaces;

        [HideIf(nameof(useSinglePatternForAllPlaces))]
        [KeyValueFormat("{0} : {1}", nameof(AdEmulatorPlace.place), nameof(AdEmulatorPlace.adType))]
        public AdEmulatorPlace[] places;

        public AdPreloadPattern PreloadPattern => preloadPattern;

        public AdEmulatorPlace GetPlace(string place, EAdType adType)
        {
            if (useSinglePatternForAllPlaces)
                return new()
                {
                    place = place,
                    adType = adType,
                    pattern = patternForAllPlaces
                };
            
            foreach (var placeSettings in places)
            {
                if (placeSettings.adType == adType && placeSettings.place == place)
                    return placeSettings;
            }

            return default;
        }
    }

    [Serializable]
    public struct AdPreloadPattern
    {
        public float loadDelay;
        public bool loadSuccess;
    }

    [Serializable]
    public struct AdEmulatorPlace
    {
        public string place;
        public EAdType adType;
        public string pattern;
    }
}