using System.Collections.Generic;
using System;

using Archive.Logic.Exceptions;

namespace Archive.Logic.Interfaces
{
    /// <summary>
    /// Интерфейс кеширования документов в ОЗУ компьютера.
    /// </summary>
    /// <typeparam name="T">Тип кешируемых объектов.</typeparam>
    public interface ICachedCollection<T>
    {
        /// <summary>
        /// Кешированная коллекция объектов.
        /// </summary>
        List<T> Collection { get; }

        /// <summary>
        /// Добавляет в коллекцию новый объект.
        /// </summary>
        /// <param name="item">Кешируемый объект.</param>
        /// <exception cref="ArgumentNullException"/>
        void Add(T item);
        /// <summary>
        /// Выполняет проверку, есть ли указанный объект в коллекции.
        /// </summary>
        /// <param name="item">Объект, который надо проверить на наличие.</param>
        /// <returns><c>True</c>, если объект есть в коллекции, иначе <c>False</c>.</returns>
        bool Contains(T item);
        /// <summary>
        /// Выполняет поиск документа по указанному ключу.
        /// </summary>
        /// <param name="Key">Ключ, по которому будет вестись поиск.</param>
        /// <returns>Найденный объект.</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ObjectNotFoundException"/>
        T Find(object Key);
        /// <summary>
        /// Выполняет полную очистку коллекции.
        /// </summary>
        void Clear();
    }
}
