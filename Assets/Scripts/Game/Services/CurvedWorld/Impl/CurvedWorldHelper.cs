using System;
using AmazingAssets.CurvedWorld;
using Cysharp.Threading.Tasks;
using Game.Utils.UniTaskExtensions;
using UnityEngine;

namespace Game.Services.CurvedWorld.Impl
{
    public class CurvedWorldHelper
    {
        private readonly CurvedWorldController _curvedWorldController;
        
        public CurvedWorldHelper(CurvedWorldController curvedWorldController)
        {
            _curvedWorldController = curvedWorldController;
        }
        
        public void SetParamaterHorizontalWorld(float startOffset, float endOffset, float startSize, float endSize, float duration)
        {
            InterpolateValues(startOffset,
                      endOffset,
                      duration,
                      value => _curvedWorldController.bendHorizontalOffset = value)
               .ForgetAndWrapExceptions();
            
            InterpolateValues(startSize,
                      endSize,
                      duration,
                      value => _curvedWorldController.bendHorizontalSize = value)
               .ForgetAndWrapExceptions();
        }
        
        public void SetParamaterVerticalWorld(float startOffset, float endOffset, float startSize, float endSize, float duration)
        {
            InterpolateValues(startOffset,
                      endOffset,
                      duration,
                      value => _curvedWorldController.bendVerticalOffset = value)
               .ForgetAndWrapExceptions();
            
            InterpolateValues(startSize,
                      endSize,
                      duration,
                      value => _curvedWorldController.bendVerticalSize = value)
               .ForgetAndWrapExceptions();
        }
        
        public void SetParamaterCurvatureWorld(float startOffset, float endOffset, float startSize, float endSize, float duration)
        {
            InterpolateValues(startOffset,
                      endOffset,
                      duration,
                      value => _curvedWorldController.bendCurvatureOffset = value)
               .ForgetAndWrapExceptions();
            
            InterpolateValues(startSize,
                      endSize,
                      duration,
                      value => _curvedWorldController.bendCurvatureSize = value)
               .ForgetAndWrapExceptions();
        }
        
        private async UniTask InterpolateValues(float start, float end, float duration, Action<float> onValueChanged)
        {
            var elapsed = 0f;
            
            while (elapsed < duration)
            {
                var progress = elapsed / duration;
                var currentValue = Mathf.Lerp(start, end, progress);
                
                onValueChanged?.Invoke(currentValue);
                
                elapsed += Time.deltaTime;
                await UniTask.Yield();
            }
            
            onValueChanged?.Invoke(end);
        }
    }
}