using Db.HoleGenerator;
using Db.HoleGenerator.Impl;
using Ecs.Views.Linkable.Impl.Modules.Collectables;
using Game.Utils.HoleGenerator;
using UnityEngine;
using UnityEngine.Pool;

namespace Editor.Tools.HoleGenerator
{
    public partial class HoleGeneratorEditor
    {
        private void CreateGameObjectPoints()
        {
            var endPoint = CreateChildObject("EndPoint");
            var cameraTargetPoint = CreateChildObject("CameraTargetPoint");
            var collectablesSpawnPoints = CreateChildObject("CollectablesSpawnPoints");

            _endPoint = endPoint.transform;
            _cameraTargetPoint = cameraTargetPoint.transform;
            
            endPoint.transform.localPosition = _topMiddlePoint;
            cameraTargetPoint.transform.localPosition = _topHoleMiddlePoint;
            collectablesSpawnPoints.transform.localPosition = _bottomMiddlePoint;
        
            SetTag(endPoint, "HoleEndPoint");
            SetTag(collectablesSpawnPoints, "HoleCollectablesSpawnPoint");

            collectablesSpawnPoints.AddComponent<CollectablesSpawnModule>();
        }
    
        private void CreateGhostTriggers(GameObject wall)
        {
            var ghostTriggerJump = Instantiate(_parameters.GhostJumpTrigger, _root.transform);
            var trigger = ghostTriggerJump.GetComponentInChildren<BoxCollider>();

            var wallPosition = CalculateWallPosition(wall, 1);
            SetupVerticalTrigger(trigger, _widthHole * 2f, _heightOfTriggers, 1f);

            _eastWallPositionUp = new Vector3(wallPosition.x, 0f, wallPosition.z)
                                - new Vector3(0f, 0f, trigger.size.z / 2);

            ghostTriggerJump.transform.localPosition = _eastWallPositionUp;
        }

        private void CreatePassObstacleEffectsStartAndStop()
        {
            var effectsTrigger = Instantiate(_parameters.EffectsTriggers, _root.transform);
            var effectsFeedbackBeforeTriggers = Instantiate(_parameters.EffectsFeedbackBeforeTriggers, _root.transform);

            foreach (var effectTrigger in effectsTrigger.GetComponentsInChildren<BoxCollider>())
            {
                if (effectTrigger.CompareTag("PassObstacleEffectStart"))
                {
                    SetupVerticalTrigger(effectTrigger, _widthHole * 2, _heightOfTriggers, _lengthHole * 2);
                    effectTrigger.transform.localPosition = new Vector3(_widthHole, 0f, _lengthHole);
                }
                else if (effectTrigger.CompareTag("PassObstacleEffectStop"))
                {
                    var wallPosition = CalculateWallPosition(_westWall, 1);
                    SetupVerticalTrigger(effectTrigger, _widthHole * 2f, _heightOfTriggers, 1f);

                    _westWallPositionUp = new Vector3(wallPosition.x, 0f, wallPosition.z)
                                        + new Vector3(0f, 0f, effectTrigger.size.z / 2);

                    effectTrigger.transform.localPosition = _westWallPositionUp;
                }
            }

            effectsFeedbackBeforeTriggers.transform.localPosition = _eastWallPositionUp;

            foreach (var collider in effectsFeedbackBeforeTriggers.GetComponentsInChildren<BoxCollider>())
            {
                var currentSize = collider.size;

                collider.size = new Vector3(_eastWall.transform.localScale.z * 2f, currentSize.y, currentSize.z);
            }
        }
    
        private void CreatePassObstacle(Transform parent, Vector3 spawnPosition)
        {
            var obstacleContainer = new GameObject("HolePassObstacle");
            using var _ = ListPool<BoxCollider>.Get(out var collidersList);
            obstacleContainer.transform.SetParent(parent);
            obstacleContainer.transform.localPosition = spawnPosition + _offsetForPassObstacles;
            SetTag(obstacleContainer, "RootHolePassObstacle");
            
            Debug.Assert(_parameters.PassObstacleHole[_holeType].Count != 0, "PassObstacleHole list is empty!");
        
            var firstObstacleConfig = _parameters.PassObstacleHole[_holeType][0];
            var firstObstacle = Instantiate(firstObstacleConfig.Prefab, obstacleContainer.transform);
            var previousCollider = GetOrAddBoxCollider(firstObstacle);

            SetupStaticCollider(previousCollider, firstObstacleConfig.Depth);

            collidersList.Add(previousCollider);
        
            for (var i = 1; i < _parameters.PassObstacleHole[_holeType].Count; i++)
            {
                var currentConfig = _parameters.PassObstacleHole[_holeType][i];
                var newObstacle = Instantiate(currentConfig.Prefab, obstacleContainer.transform);
                
                switch (newObstacle.tag)
                {
                    case "HolePassObstacle":
                        previousCollider = ProcessHolePassObstacle(newObstacle, previousCollider, currentConfig);
                        break;
                    case "HoleWater":
                        ProcessHoleWater(newObstacle, currentConfig);
                        break;
                    default:
                        previousCollider = ProcessObjectsIncludedInHole(newObstacle, previousCollider, currentConfig);
                        break;
                }

                if (!previousCollider.CompareTag("HoleWater"))
                {
                    collidersList.Add(previousCollider);
                }
            }

            for (var j = 0; j < _parameters.PassObstacleHole[_holeType].Count; j++)
            {
                var currentConfig = _parameters.PassObstacleHole[_holeType][j];
                
                CalculateWidthAndLengthOffset(collidersList[j], currentConfig.OffsetWidth, currentConfig.OffsetHeight);
            }
        }

