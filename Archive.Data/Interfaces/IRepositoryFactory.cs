namespace Archive.Data.Interfaces
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> Getrepository<TEntity>() where TEntity : class;
    }
}
