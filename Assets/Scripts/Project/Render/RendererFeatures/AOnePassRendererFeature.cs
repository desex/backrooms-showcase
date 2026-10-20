using Project.Render.RenderPasses;
using Project.Render.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Project.Render.RendererFeatures
{
    public abstract class AOnePassRendererFeature<TPass, TSettings> : ScriptableRendererFeature where TPass : AOnePassScriptableRenderPass<TSettings>, new()
    {
        public TSettings Settings;
        [SerializeField] private Shader _shader;
        [SerializeField] private bool _useBlit = true;
        
        private Material _material;
        private TPass _renderPass;

        protected abstract string ShaderName { get; }
        protected abstract RenderPassEvent RenderPassEvent { get; }
        protected virtual RenderTextureFormat RenderTextureFormat => RenderTextureFormat.Default;

        public override void Create()
        {
            if (_shader == null)
                _shader = Shader.Find(ShaderName);

            var renderPassSetupParameters = new RenderPassSetupParameters()
            {
                RenderTextureFormat = RenderTextureFormat,
                UseBlit = _useBlit,
            };
            
            _material = new Material(_shader);
            _renderPass = new TPass();
            _renderPass.SetupFields(_material, Settings, renderPassSetupParameters);
            _renderPass.renderPassEvent = RenderPassEvent;
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (renderingData.cameraData.cameraType == CameraType.Game)
            {
                renderer.EnqueuePass(_renderPass);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _renderPass.Dispose();
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                Destroy(_material);
            }
            else
            {
                DestroyImmediate(_material);
            }
#else
            Destroy(_material);
#endif
        }
    }
}