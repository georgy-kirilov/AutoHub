namespace AutoHub.Services.Data
{
    using AutoHub.Data.Common.Repositories;
    using AutoHub.Data.Models;

    public class TownsService : ITownsService
    {
        private readonly IDeletableRepository<Region> regionsRepository;
        private readonly IDeletableRepository<Town> townsRepository;

        public TownsService(
            IDeletableRepository<Region> regionsRepository,
            IDeletableRepository<Town> townsRepository)
        {
            this.regionsRepository = regionsRepository;
            this.townsRepository = townsRepository;
        }

        public async Task AddTownAsync(string regionName, string townName)
        {
            Region region = this.regionsRepository.All().FirstOrDefault(r => r.Name == regionName);

            if (region == null)
            {
                return;
            }

            Town town = region.Towns.FirstOrDefault(t => t.Name == townName)
                ?? new Town { Name = townName, RegionId = region.Id };

            await this.townsRepository.AddAsync(town);
            await this.townsRepository.SaveChangesAsync();
        }
    }
}
