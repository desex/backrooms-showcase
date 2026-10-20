using Project.Render.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Project.Render.RenderPasses
{
    public abstract class AOnePassScriptableRenderPass<TSettings> : ScriptableRenderPass
    {
        protected TSettings Settings;
        protected Material Material;
        private RenderTextureDescriptor _textureDescriptor;
        private RTHandle _textureHandle;

        private bool _useBlit;

        public void SetupFields(Material material, TSettings settings, RenderPassSetupParameters setupParameters)
        {
            Settings = settings;
            Material = material;
            _useBlit = setupParameters.UseBlit;

            _textureDescriptor = new RenderTextureDescriptor(Screen.width, Screen.height, setupParameters.RenderTextureFormat, 0);
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            _textureDescriptor.width = cameraTextureDescriptor.width;
            _textureDescriptor.height = cameraTextureDescriptor.height;

            RenderingUtils.ReAllocateIfNeeded(ref _textureHandle, _textureDescriptor);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (Material == null)
                return;
            
            var commandBuffer = CommandBufferPool.Get();

            var cameraTargetHandle = renderingData.cameraData.renderer.cameraColorTargetHandle;

            UpdateSettings();

            if (_useBlit)
            {
                Blit(commandBuffer, cameraTargetHandle, _textureHandle);
                Blit(commandBuffer, _textureHandle, cameraTargetHandle, Material);
            } 
            else
            {
                CoreUtils.SetRenderTarget(commandBuffer, cameraTargetHandle);
                commandBuffer.DrawProcedural(Matrix4x4.identity, Material, 0, MeshTopology.Triangles, 3, 1);
            }
            
            context.ExecuteCommandBuffer(commandBuffer);
            CommandBufferPool.Release(commandBuffer);
        }

        public void Dispose()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
                Object.Destroy(Material);
            else
                Object.DestroyImmediate(Material);
#else
                Object.Destroy(Material);
#endif
            _textureHandle?.Release();
        }

        protected abstract void UpdateSettings();
    }
}