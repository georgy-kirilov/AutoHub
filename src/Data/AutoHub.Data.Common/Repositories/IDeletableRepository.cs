namespace AutoHub.Data.Common.Repositories
{
    using AutoHub.Data.Common.Models;

    public interface IDeletableRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletable
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
