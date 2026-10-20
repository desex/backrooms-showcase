#if UNITY_EDITOR
using System.Linq;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Utils.PrefabMatrix;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Utils.Obstacles
{
    [HideReferenceObjectPicker]
    public sealed class ObstaclesMatrix : IPrefabMatrix<ObstacleGroupView>
    {
        [ValidateInput("@PrefabMatrixUtils.ValidateMatrix<ObstacleGroupView>(this, PrefabMatrix)")]
        [HideReferenceObjectPicker]
        [PrefabTableMatrix]
        [SerializeField] //DONT DELETE
        private GameObject[,] _prefabMatrix = new GameObject[PrefabMatrixUtils.ROW_AMOUNT, 1];
        
        public GameObject[,] PrefabMatrix 
        {
            get => _prefabMatrix;
            set => _prefabMatrix = value;
        }
        
        [ShowIf("@PrefabMatrix.GetLength(1) > 1")]
        [Button("Delete last row")]
        private void DeleteLastRow()
        {
            // Более показать не могу
        }
        
        public ObstacleGroupView[] ToArray()
        {
            // Более показать не могу
            return global::System.Array.Empty<ObstacleGroupView>();
        }
    }
}
#endif