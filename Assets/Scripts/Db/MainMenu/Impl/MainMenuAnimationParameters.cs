using UnityEngine;

namespace Db.MainMenu.Impl
{
    [CreateAssetMenu(menuName = "Settings/UI/" + nameof(MainMenuAnimationParameters), fileName = nameof(MainMenuAnimationParameters))]
    public class MainMenuAnimationParameters : ScriptableObject, IMainMenuAnimationParameters
    {
        [field: Space]   
        [field: Header("Specific Parameters")]
        [field: SerializeField] public CassetteAnimationParameters Cassette { get; private set; }
        
        [field: Space]
        [field: SerializeField] public MonitorAnimationParameters Monitor { get; private set; }
    }
}