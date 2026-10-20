#if UNITY_EDITOR

using Ecs.Game.Components;
using Ecs.Game.Components.Camera;
using Ecs.Game.Components.Chaser;
using Ecs.Game.Components.Checkpoints;
using Ecs.Game.Components.Chunks;
using Ecs.Game.Components.Collectables;
using Ecs.Game.Components.Light;
using Ecs.Game.Components.Player;
using Ecs.Game.Components.Sound;
using Ecs.Game.Components.Tutorial;
using Ecs.Game.Components.Weapon;

namespace Ecs.Utils.EntityDebugNamePrinter
{
    public static class EditorComponentNameProvider
    {
        public static readonly string[] ValidComponentNames =
        {
            nameof(WeaponComponent),
            nameof(PauseComponent),
            nameof(ObstacleComponent),
            nameof(ObstacleGroupComponent),
            nameof(SoundComponent),
            nameof(GameStateComponent),
            nameof(PlayerComponent),
            nameof(CameraComponent),
            nameof(MainMenuCameraComponent),
            nameof(WallTypeComponent),
            nameof(CollectableItemComponent),
            nameof(ChaserComponent),
            nameof(ChunkNodeComponent),
            nameof(CheckpointComponent),
            nameof(AiComponent),
            nameof(TutorialComponent),
            nameof(LightComponent),
        };
    }
}

#endif