﻿namespace AutoHub.Data.Seeding
{
    using AutoHub.Data.Seeding.MandatoryEntityValues;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class AppDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(AppDbContextSeeder));

            var seeders = new List<ISeeder>
            {
                new RolesSeeder(),
                new EnginesSeeder(),
                new BodyStylesSeeder(),
                new TransmissionsSeeder(),
                new ConditionsSeeder(),
                new EuroStandardsSeeder(),
                new BrandsSeeder(),
                new ColorsSeeder(),
                new RegionsSeeder(),
                new ModelsSeeder(Models.ModelsByBrand),
                new TownsSeeder(Towns.TownsByRegion),
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                string message = $"Seeder {seeder.GetType().Name} done.";
                logger.LogInformation(message);
            }
        }
    }
}
