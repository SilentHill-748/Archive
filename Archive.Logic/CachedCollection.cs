using System;
using System.Collections.Generic;
using System.Linq;

using Archive.Logic.Interfaces;
using Archive.Logic.Exceptions;
using System.Reflection;

namespace Archive.Logic
{
    public class CachedCollection<T> : ICachedCollection<T>
    {
        private readonly List<T> _collection;


        public CachedCollection()
        {
            _collection = new List<T>();
        }


        public List<T> Collection
        {
            get => _collection;
        }

        public void Add(T item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            _collection.Add(item);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(T item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            return _collection.Contains(item);
        }

        public T Find(object key)
        {
            for (int i = 0; i < _collection.Count; i++)
            {
                Type? itemType = _collection[i]?.GetType();

                if (itemType is not null)
                {
                    foreach (PropertyInfo property in itemType.GetProperties())
                    {
                        object? propertyValue = property.GetValue(itemType);

                        if (propertyValue is not null && 
                            propertyValue.Equals(key))
                        {
                            return _collection[i];
                        }
                    }
                }
            }

            throw new ObjectNotFoundException("Объект не найден в кеше!");
        }
    }
}
