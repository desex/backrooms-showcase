using Sirenix.OdinInspector;

namespace Game.Utils.PrefabMatrix
{
    public class PrefabTableMatrixAttribute : TableMatrixAttribute
    {
        public PrefabTableMatrixAttribute()
        {
            SquareCells = true;
            HideColumnIndices = true;
            HideRowIndices = true;
            ResizableColumns = false;
        }
    }
}