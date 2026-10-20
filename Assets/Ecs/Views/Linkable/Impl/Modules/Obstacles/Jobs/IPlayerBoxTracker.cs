using Core.Utils.TriggerCollision;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs
{
    public interface IPlayerBoxTracker
    {
        public void AddBox(BoxActionData boxActionData);
        public void RemoveBox(int boxActionDataId);
        public void ReplaceBoxSize(int uniqueUid, Vector3 size);
    }
}