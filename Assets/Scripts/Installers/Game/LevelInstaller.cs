using Game.Providers;
using Game.Providers.Impls;
using Game.Utils;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private GameField gameField;

        public override void InstallBindings()
        {
            var gameFiledProvider = new GameFieldProvider()
            {
                GameField = gameField,
            };
            
            Container.Bind<IGameFieldProvider>().FromInstance(gameFiledProvider).AsSingle();
        }
    }
}