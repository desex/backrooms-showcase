using System;
using System.Collections.Generic;
using Core.Utils.TriggerCollision;
using Unity.Collections;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs
{
    public class PlayerBoxList : IDisposable
    {
        private readonly Dictionary<int, BoxAction> _boxActions = new Dictionary<int, BoxAction>();
        private readonly List<BoxAction> _boxActionsList = new List<BoxAction>();
        private NativeList<BoxData> _boxDataList = new NativeList<BoxData>(Allocator.Persistent);

        public int Count => _boxActionsList.Count;

        public NativeArray<BoxData>.ReadOnly BoxesData => _boxDataList.AsArray()
                                                                      .AsReadOnly();

        public BoxActionData this[int index]
        {
            get => new BoxActionData(_boxActionsList[index], _boxDataList[index]);

            set
            {
                _boxActionsList[index] = value;
                _boxDataList[index] = value;
                _boxActions[value.Uid] = value;
            }
        }

        public void Dispose()
        {
            // Более показать не могу
        }

        public void Add(BoxActionData boxActionData)
        {
            // Более показать не могу
        }

        public bool Contains(BoxActionData boxActionData)
        {
            // Более показать не могу
            return default;
        }

        public void Clear()
        {
            // Более показать не могу
        }

        public void Remove(int boxActionDataId)
        {
            // Более показать не могу
        }
    }
}