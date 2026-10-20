using Core.Utils;
using Ecs.Views.Linkable.Impl.Modules;
using Game.Utils;
using Game.Utils.HoleGenerator;
using UnityEngine;
using UnityEngine.Pool;

namespace Editor.Tools.HoleGenerator
{
    public partial class HoleGeneratorEditor
    {
        private void ModifyAndSaveMaterial(GameObject gameObject, Vector2 tiling)
        {
            if (gameObject.TryGetComponent(out MaterialTilingApplier materialTilingApplier))
            {
                materialTilingApplier.SetTiling(tiling);
            }
            else
            {
                var materialTilingApplierForNew = gameObject.AddComponent<MaterialTilingApplier>();
                materialTilingApplierForNew.SetTiling(tiling);
            }
        }
        
        private void AddFogApplier(GameObject gameObject)
        {
            gameObject.AddComponent<MaterialFogApplier>();
        }

        private void SubstitutionMaterialOnGameObject(GameObject gameObject, Material material)
        {
            var renderer = gameObject.GetComponentInChildren<Renderer>();

            Debug.Assert(renderer != null, "Renderer not found");

            renderer.material = material;
        }

        private void ChangeMaterialForLevel()
        {
            switch (_levelType)
            {
                case EGameLevelType.Yellow:
                    ApplyCommonLevelMaterials(_yellowMaterialsParameters);
                    break;
                case EGameLevelType.Pool:
                    ApplyPoolLevelSpecifics(_poolMaterialsParameters);
                    break;
                case EGameLevelType.Red:
                    ApplyRedLevelSpecifics();
                    break;
                case EGameLevelType.Parking:
                    Debug.Assert(false, "Level Parking is not implemented");
                    break;
                default:
                    Debug.Assert(false, "Unknown level type");
                    break;
            }
        }

        private void InitializeMaterials()
        {
            _yellowMaterialsParameters = new LevelMaterialsParameters()
            {
                HoleWall = _parameters.HoleWallYellowMaterial,
                HoleWallAttached = _parameters.HoleWallAttachedYellowMaterial,
                UpHoleWall = _parameters.UpHoleWallYellowMaterial,
                HoleFloor = _parameters.HoleFloorYellowMaterial,
                UpHoleCelling = _parameters.UpHoleCellingYellowMaterial,
            };

            _poolMaterialsParameters = new LevelMaterialsParameters()
            {
                HoleWall = _parameters.HoleWallPoolMaterial,
                HoleWallAttached = _parameters.HoleWallAttachedPoolMaterial,
                UpHoleWall = _parameters.UpHoleWallPoolMaterial,
                HoleFloor = _parameters.HoleFloorPoolMaterial,
                UpHoleCelling = _parameters.UpHoleCellingPoolMaterial,
            };
            
            _redMaterialsParameters = new LevelMaterialsParameters()
            {
                HoleWall = _parameters.HoleWallRedMaterial,
                HoleWallAttached = _parameters.HoleWallAttachedRedMaterial,
                UpHoleWall = _parameters.UpHoleWallRedMaterial,
                HoleFloor = _parameters.HoleFloorRedMaterial,
                UpHoleCelling = _parameters.UpHoleCellingRedMaterial,
            };
        }
        
        private void ApplyCommonLevelMaterials(LevelMaterialsParameters materials)
        {
            ApplyWallMaterials(materials);
            ApplyFloorMaterials(materials);
            ApplyCeilingMaterials(materials);
        }
        
        private void ApplyPoolLevelSpecifics(LevelMaterialsParameters materials)
        {
            ApplyWallMaterials(materials);
            ApplyFloorMaterials(materials);
            ApplyCeilingMaterials(materials);
            
            RemoveBaseBoards(_upperSouthWall, materials);
            RemoveBaseBoards(_upperNorthWall, materials);
            
            AddFogApplier(_root);
        }   

        private void ApplyRedLevelSpecifics()
        {
            var materials = _redMaterialsParameters;
            ApplyCommonLevelMaterials(materials);
            
            RemoveBaseBoards(_upperSouthWall, materials);
            RemoveBaseBoards(_upperNorthWall, materials);
            
            ModifyFloorMaterials();
        }

        private void ApplyWallMaterials(LevelMaterialsParameters materials)
        {
            SetMaterial(_eastWall, materials.HoleWall);
            SetMaterial(_westWall, materials.HoleWall);
            
            SetMaterial(_upperNorthWall, materials.UpHoleWall);
            SetMaterial(_upperSouthWall, materials.UpHoleWall);
            
            SetMaterial(_northWall, _isAttachedToRight ? materials.HoleWallAttached : materials.HoleWall);
            SetMaterial(_southWall, _isAttachedToLeft ? materials.HoleWallAttached : materials.HoleWall);
        }

        private void ApplyFloorMaterials(LevelMaterialsParameters materials)
        {
            SetFloorMaterialWithScale(_northFloor, materials.HoleFloor);
            
            SetFloorMaterialWithScale(_southFloor, materials.HoleFloor);
            
            SetFloorMaterialWithScale(_eastFloor, materials.HoleFloor);
            SetFloorMaterialWithScale(_westFloor, materials.HoleFloor);
        }

        private void ApplyCeilingMaterials(LevelMaterialsParameters materials)
        {
            SetMaterial(_upperCeilingSouthWall, materials.UpHoleCelling);
            SetMaterial(_upperCeilingNorthWall, materials.UpHoleCelling);
        }

        private void ModifyFloorMaterials()
        {
            ModifyFloorScale(_northFloor);
            ModifyFloorScale(_southFloor);
            ModifyFloorScale(_eastFloor);
            ModifyFloorScale(_westFloor);
        }
        
        private void SetMaterial(GameObject obj, Material material)
        {
            if (obj != null)
                SubstitutionMaterialOnGameObject(obj, material);
        }

        private void SetFloorMaterialWithScale(GameObject floor, Material material)
        {
            SetMaterial(floor, material);
            if (floor != null && _levelType == EGameLevelType.Red)
            {
                ModifyAndSaveMaterial(floor, GetFloorScale(floor));
            }
        }

        private void RemoveBaseBoards(GameObject wall, LevelMaterialsParameters materials)  
        {
            var materialsForWestWall = wall.GetComponentInChildren<MeshRenderer>().sharedMaterials;

            using var _ = ListPool<Material>.Get(out var materialsList);
            materialsList.AddRange(materialsForWestWall);
            materialsList.RemoveAtLast();
            materialsList.RemoveAtLast();
            materialsList.Add(materials.UpHoleWall);
            wall.GetComponentInChildren<MeshRenderer>().materials = materialsList.ToArray();
        }
    }
}