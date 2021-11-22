namespace AutoHub.Data.Seeding
{
    using AutoHub.Services.Data;

    using Microsoft.Extensions.DependencyInjection;

    public class TownsSeeder : ISeeder
    {
        private readonly IEnumerable<KeyValuePair<string, List<string>>> townsByRegion;

        public TownsSeeder(IEnumerable<KeyValuePair<string, List<string>>> townsByRegion)
        {
            this.townsByRegion = townsByRegion;
        }

        public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
        {
            var townsService = serviceProvider.GetRequiredService<ITownsService>();

            foreach (var region in this.townsByRegion)
            {
                foreach (string town in region.Value)
                {
                    await townsService.AddTownAsync(region.Key, town);
                }
            }
        }
    }
}
