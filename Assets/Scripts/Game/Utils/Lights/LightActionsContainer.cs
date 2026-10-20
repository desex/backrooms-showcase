using System;
using System.Collections.Generic;
using Game.Utils.Lights.Actions;
using Sirenix.OdinInspector;

namespace Game.Utils.Lights
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class LightActionsContainer
    {
        public bool NeedResetInEnd;
        public List<ILightActionData> Commands = new ();
    }
}