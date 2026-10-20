using System;
using Db.SoundFxBase;
using UnityEngine;

namespace Db.SoundScreamer
{
    [Serializable]
    public struct ScreamerParameterVo
    {
        public ESoundFxName ScreamerFxName;

        [Header("Playback time range in seconds")]
        public Vector2 PlaybackRange;

        public ScreamerParameterVo(ESoundFxName screamerFxName, Vector2 playbackRange)
        {
            ScreamerFxName = screamerFxName;
            PlaybackRange = playbackRange;
        }
    }
}