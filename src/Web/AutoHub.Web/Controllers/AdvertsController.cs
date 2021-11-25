namespace AutoHub.Web.Controllers
{
    using AutoHub.Common;
    using AutoHub.Common.Exceptions;
    using AutoHub.Data.Common.Repositories;
    using AutoHub.Data.Models;
    using AutoHub.Services;
    using AutoHub.Services.Data;
    using AutoHub.Web.LanguageResources;
    using AutoHub.Web.ViewModels.Adverts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AdvertsController : Controller
    {
        private readonly IAdvertsService advertsService;
        private readonly IDeletableRepository<Engine> enginesRepository;
        private readonly IDeletableRepository<Transmission> transmissionsRepository;
        private readonly IDeletableRepository<BodyStyle> bodyStylesRepository;
        private readonly IDeletableRepository<Color> colorsRepository;
        private readonly IDeletableRepository<Brand> brandsRepository;
        private readonly IDeletableRepository<Region> regionsRepository;
        private readonly IDeletableRepository<Condition> conditionsRepository;
        private readonly IDeletableRepository<Town> townsRepository;
        private readonly IDeletableRepository<Model> modelsRepository;
        private readonly IDateTimeService dateTimeService;
        private readonly IViewModelsPropertySeederService viewModelsPropertySeederService;

        public AdvertsController(
            IAdvertsService advertsService,
            IDeletableRepository<Engine> enginesRepository,
            IDeletableRepository<Transmission> transmissionsRepository,
            IDeletableRepository<BodyStyle> bodyStylesRepository,
            IDeletableRepository<Color> colorsRepository,
            IDeletableRepository<Brand> brandsRepository,
            IDeletableRepository<Region> regionsRepository,
            IDeletableRepository<Condition> conditionsRepository,
            IDeletableRepository<Town> townsRepository,
            IDeletableRepository<Model> modelsRepository,
            IDateTimeService dateTimeService,
            IViewModelsPropertySeederService viewModelsPropertySeederService)
        {
            this.advertsService = advertsService;
            this.enginesRepository = enginesRepository;
            this.transmissionsRepository = transmissionsRepository;
            this.bodyStylesRepository = bodyStylesRepository;
            this.colorsRepository = colorsRepository;
            this.brandsRepository = brandsRepository;
            this.regionsRepository = regionsRepository;
            this.conditionsRepository = conditionsRepository;
            this.townsRepository = townsRepository;
            this.modelsRepository = modelsRepository;
            this.dateTimeService = dateTimeService;
            this.viewModelsPropertySeederService = viewModelsPropertySeederService;
        }

        public IActionResult ById(string id)
        {
            throw new NotImplementedException(nameof(this.ById));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var inputModel = new CreateAdvertInputModel();
            this.viewModelsPropertySeederService.SeedCreateAdvertInputModel(inputModel);
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateAdvertInputModel input)
        {
            this.viewModelsPropertySeederService.SeedCreateAdvertInputModel(input);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string advertId;

            try
            {
                Advert advert = await this.advertsService.CreateAdvertAsync(input, this.User);
                advertId = advert.Id.ToString();
            }
            catch (InvalidModelStateException ex)
            {
                this.ModelState.AddErrors(ex.ErrorsByPropertyName);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.ById), advertId);
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetModelsByBrand(GetModelsByBrandInputModel input)
        {
            var models = this.modelsRepository.All()
                .Where(m => m.BrandId == input.BrandId).Select(m => new { m.Id, m.Name }).ToList();

            return this.Ok(models);
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetTownsByRegion(GetTownsByRegionInputModel input)
        {
            var towns = this.townsRepository.All()
                .Where(t => t.RegionId == input.RegionId).Select(t => new { t.Id, t.Name }).ToList();

            return this.Ok(towns);
        }
    }
}
