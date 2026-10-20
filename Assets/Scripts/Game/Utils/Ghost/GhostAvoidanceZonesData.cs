using System;
using System.Collections.Generic;

namespace Game.Utils.Ghost
{
    [Serializable]
    public class GhostAvoidanceZonesData
    {
        private readonly List<GhostSafeZonePositionsData> _safeZones;

        public IReadOnlyList<GhostSafeZonePositionsData> SafeZones => _safeZones;

        public GhostAvoidanceZonesData(List<GhostSafeZonePositionsData> safeZones)
        {
            _safeZones = safeZones;
        }
    }
}