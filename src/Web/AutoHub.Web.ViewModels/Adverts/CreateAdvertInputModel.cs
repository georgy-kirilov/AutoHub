namespace AutoHub.Web.ViewModels.Adverts
{
    using System.ComponentModel.DataAnnotations;

    using AutoHub.Common;
    using AutoHub.Common.Attributes;
    using AutoHub.Web.LanguageResources;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateAdvertInputModel
    {
        [Display(
            Name = nameof(Resource.TitleDisplayName),
            ResourceType = typeof(Resource))]
        [Required(
            ErrorMessageResourceName = nameof(Resource.TitleIsRequired),
            ErrorMessageResourceType = typeof(Resource))]
        public string Title { get; set; }

        [Display(
            Name = nameof(Resource.DescriptionDisplayName),
            ResourceType = typeof(Resource))]
        public string Description { get; set; }

        [Display(
            Name = nameof(Resource.PriceDisplayName),
            ResourceType = typeof(Resource))]
        public decimal? Price { get; set; }

        [ManufactureYear]
        [Display(
            Name = nameof(Resource.ManufactureYearDisplayName),
            ResourceType = typeof(Resource))]
        public int ManufactureYear { get; set; }

        [Display(
            Name = nameof(Resource.ManufactureMonthDisplayName),
            ResourceType = typeof(Resource))]
        [Range(
            minimum: 1,
            maximum: 12,
            ErrorMessageResourceName = nameof(Resource.InvalidMonthErrorMessage),
            ErrorMessageResourceType = typeof(Resource))]
        public int ManufactureMonth { get; set; }

        [Display(
            Name = nameof(Resource.KilometrageDisplayName),
            ResourceType = typeof(Resource))]
        [Range(
            minimum: 0,
            maximum: long.MaxValue)]
        public long Kilometrage { get; set; }

        [Display(
            Name = nameof(Resource.HorsePowersDisplayName),
            ResourceType = typeof(Resource))]
        [HorsePowersRange(
            minHorsePowers: 1,
            ValidationConstraints.MaxHorsePowers)]
        public int HorsePowers { get; set; }

        [Display(
            Name = nameof(Resource.IsExteriorMetallicDisplayName),
            ResourceType = typeof(Resource))]
        public bool IsExteriorMetallic { get; set; }

        [Display(
            Name = nameof(Resource.IsNewImportDisplayName),
            ResourceType = typeof(Resource))]
        public bool IsNewImport { get; set; }

        [Display(
            Name = nameof(Resource.ModelDisplayName),
            ResourceType = typeof(Resource))]
        public int ModelId { get; set; }

        [Display(
            Name = nameof(Resource.EngineDisplayName),
            ResourceType = typeof(Resource))]
        public int EngineId { get; set; }

        [Display(
            Name = nameof(Resource.BodyStyleDisplayName),
            ResourceType = typeof(Resource))]
        public int BodyStyleId { get; set; }

        [Display(
            Name = nameof(Resource.TransmissionDisplayName),
            ResourceType = typeof(Resource))]
        public int TransmissionId { get; set; }

        [Display(
            Name = nameof(Resource.ColorDisplayName),
            ResourceType = typeof(Resource))]
        public int ColorId { get; set; }

        [Display(
            Name = nameof(Resource.TownDisplayName),
            ResourceType = typeof(Resource))]
        public int TownId { get; set; }

        [Display(
            Name = nameof(Resource.ConditionDisplayName),
            ResourceType = typeof(Resource))]
        public int ConditionId { get; set; }

        [Display(
            Name = nameof(Resource.EuroStandardDisplayName),
            ResourceType = typeof(Resource))]
        public int EuroStandardId { get; set; }

        public IEnumerable<SelectListItem> EngineItems { get; set; }

        public IEnumerable<SelectListItem> TransmissionItems { get; set; }

        public IEnumerable<SelectListItem> BodyStyleItems { get; set; }

        public IEnumerable<SelectListItem> ColorItems { get; set; }

        public IEnumerable<SelectListItem> ConditionItems { get; set; }

        public IEnumerable<SelectListItem> BrandItems { get; set; }

        public IEnumerable<SelectListItem> RegionItems { get; set; }

        public IEnumerable<SelectListItem> MonthItems { get; set; }

        public IEnumerable<SelectListItem> YearItems { get; set; }
    }
}
