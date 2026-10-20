using Cysharp.Threading.Tasks;
using Db.LampHiders;
using Ecs.Utils;
using Game.Utils.Overlaps;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    public class LampHiderModule : AObjectViewModule
    {
        [Inject] private readonly IOverlapService _overlapService;
        [Inject] private readonly ILampHiderModuleSettings _settings;
        
        [SerializeField] private Transform _triggerTransform;
        [SerializeField] private Collider _trigger;

        public override async void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }
        
        //TODO: Optimize in the future
        private void HideLamps()
        {
            // var rotationAngle = _triggerTransform.eulerAngles;
            // var isVeered = Mathf.Approximately(rotationAngle.y, _settings.StraightAngle)
            //             || Mathf.Approximately(rotationAngle.y, -_settings.StraightAngle)
            //             || Mathf.Approximately(rotationAngle.y, _settings.InvertedStraightAngle)
            //             || Mathf.Approximately(rotationAngle.y, -_settings.InvertedStraightAngle);
            //
            // var triggerSize = _trigger.size;
            // var squareX = triggerSize.x + _settings.AdditionalSize;
            // var squareZ = triggerSize.z + _settings.AdditionalSize;
            //
            // var square = isVeered
            //     ? new Vector3(squareZ, _settings.SquareY, squareX)
            //     : new Vector3(squareX, _settings.SquareY, squareZ);
            //
            // var position = _triggerTransform.position;
            // var center = new Vector3(position.x, position.y + _settings.CenterY, position.z);
            //
            // using var _ = _overlapService.OverlapBox<LampView>(center, square, LayerMasks.Lamp, out var lampViews);
            //
            // foreach (var view in lampViews)
            //     view.Hide();
        }
    }
}