using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Utils.Checkpoint
{
    [Serializable]
    public struct CheckpointTexturesData
    {
        [PreviewField] [HorizontalGroup]
        public Texture2D Appear;
        [PreviewField] [HorizontalGroup]
        public Texture2D Loop;
    }
}