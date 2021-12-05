using System.Collections.Generic;

namespace Archive.Logic.Interfaces
{
    /// <summary>
    /// Интерфейс преобраования документа <see cref="ITextDocument"/> в объект <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности баз данных.</typeparam>
    public interface IDocumentMapper<TEntity>
    {
        /// <summary>
        /// Преобразует коллекцию объектов <see cref="ITextDocument"/> в коллекцию объектов <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="textDocuments">Коллекция документов, которую нужно преобразовать.</param>
        /// <returns>Коллекция <see cref="List{T}"/>, где T - <typeparamref name="TEntity"/>.</returns>
        List<TEntity> Map(List<ITextDocument> textDocuments);
    }
}
