using System.Collections.Generic;

namespace Archive.Data.Interfaces
{
    /// <summary>
    /// Инерфейс, предоставляющий доступ к данным выбранной сущности.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности базы данных.</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Добавляет в контекст всю коллекцию сущностей типа <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entities">Сущности, которые будут добавлены в контекст базы данных.</param>
        void Add(IEnumerable<TEntity> entities);

        /// <summary>
        /// Обновляет все объекты <see cref="TEntity"/> в кнтексте базы данных.
        /// </summary>
        /// <param name="entities">Коллекция сущностей для обновления.</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Возвращает все объекты сущности.
        /// </summary>
        /// <returns>Коллекция <see cref="IEnumerable{T}"/> объектов <typeparamref name="TEntity"/>.</returns>
        IEnumerable<TEntity> GetAll();
    }
}
