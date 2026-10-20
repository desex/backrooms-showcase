using System;
using Cysharp.Threading.Tasks;
using Db.Shaders;
using PdUtils.SceneLoadingProcessor;
using PdUtils.SceneLoadingProcessor.Impls;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Game.Services.SceneLoading.Processors
{
    public sealed class WarmupShadersProcess : Process, IProgressable
    {
        private readonly IWarmupShadersBase _warmupShadersBase;

        private int _shadersPassedCount = 0;

        public float Progress => _warmupShadersBase.WarmupShaders.Length != 0 
            ? _shadersPassedCount / (float)_warmupShadersBase.WarmupShaders.Length 
            : 1f;
        
        public WarmupShadersProcess(IWarmupShadersBase warmupShadersBase)
        {
            _warmupShadersBase = warmupShadersBase;
        }
        
        public override void Do(Action onComplete)
        {
            WarmupShaders(onComplete).Forget();
        }

        private async UniTask WarmupShaders(Action onComplete)
        {
            var shaderWarmupSetup = new ShaderWarmupSetup
            {
                vdecl = new[]
                {
                    new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3),
                    new VertexAttributeDescriptor(VertexAttribute.Normal, VertexAttributeFormat.Float16, 2),
                    new VertexAttributeDescriptor(VertexAttribute.Tangent, VertexAttributeFormat.UNorm8, 4),
                },
            };
            
            foreach (var shader in _warmupShadersBase.WarmupShaders)
            {
                ShaderWarmup.WarmupShader(shader, shaderWarmupSetup);
                _shadersPassedCount++;
                await UniTask.Yield();
            }
            
            onComplete();
        }
    }
}