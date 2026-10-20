using UnityEngine;

namespace Db.Shaders
{
    public interface IWarmupShadersBase
    {
        public Shader[] WarmupShaders { get; }
    }
}