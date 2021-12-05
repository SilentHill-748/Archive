using Microsoft.EntityFrameworkCore;

namespace Archive.Data.Interfaces
{
    /// <summary>
    /// Интерфейс, гарантирующий, что объект будет использовать один контекст данных.
    /// </summary>
    /// <typeparam name="TContext">Тип контекста данных.</typeparam>
    public interface IUnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        /// <summary>
        /// Текущий контекст базы данных.
        /// </summary>
        DbContext DbContext { get; }
    }
}
