﻿namespace AutoHub.Web.Controllers
{
    using AutoHub.Common;
    using AutoHub.Common.Exceptions;
    using AutoHub.Data.Common.Repositories;
    using AutoHub.Data.Models;
    using AutoHub.Services;
    using AutoHub.Services.Data;
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
            IDateTimeService dateTimeService)
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
        }

        public IActionResult ById(string id)
        {
            throw new NotImplementedException(nameof(this.ById));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var brands = this.brandsRepository.All()
                .OrderBy(b => b.Name).Select(b => new { b.Id, b.Name }).ToList();

            var brandGroupsByLetter = brands
                .Select(b => b.Name.First().ToString())
                .Distinct()
                .Select(name => new SelectListGroup { Name = name })
                .ToDictionary(g => g.Name, g => g);

            var regions = this.regionsRepository.All()
                .OrderBy(r => r.Name).Select(r => new { r.Id, r.Name }).ToList();

            var regionGroupsByLetter = regions.Select(r => r.Name.First().ToString())
                .Distinct()
                .Select(name => new SelectListGroup { Name = name })
                .ToDictionary(g => g.Name, g => g);

            var cultureInfo = this.Request.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture;

            var monthItems = this.dateTimeService.GetMonthNamesAndNumbers(cultureInfo)
                .Select(m => new SelectListItem(m.Key, m.Value.ToString()));

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

                BrandItems = brands
                    .Select(b => new SelectListItem
                    {
                        Text = b.Name,
                        Value = b.Id.ToString(),
                        Group = brandGroupsByLetter[b.Name.First().ToString()],
                    }),

                RegionItems = regions
                    .Select(r => new SelectListItem
                    {
                        Text = r.Name,
                        Value = r.Id.ToString(),
                        Group = regionGroupsByLetter[r.Name.First().ToString()],
                    }),

                ConditionItems = this.conditionsRepository.All()
                    .ToList().Select(c => new SelectListItem(c.Type, c.Id.ToString())),

                MonthItems = monthItems,

                YearItems = Enumerable.Range(
                        ValidationConstraints.MinManufactureYear,
                        DateTime.UtcNow.Year - ValidationConstraints.MinManufactureYear)
                    .OrderByDescending(x => x)
                    .Select(x => new SelectListItem(x.ToString(), x.ToString())),
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
