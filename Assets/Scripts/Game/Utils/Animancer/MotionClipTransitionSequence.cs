using System;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

namespace Game.Utils.Animancer
{ 
    [Serializable]
    public class MotionClipTransitionSequence : MotionClipTransition, ISerializationCallbackReceiver, ICopyable<MotionClipTransitionSequence>
    {
        [DrawAfterEvents]
        [SerializeField]
        [Tooltip("The other transitions to play in order after the first one.")]
        private MotionClipTransition[] _Others = Array.Empty<MotionClipTransition>();

        /// <summary>[<see cref="SerializeField"/>] The transitions to play in order after the first one.</summary>
        public ref MotionClipTransition[] Others => ref _Others;

        /// <summary>The last of the <see cref="Others"/> (or <c>this</c> if there are none).</summary>
        public MotionClipTransition LastTransition => _Others.Length > 0 ? _Others[^1] : this;

        /// <inheritdoc/>
        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        /// <inheritdoc/>
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            // Более показать не могу
        }

        private System.Action _OnEnd;

        /// <inheritdoc/>
        public override void Apply(AnimancerState state)
        {
            // Более показать не могу
        }

        /// <summary>Is everything in this sequence valid?</summary>
        public override bool IsValid
        {
            get
            {
                if (!base.IsValid)
                    return false;

                for (var i = 0; i < _Others.Length; i++)
                    if (!_Others[i].IsValid)
                        return false;

                return true;
            }
        }

        /// <summary>Is the last animation in this sequence looping?</summary>
        public override bool IsLooping => _Others.Length > 0 ? LastTransition.IsLooping : base.IsLooping;

        /// <inheritdoc/>
        public override float Length
        {
            get
            {
                var length = base.Length;
                for (var i = 0; i < _Others.Length; i++)
                    length += _Others[i].Length;
                return length;
            }
        }

        /// <inheritdoc/>
        public override float MaximumDuration
        {
            get
            {
                var value = base.MaximumDuration;
                for (var i = 0; i < _Others.Length; i++)
                    value += _Others[i].MaximumDuration;
                return value;
            }
        }

        /// <inheritdoc/>
        public override float AverageAngularSpeed
        {
            get
            {
                var speed = base.AverageAngularSpeed;
                if (_Others.Length == 0)
                    return speed;

                var duration = base.MaximumDuration;
                speed *= duration;

                for (var i = 0; i < _Others.Length; i++)
                {
                    var other = _Others[i];
                    var otherSpeed = other.AverageAngularSpeed;
                    var otherDuration = other.MaximumDuration;
                    speed += otherSpeed * otherDuration;
                    duration += otherDuration;
                }

                return speed / duration;
            }
        }

        /// <inheritdoc/>
        public override Vector3 AverageVelocity
        {
            get
            {
                var velocity = base.AverageVelocity;

                if (_Others.Length == 0)
                    return velocity;

                var duration = base.MaximumDuration;
                velocity *= duration;

                for (var i = 0; i < _Others.Length; i++)
                {
                    var other = _Others[i];
                    var otherVelocity = other.AverageVelocity;
                    var otherDuration = other.MaximumDuration;
                    velocity += otherVelocity * otherDuration;
                    duration += otherDuration;
                }

                return velocity / duration;
            }
        }

        /// <summary>Adds the <see cref="ClipTransition.Clip"/> of everything in this sequence to the collection.</summary>
        public override void GatherAnimationClips(ICollection<AnimationClip> clips)
        {
            // Более показать не могу
        }

        /// <inheritdoc/>
        public virtual void CopyFrom(MotionClipTransitionSequence copyFrom)
        {
            // Более показать не могу
        }

        #region Events

        /// <summary>The <see cref="AnimancerEvent.Sequence.EndEvent"/> of the last transition in this sequence.</summary>
        public AnimancerEvent EndEvent
        {
            get => LastTransition.Events.EndEvent;
            set => LastTransition.Events.EndEvent = value;
        }

        /// <summary>Adds an event at the specified time relative to the entire sequence.</summary>
        public void AddEvent(float time, bool normalized, System.Action callback)
        {
            // Более показать не могу
        }

        /// <summary>
        /// Tries to add the `callback` as an event to the `transition` if the `time` is within the `length` and
        /// returns true if successful. Otherwise subtracts the `length` from the `time` and returns false so it can be
        /// tried in the next transition in the sequence.
        /// </summary>
        private static bool TryAddEvent(ClipTransition transition, float length, ref float time, System.Action callback)
        {
            // Более показать не могу
            return default;
        }

        /// <summary>
        /// Adds the `callback` as an event to the `transition` at the specified `time` (in seconds, starting from the
        /// <see cref="ClipTransition.NormalizedStartTime"/>).
        /// </summary>
        private static void AddEvent(ClipTransition transition, float time, System.Action callback)
        {
            // Более показать не могу
        }

        #endregion
    }
}