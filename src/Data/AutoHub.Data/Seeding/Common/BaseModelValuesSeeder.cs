namespace AutoHub.Data.Seeding.Common
{
    public abstract class BaseModelValuesSeeder<TEntity, TValue> : ISeeder
        where TEntity : class
    {
        private readonly IEnumerable<TValue> values;
        private readonly Func<TEntity, TValue, bool> existenceChecker;
        private readonly Func<TValue, TEntity> entityMaker;

        protected BaseModelValuesSeeder(
            IEnumerable<TValue> values,
            Func<TEntity, TValue, bool> existenceCondition,
            Func<TValue, TEntity> entityMaker)
        {
            this.values = values;
            this.existenceChecker = existenceCondition;
            this.entityMaker = entityMaker;
        }

        public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
        {
            var dbSet = dbContext.Set<TEntity>();

            foreach (TValue value in this.values)
            {
                if (dbSet.AsEnumerable().Any(x => this.existenceChecker.Invoke(x, value)))
                {
                    continue;
                }

                var entity = this.entityMaker.Invoke(value);
                await dbSet.AddAsync(entity);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
