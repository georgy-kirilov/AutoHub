namespace AutoHub.Services.Data
{
    using AutoHub.Data.Common.Repositories;
    using AutoHub.Data.Models;

    public class ModelsService : IModelsService
    {
        private readonly IDeletableRepository<Brand> brandsRepository;
        private readonly IDeletableRepository<Model> modelsRepository;

        public ModelsService(
            IDeletableRepository<Brand> brandsRepository,
            IDeletableRepository<Model> modelsRepository)
        {
            this.brandsRepository = brandsRepository;
            this.modelsRepository = modelsRepository;
        }

        public async Task AddModelAsync(string brandName, string modelName)
        {
            Brand brand = this.brandsRepository.All().FirstOrDefault(b => b.Name == brandName);

            if (brand == null)
            {
                return;
            }

            Model model = brand.Models.FirstOrDefault(m => m.Name == modelName) 
                ?? new Model { Name = modelName, BrandId = brand.Id };

            await this.modelsRepository.AddAsync(model);
            await this.modelsRepository.SaveChangesAsync();
        }
    }
}
