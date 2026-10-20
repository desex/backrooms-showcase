#if UNITY_EDITOR
using System.Linq;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.PrefabMatrix;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Utils.Chunks
{
    [HideReferenceObjectPicker]
    public sealed class WallViewMatrix : IPrefabMatrix<ChunkWallView>
    {
        [ValidateInput("@PrefabMatrixUtils.ValidateMatrix<ChunkWallView>(this, PrefabMatrix)")]
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
            PrefabMatrixUtils.DeleteLastRow(this);
        }

        public ChunkWallView[] ToArray()
        {
            return PrefabMatrix.Cast<GameObject>()
                               .Where(wall => wall != null)
                               .Select(wall => wall.GetComponent<ChunkWallView>())
                               .ToArray();
        }
    }
}
#endif