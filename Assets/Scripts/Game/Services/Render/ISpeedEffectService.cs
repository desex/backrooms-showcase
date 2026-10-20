using Ecs.Utils;
using Ecs.Utils.ObstaclePass;
using Game.Utils.Movement;

namespace Game.Services.Render
{
    public interface ISpeedEffectService
    {
        void ChangeParametersInSpeedBoostFeature(
            float speed, 
            float maskSize, 
            float maskContrast, 
            float smoothness, 
            float power
        );
        
        ESpeedEffectAxis GetSpeedEffectAxis();
        
        void EnableSpeedEffect(EObstacleTypeForAcceleration obstacleTypeForAcceleration);
        void EnableSpeedEffect();
        void DisableSpeedEffect();
        void ChangeActiveFeature(bool isActive);
        void SetSpeedEffectAxis(EPassObstacleType passObstacleType);
    }
}