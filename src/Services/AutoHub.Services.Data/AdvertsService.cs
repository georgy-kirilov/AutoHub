namespace AutoHub.Services.Data
{
    using System.Security.Claims;

    using AutoHub.Common.Exceptions;
    using AutoHub.Data.Common.Models;
    using AutoHub.Data.Common.Repositories;
    using AutoHub.Data.Models;
    using AutoHub.Web.LanguageResources;
    using AutoHub.Web.ViewModels.Adverts;
    using Microsoft.AspNetCore.Identity;

    public class AdvertsService : IAdvertsService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IDeletableRepository<Advert> advertsRepository;
        private readonly IDeletableRepository<Engine> enginesRepository;
        private readonly IDeletableRepository<BodyStyle> bodyStylesRepository;
        private readonly IDeletableRepository<Color> colorsRepository;
        private readonly IDeletableRepository<Condition> conditionsRepository;
        private readonly IDeletableRepository<EuroStandard> euroStandardsRepository;
        private readonly IDeletableRepository<Model> modelsRepository;
        private readonly IDeletableRepository<Town> townsRepository;
        private readonly IDeletableRepository<Transmission> transmissionsRepository;

        public AdvertsService(
            UserManager<AppUser> userManager,
            IDeletableRepository<Advert> advertsRepository,
            IDeletableRepository<Engine> enginesRepository,
            IDeletableRepository<BodyStyle> bodyStylesRepository,
            IDeletableRepository<Color> colorsRepository,
            IDeletableRepository<Condition> conditionsRepository,
            IDeletableRepository<EuroStandard> euroStandardsRepository,
            IDeletableRepository<Model> modelsRepository,
            IDeletableRepository<Town> townsRepository,
            IDeletableRepository<Transmission> transmissionsRepository)
        {
            this.userManager = userManager;
            this.advertsRepository = advertsRepository;
            this.enginesRepository = enginesRepository;
            this.bodyStylesRepository = bodyStylesRepository;
            this.colorsRepository = colorsRepository;
            this.conditionsRepository = conditionsRepository;
            this.euroStandardsRepository = euroStandardsRepository;
            this.modelsRepository = modelsRepository;
            this.townsRepository = townsRepository;
            this.transmissionsRepository = transmissionsRepository;
        }

        public async Task<Advert> CreateAdvertAsync(CreateAdvertInputModel input, ClaimsPrincipal authorClaims)
        {
            AppUser author = await this.userManager.GetUserAsync(authorClaims);

            if (author == null)
            {
                throw new NoSuchUserException();
            }

            var errorsByPropertyName = new Dictionary<string, List<string>>();

            string errorMessageFormat = Resource.SuchItemDoesNotExistErrorMessageFormat;

            Engine engine = this.enginesRepository.All()
                .FirstOrDefault(e => e.Id == input.EngineId);

            BodyStyle bodyStyle = this.bodyStylesRepository.All()
                .FirstOrDefault(bs => bs.Id == input.BodyStyleId);

            Color color = this.colorsRepository.All()
                .FirstOrDefault(c => c.Id == input.ColorId);

            Condition condition = this.conditionsRepository.All()
                .FirstOrDefault(c => c.Id == input.ConditionId);

            EuroStandard euroStandard = this.euroStandardsRepository.All()
                .FirstOrDefault(es => es.Id == input.EuroStandardId);

            Model model = this.modelsRepository.All()
                .FirstOrDefault(m => m.Id == input.ModelId);

            Town town = this.townsRepository.All()
                .FirstOrDefault(t => t.Id == input.TownId);

            Transmission transmission = this.transmissionsRepository.All()
                .FirstOrDefault(t => t.Id == input.TransmissionId);

            var entries = new KeyValuePair<string, BaseModel<int>>[]
            {
                new(nameof(input.EngineId), engine),
                new(nameof(input.BodyStyleId), bodyStyle),
                new(nameof(input.ColorId), color),
                new(nameof(input.ConditionId), condition),
                new(nameof(input.EuroStandardId), euroStandard),
                new(nameof(input.ModelId), model),
                new(nameof(input.TownId), town),
                new(nameof(input.TransmissionId), transmission),
            };

            foreach (var entry in entries)
            {
                if (entry.Value != null)
                {
                    continue;
                }

                if (!errorsByPropertyName.ContainsKey(entry.Key))
                {
                    errorsByPropertyName.Add(entry.Key, new List<string>());
                }

                string error = string.Format(errorMessageFormat, entry.Key);
                errorsByPropertyName[entry.Key].Add(error);
            }

            if (errorsByPropertyName.Count != 0)
            {
                throw new InvalidModelStateException(errorsByPropertyName);
            }

            var advert = new Advert
            {
                Title = input.Title,
                Description = input.Description,
                ManufacturedOn = new DateTime(input.ManufactureYear, input.ManufactureMonth, 1),
                Price = input.Price == 0 ? null : input.Price,
                Kilometrage = input.Kilometrage,
                HorsePowers = input.HorsePowers,
                IsNewImport = input.IsNewImport,
                IsEuroStandardExact = true,
                IsExteriorMetallic = input.IsExteriorMetallic,
                HasFourDoors = input.HasFourDoors,
                Author = author,
                BodyStyleId = bodyStyle.Id,
                BrandId = model.BrandId,
                ColorId = color.Id,
                ConditionId = condition.Id,
                EngineId = engine.Id,
                EuroStandardId = euroStandard.Id,
                ModelId = model.Id,
                RegionId = town.RegionId,
                TownId = town.Id,
                TransmissionId = transmission.Id,
            };

            await this.advertsRepository.AddAsync(advert);
            await this.advertsRepository.SaveChangesAsync();

            return advert;
        }
    }
}
