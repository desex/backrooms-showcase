using Ecs.Utils.Groups;
using JCMG.EntitasRedux;

namespace Ecs.Core.Systems
{
    public abstract class ACharacterUpdateSystem : IUpdateSystem
    { 
        private readonly IGameGroupUtils _gameGroupUtils;

        protected ACharacterUpdateSystem(IGameGroupUtils gameGroupUtils)
        {
            _gameGroupUtils = gameGroupUtils;
        }

        protected abstract bool EntityFilter(GameEntity entity);
        protected abstract void HandleUpdate(GameEntity entity);

        public void Update()
        {
            using var _ = _gameGroupUtils.GetCharacters(out var characters);
            
            foreach (var gameEntity in characters)
            {
                if (!EntityFilter(gameEntity))
                    continue;
                
                HandleUpdate(gameEntity);
            };
        }
    }
}