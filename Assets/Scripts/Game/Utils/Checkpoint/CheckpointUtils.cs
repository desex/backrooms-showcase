using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils.Checkpoint
{
    public class CheckpointUtils
    {
        private static readonly List<float> CheckpointSpawnDistances = new();

        public static List<float> CalculateDistancesForSpawn(
            float start, 
            float end,
            float checkpointFrequencyInMeters,
            float distanceToMetersRatio)
        {
            // Более показать не могу
            return default;
        }
    }
}