namespace Archive.Data.Interfaces
{
    /// <summary>
    /// Интерфейс, выполняющий сохранение всех изменений в контексте базы данных.
    /// </summary>
    public interface IUnitOfWork : IRepositoryFactory
    {
        /// <summary>
        /// Фиксирует все изменения в контексте в базу данных.
        /// </summary>
        /// <returns>Число затронутых сущностей.</returns>
        int SaveChanges();
    }
}
