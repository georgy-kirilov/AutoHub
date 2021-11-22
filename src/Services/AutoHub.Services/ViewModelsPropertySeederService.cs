namespace AutoHub.Services
{
    using System.Globalization;

    using AutoHub.Common;
    using AutoHub.Data.Common.Repositories;
    using AutoHub.Data.Models;
    using AutoHub.Web.ViewModels.Adverts;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ViewModelsPropertySeederService : IViewModelsPropertySeederService
    {
        private readonly IDeletableRepository<Engine> enginesRepository;
        private readonly IDeletableRepository<Transmission> transmissionsRepository;
        private readonly IDeletableRepository<BodyStyle> bodyStylesRepository;
        private readonly IDeletableRepository<Color> colorsRepository;
        private readonly IDeletableRepository<Condition> conditionsRepository;
        private readonly IDeletableRepository<Brand> brandsRepository;
        private readonly IDeletableRepository<Region> regionsRepository;
        private readonly IDeletableRepository<EuroStandard> euroStandardsRepository;
        private readonly IDateTimeService dateTimeService;

        public ViewModelsPropertySeederService(
            IDeletableRepository<Engine> enginesRepository,
            IDeletableRepository<Transmission> transmissionsRepository,
            IDeletableRepository<BodyStyle> bodyStylesRepository,
            IDeletableRepository<Color> colorsRepository,
            IDeletableRepository<Condition> conditionsRepository,
            IDeletableRepository<Brand> brandsRepository,
            IDeletableRepository<Region> regionsRepository,
            IDeletableRepository<EuroStandard> euroStandardsRepository,
            IDateTimeService dateTimeService)
        {
            this.enginesRepository = enginesRepository;
            this.transmissionsRepository = transmissionsRepository;
            this.bodyStylesRepository = bodyStylesRepository;
            this.colorsRepository = colorsRepository;
            this.conditionsRepository = conditionsRepository;
            this.brandsRepository = brandsRepository;
            this.regionsRepository = regionsRepository;
            this.euroStandardsRepository = euroStandardsRepository;
            this.dateTimeService = dateTimeService;
        }

        public void SeedCreateAdvertInputModel(CreateAdvertInputModel input)
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

            var cultureInfo = new CultureInfo("en-US");

            var monthItems = this.dateTimeService.GetMonthNamesAndNumbers(cultureInfo)
                .Select(m => new SelectListItem(m.Key, m.Value.ToString()));

            var yearItems = Enumerable
                .Range(
                    ValidationConstraints.MinManufactureYear,
                    DateTime.UtcNow.Year - ValidationConstraints.MinManufactureYear)
                .OrderByDescending(x => x)
                .Select(x => new SelectListItem(x.ToString(), x.ToString()));

            input.EngineItems = this.enginesRepository.All()
                .ToList().Select(e => new SelectListItem(e.Type, e.Id.ToString()));

            input.TransmissionItems = this.transmissionsRepository.All()
                .ToList().Select(t => new SelectListItem(t.Type, t.Id.ToString()));

            input.BodyStyleItems = this.bodyStylesRepository.All()
                .ToList().Select(bs => new SelectListItem(bs.Name, bs.Id.ToString()));

            input.ColorItems = this.colorsRepository.All()
                .ToList().Select(c => new SelectListItem(c.Name, c.Id.ToString()));

            input.ConditionItems = this.conditionsRepository.All()
                .ToList().Select(c => new SelectListItem(c.Type, c.Id.ToString()));

            input.BrandItems = brands.Select(b =>
                new SelectListItem
                {
                    Text = b.Name,
                    Value = b.Id.ToString(),
                    Group = brandGroupsByLetter[b.Name.First().ToString()],
                });

            input.RegionItems = regions.Select(r =>
                new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString(),
                    Group = regionGroupsByLetter[r.Name.First().ToString()],
                });

            input.EuroStandardItems = this.euroStandardsRepository.All()
                .OrderBy(es => es.Type).Select(es => new SelectListItem(es.Type, es.Id.ToString()));

            input.MonthItems = monthItems;

            input.YearItems = yearItems;
        }
    }
}
