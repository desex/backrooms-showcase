using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components
{
    [Game]
    public class CharacterControllerComponent : IComponent
    {
        public CharacterController Value;
    }
}