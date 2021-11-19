namespace AutoHub.Web.Controllers
{
    using AutoHub.Common;
    using AutoHub.Common.Exceptions;
    using AutoHub.Data.Common.Repositories;
    using AutoHub.Data.Models;
    using AutoHub.Services.Data;
    using AutoHub.Web.ViewModels.Adverts;
    using Microsoft.AspNetCore.Authorization;
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
            IDeletableRepository<Model> modelsRepository)
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
        }

        public IActionResult ById(string id)
        {
            throw new NotImplementedException(nameof(this.ById));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var inputModel = new CreateAdvertInputModel
            {
                EngineItems = this.enginesRepository.All()
                    .ToList().Select(e => new SelectListItem(e.Type, e.Id.ToString())),

                TransmissionItems = this.transmissionsRepository.All()
                    .ToList().Select(t => new SelectListItem(t.Type, t.Id.ToString())),

                BodyStyleItems = this.bodyStylesRepository.All()
                    .ToList().Select(bs => new SelectListItem(bs.Name, bs.Id.ToString())),

                ColorItems = this.colorsRepository.All()
                    .ToList().Select(c => new SelectListItem(c.Name, c.Id.ToString())),

                BrandItems = this.brandsRepository.All()
                    .ToList().Select(b => new SelectListItem(b.Name, b.Id.ToString())),

                RegionItems = this.regionsRepository.All()
                    .ToList().Select(r => new SelectListItem(r.Name, r.Id.ToString())),

                ConditionItems = this.conditionsRepository.All()
                    .ToList().Select(c => new SelectListItem(c.Type, c.Id.ToString())),
            };

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateAdvertInputModel input)
        {
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

        [HttpPost]
        public ActionResult<IEnumerable<string>> GetModelsByBrand([FromBody] GetModelsByBrandInputModel input)
        {
            var models = this.modelsRepository.All().Where(m => m.BrandId == input.BrandId).ToList();
            return this.Ok(models);
        }

        [HttpPost]
        public ActionResult<IEnumerable<string>> GetTownsByRegion([FromBody] GetTownsByRegionInputModel input)
        {
            var towns = this.townsRepository.All().Where(t => t.RegionId == input.RegionId).ToList();
            return this.Ok(towns);
        }
    }
}
