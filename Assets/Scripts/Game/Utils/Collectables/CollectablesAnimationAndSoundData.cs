using System;
using Db.SoundFxBase;
using Game.Utils.Animancer;
using UnityEngine;

namespace Game.Utils.Collectables
{
    [Serializable]
    public struct CollectablesAnimationAndSoundData
    {
        public MotionClipTransition Clip;
        public ESoundFxName SoundFxName;
        public ECollectableSoundType CollectableSoundType;
        public GameObject CollectablesModel;
        public float AnimationSpeedMultiplier;
    }
}