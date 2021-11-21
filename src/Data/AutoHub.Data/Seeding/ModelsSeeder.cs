namespace AutoHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoHub.Services.Data;
    using Microsoft.Extensions.DependencyInjection;

    public class ModelsSeeder : ISeeder
    {
        private readonly IEnumerable<KeyValuePair<string, List<string>>> modelsByBrand;

        public ModelsSeeder(IEnumerable<KeyValuePair<string, List<string>>> modelsByBrand)
        {
            this.modelsByBrand = modelsByBrand;
        }

        public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
        {
            var modelsService = serviceProvider.GetRequiredService<IModelsService>();

            foreach (var brand in this.modelsByBrand)
            {
                foreach (string model in brand.Value)
                {
                    await modelsService.AddModelAsync(brand.Key, model);
                }
            }
        }
    }
}
