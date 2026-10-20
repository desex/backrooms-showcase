using System.Linq;
using System.Reflection;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles;
using Ecs.Views.Linkable.Impl.Obstacles;

namespace Editor.Tools.HoleGenerator
{
    public partial class HoleGeneratorEditor
    {
        private void PreInitialize()
        {
            var holeObstacleView = _root.AddComponent<ObstacleGroupView>();
        
            holeObstacleView.FillModules();
        
            _rootGroupView = holeObstacleView;
        }

        private void PostInitialize()
        {
            var modules = _root.GetComponentsInChildren<AObjectViewModule>();

            var reviveCameraModule = (ReviveCameraFocusModule)modules.First(module => module.GetType() == typeof(ReviveCameraFocusModule));

            SetValueInField(_rootGroupView, "IsKillingInstantly", true);
        
            SetValueInField(_rootGroupView, "EndPoint", _endPoint);
            
            SetValueInField(reviveCameraModule, "_lookAtTarget", _cameraTargetPoint);

            _rootGroupView.Modules.AddRange(modules);
            
            _rootGroupView.Modules.RemoveAt(0);
            
            _rootGroupView.PreInitialize();
        }
    
        private void SetValueInField<T, TK>(T objectToSetProperty, string nameField, TK valueToSet)
        {
            var type = objectToSetProperty.GetType();
                
            var propertyInfo = type.GetProperty(nameField, 
                                                BindingFlags.Public 
                                              | BindingFlags.Instance 
                                              | BindingFlags.NonPublic);
        
            propertyInfo?.SetValue(objectToSetProperty, valueToSet);
            
            var fieldInfo = type.GetField(nameField, 
                                                BindingFlags.Public 
                                              | BindingFlags.Instance 
                                              | BindingFlags.NonPublic);
        
            fieldInfo?.SetValue(objectToSetProperty, valueToSet);
        }
    }
}