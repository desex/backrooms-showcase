using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerSlideAndJumpParameters), fileName = nameof(PlayerSlideAndJumpParameters))]
    public class PlayerSlideAndJumpParameters : ScriptableObject, IPlayerSlideAndJumpParameters
    {
        [field: Header("General")]
        [field: SerializeField] public float SwipeThreshold { get; private set; }
        
        [field: Header("Slide")]
        [field: SerializeField] public float SlideRollAnimationThreshold { get; private set; } = 0.1f;
        [field: SerializeField] public float SlideTime { get; private set; }
        [field: SerializeField] public float TargetSlideSpeed { get; private set; }
        [field: SerializeField] public float SecondSlideTime { get; private set; }
        [field: SerializeField] public float ThirdSlideTime { get; private set; }
        [field: SerializeField] public float ControllableSlideSpeed { get; private set; }
        [field: SerializeField] public float ControllableSlideAcceleration { get; private set; }
        [field: SerializeField] public float ControllableSlideHorizontalSpeed { get; private set; }
        [field: SerializeField] public float TimeToEndControllableSliding { get; private set; }
        [field: SerializeField] public float ControllableSlideSpeedDecreaseMultiplier { get; private set; } = 0.5f;
        
        [field: Header("Jump")]
        [field: SerializeField] public float JumpHeight { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField] public float DoubleJumpForce { get; private set; }
        [field: SerializeField] public float DoubleJumpHeight { get; private set; }
        [field: SerializeField] public float DoubleJumpInHoleHeight { get; private set; }
        [field: SerializeField] public float DoubleJumpSpeed { get; private set; }

        [field: Header("Roll")]
        [field: SerializeField] public float RollDuration { get; private set; }

        [field: Header("Land")]
        [field: SerializeField] public float LandDuration { get; private set; } = 0.1f;

        [field: Header("CharacterControllerRadiusSize")]
        [field: SerializeField] public float ControllableSlideCharacterControllerRadiusSize { get; private set; } = 0.49f;
        [field: SerializeField] public float DefaultCharacterControllerRadiusSize { get; private set; }= 0.25f;
    }
}