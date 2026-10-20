using System;
using Db.SceneLoading;
using Game.Utils;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class ScenesBase : IScenesBase
        {
            public string YellowLevel { get; private set; } = "GameYellow";
            public string RedLevel { get; private set; } = "GameRed";
            public string ParkingLevel { get; private set; } = "GameParking";

            public string GetSceneNameByType(EGameLevelType levelType)
            {
                switch (levelType)
                {
                    case EGameLevelType.Yellow:
                        return YellowLevel; 
                    
                    case EGameLevelType.Red:
                        return RedLevel; 
                    
                    case EGameLevelType.Parking:
                        return ParkingLevel; 
                    
                    default:
                    case EGameLevelType.None:
                        throw new ArgumentOutOfRangeException(nameof(levelType), levelType, null);
                }
            }

            public void SetScene(string name)
            {
                YellowLevel = name;
            }
        }
    }
}