        private BoxCollider ProcessHolePassObstacle(GameObject obstacle,
                                                    BoxCollider previousCollider,
                                                    ObstaclePassSettingsParameters config
        )
        {
            var targetCollider = obstacle.GetComponentInChildren<BoxCollider>(includeInactive: true);
            
            if (targetCollider == null || !targetCollider.CompareTag("HolePassObstacleReadyTrigger"))
            {
                Debug.Assert(
                    targetCollider != null || targetCollider.CompareTag("HolePassObstacleReadyTrigger"), 
                    "HolePassObstacleReadyTrigger not found in HolePassObstacle!"
                );
                return previousCollider;
            }

            var originalColliderSize = targetCollider.size;
            SetupStaticCollider(targetCollider, config.Depth);
        
            var verticalOffset = (previousCollider.size.y / 2 + targetCollider.size.y / 2) + config.DistanceBetweenObjects;
            obstacle.transform.localPosition = previousCollider.center + Vector3.down * verticalOffset;

            var sizeDifference = originalColliderSize - targetCollider.size;
            var halfSizeDiff = sizeDifference * 0.5f;

            foreach (Transform child in obstacle.transform)
            {
                if (child == null || child == targetCollider.transform)
                    continue;

                if (Mathf.Approximately(child.localPosition.z, 0f))
                {
                    child.localPosition = new Vector3(
                        child.localPosition.x,
                        child.localPosition.y - halfSizeDiff.y,
                        child.localPosition.z
                    );
                }
                else
                {
                    child.localPosition = new Vector3(
                        child.localPosition.x,
                        child.localPosition.y - halfSizeDiff.y,
                        child.localPosition.z - halfSizeDiff.z
                    );
                }
            }
 
            return targetCollider;
        }

        private BoxCollider ProcessObjectsIncludedInHole(GameObject obstacle,
                                                         BoxCollider previousCollider,
                                                         ObstaclePassSettingsParameters config
        )
        {
            var currentCollider = GetOrAddBoxCollider(obstacle);

            if (previousCollider.CompareTag("HolePassObstacleReadyTrigger"))
            {
                SetupStaticCollider(currentCollider, config.Depth);

                var verticalOffset =
                    CalculateVerticalOffset(previousCollider, currentCollider, config.DistanceBetweenObjects);
                
                currentCollider.center = previousCollider.center + new Vector3(0f, verticalOffset, 0f);
            }
            else
            {
                SetupStaticCollider(currentCollider, config.Depth);
                var verticalOffset = (previousCollider.size.y / 2 + currentCollider.size.y / 2)
                                   + config.DistanceBetweenObjects;
                currentCollider.center = previousCollider.center + Vector3.down * verticalOffset;
            }

            return currentCollider;
        }
        
        private void ProcessHoleWater(GameObject obstacle, ObstaclePassSettingsParameters config)
        {
            var collider = obstacle.GetComponentInChildren<BoxCollider>();
            var colliderSizeY = collider.size.y;
            
            SetupStaticCollider(collider, config.Depth);
            
            collider.size = new Vector3(collider.size.x, colliderSizeY, collider.size.z);

            var waterModulePosition = new Vector3(
                obstacle.transform.position.x,
                obstacle.transform.position.y - _waterOffsetFromTop,
                obstacle.transform.position.z
            ); 
            
            obstacle.transform.position = waterModulePosition;
        }
    
        private BoxCollider GetOrAddBoxCollider(GameObject gameObject)
        {
            var collider = gameObject.GetComponent<BoxCollider>();
            return collider != null ? collider : gameObject.AddComponent<BoxCollider>();
        }
    
        private void SetupStaticCollider(BoxCollider collider, float depth)
        {
            collider.size = new Vector3(_widthHole * 2, depth, _lengthHole * 2);
            collider.center = Vector3.zero;
        }
    
        private void SetupVerticalTrigger(BoxCollider trigger, float width, float height, float depth)
        {
            trigger.size = new Vector3(width, height, depth);
            trigger.center = new Vector3(0f, height / 2, 0f);
        }

        private float CalculateVerticalOffset(BoxCollider upperCollider, BoxCollider lowerCollider, float distanceBetween)
        {
            return
                 - (upperCollider.size.y / 2 + lowerCollider.size.y)
                 - distanceBetween
                 - upperCollider.size.y / 2;
        }

        private void CalculateWidthAndLengthOffset(BoxCollider collider, float widthOffset, float lengthOffset)
        {
            var currentSize = collider.size;
            var currentCenter = collider.center;
            var newSize = new Vector3(currentSize.x + widthOffset, currentSize.y, currentSize.z + lengthOffset);
            var newCenter = new Vector3(currentCenter.x + widthOffset / 2, currentCenter.y, currentCenter.z + lengthOffset / 2);
            collider.center = newCenter;
            collider.size = newSize;
            collider.transform.localPosition = new Vector3(-widthOffset, collider.transform.localPosition.y, -lengthOffset);
        }
    
        private GameObject CreateChildObject(string nameObject)
        {
            var obj = new GameObject(nameObject);
            obj.transform.SetParent(_root.transform);
            return obj;
        }

        private void CreateWaterObject()
        {
            GameObject chosenWaterPrefab = null;
            
            foreach (var generatorPrefabVo in _prefabsParameters.GetPrefab(EGeneratorPrefab.Water))
            {
                if (generatorPrefabVo.LevelType != _levelType)
                    continue;

                chosenWaterPrefab = generatorPrefabVo.Prefabs[0];
            }

            if (!chosenWaterPrefab)
            {
                Debug.LogError($"[{nameof(HoleGeneratorEditor)}.Fillers]: No prefab found for level type {_levelType}");
                
                return;
            }
            
            _waterObject = Instantiate(chosenWaterPrefab, _root.transform);
        }
    }
}