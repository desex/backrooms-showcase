using System;
using System.Collections.Generic;
using Db.SoundFxBase;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Modules.Sound.Structures
{
    [Serializable]
    public struct InteractiveSoundInfo
    {
        [Header("The ID of the sound to play")]
        public ESoundFxName SoundFxName;
        
        [Header("Play sound when triggered in a specific region.")]
        public bool IsPlayOnRegionTrigger;

        [HideIf("IsPlayOnRegionTrigger")]
        public Collider PlayOnRegionTrigger;

        [ShowIf("IsPlayOnRegionTrigger")]
        [Header("Sound playback collider.")]
        public Collider StartTrigger;

        [ShowIf("IsPlayOnRegionTrigger" )]
        [Header("Sound stop collider.")]
        public Collider StopTrigger;
        
        [Header("When enabled, it will create a unique emitter and assign its instance to create a game object.")]
        public bool ThisSoundIsUnique;

        [ShowIf("ThisSoundIsUnique")]
        public Transform SoundEmitterContainer;

        [Header("Temporarily disables sounds until the sound is played.")]
        public bool ScheduleTemporaryMute;

        [ShowIf("ScheduleTemporaryMute")]
        public List<ESoundFxName> ScheduledPauseSounds;
    }
}