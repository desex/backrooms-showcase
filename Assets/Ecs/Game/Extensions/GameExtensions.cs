using Db.Ai;
using Db.SoundFxBase;
using Ecs.Managers;
using Ecs.Utils.Monsters;
using Game.Services.General;
using Game.Utils.Checkpoint;
using Game.Utils.Movement;
using Game.Utils.MovementBlock;
using Game.Utils.Sound;
using Game.Utils.Tutorial;
using UnityEngine;

namespace Ecs.Game.Extensions
{
    public static class GameExtensions
    {
        public static GameEntity CreatePlayer(
            this GameContext context,
            Transform spawnTransform,
            int maxHealth,
            float initialForwardSpeedMultiplier
        )
        {
            // Более показать не могу
            return default;
        }

        public static GameEntity CreateCamera(this GameContext context)
        {
            // Более показать не могу
            return default;
        }

        public static GameEntity CreateMainMenuCamera(this GameContext context)
        {
            // Более показать не могу
            return default;
        }

        public static GameEntity CreateCheckpoint(this GameContext context)
        {
            // Более показать не могу
            return default;
        }

        public static GameEntity CreateVirtualCamera(this GameContext context)
        {
            // Более показать не могу
            return default;
        }

        public static GameEntity CreateGhost(
            this GameContext context,
            Vector3 position,
            Quaternion rotation,
            float initialForwardSpeedMultiplier,
            Transform container
        )
        {
            // Более показать не могу
            return default;
        }

        public static GameEntity CreateSoundEntity(this GameContext context)
        {
            // Более показать не могу
            return default;
        }

        public static GameEntity CreateChaserEntity(
            this GameContext context,
            Vector3 position,
            float currentDistance,
            float targetDistance,
            float currentTime
        )
        {
            // Более показать не могу
            return default;
        }

        public static GameEntity CreateLevelEntity(this GameContext context)
        {
            // Более показать не могу
            return default;
        }

        public static GameEntity CreateTutorialEntity(this GameContext context)
        {
            // Более показать не могу
            return default;
        }

        public static GameEntity CreateLightEntity(this GameContext context)
        {
            // Более показать не могу
            return default;
        }
    }
}