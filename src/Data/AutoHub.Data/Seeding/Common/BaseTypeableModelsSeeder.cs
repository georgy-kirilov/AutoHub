namespace AutoHub.Data.Seeding.Common
{
    using AutoHub.Data.Models.Base;

    public abstract class BaseTypeableModelsSeeder<TEntity> : BaseModelValuesSeeder<TEntity, string>
        where TEntity : BaseTypeableOneToManyAdvertsModel, new()
    {
        protected BaseTypeableModelsSeeder(IEnumerable<string> types)
            : base(
                  types,
                  (entity, type) => entity.Type == type,
                  type => new TEntity { Type = type })
        {
        }
    }
}
