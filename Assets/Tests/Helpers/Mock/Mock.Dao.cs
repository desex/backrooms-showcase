using PdUtils.Dao;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class Dao<T> : IDao<T>
        {
            private T _value;

            public Dao()
            {
            }

            public Dao(T value)
            {
                _value = value;
            }

            public void Save(T entity)
            {
                _value = entity;
            }

            public T Load()
            {
                return _value;
            }

            public void Remove()
            {
                _value = default;
            }
        }
    }
}