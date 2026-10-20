using System.Collections.Generic;
using Game.Utils.HoleGenerator;
using UnityEngine;

namespace Db.HoleGenerator
{
    public interface IHoleConstructorParameters
    {
        GameObject HoleBottom { get; }
        GameObject HoleWall { get; }
        GameObject HoleFloor { get; }
        GameObject UpHoleWall { get; }
        GameObject UpHoleCelling { get; }
        
        GameObject EffectsTriggers { get; }
        GameObject EffectsFeedbackBeforeTriggers { get; }
        GameObject GhostJumpTrigger { get; }
    
        Dictionary<EGeneratorHoleType, List<ObstaclePassSettingsParameters>> PassObstacleHole { get; }
        IReadOnlyDictionary<EWallSide, HoleFloorDimensionsParameters> FloorsSettings { get; }
        
        Material HoleWallAttachedYellowMaterial { get; }
        Material HoleWallYellowMaterial { get; }
        Material HoleFloorYellowMaterial { get; }
        Material UpHoleWallYellowMaterial { get;  }
        Material UpHoleCellingYellowMaterial { get; }
        
        Material HoleWallAttachedPoolMaterial { get; }
        Material HoleWallPoolMaterial { get; }
        Material HoleFloorPoolMaterial { get; }
        Material UpHoleWallPoolMaterial { get; }
        Material UpHoleCellingPoolMaterial { get; }
        
        Material HoleWallAttachedRedMaterial { get; }
        Material HoleWallRedMaterial { get; }
        Material HoleFloorRedMaterial { get; }
        Material UpHoleWallRedMaterial { get; }
        Material UpHoleCellingRedMaterial { get; }
    }
}