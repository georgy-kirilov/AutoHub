namespace AutoHub.Data.Seeding.Common
{
    using AutoHub.Data.Models.Base;

    public abstract class BaseNameableModelsSeeder<TEntity> : BaseModelValuesSeeder<TEntity, string>
        where TEntity : BaseNameableOneToManyAdvertsModel, new()
    {
        protected BaseNameableModelsSeeder(IEnumerable<string> names)
            : base(
                  names,
                  (entity, name) => entity.Name == name,
                  name => new TEntity { Name = name })
        {
        }
    }
}
