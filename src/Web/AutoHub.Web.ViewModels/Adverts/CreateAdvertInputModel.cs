namespace AutoHub.Web.ViewModels.Adverts
{
    using System.ComponentModel.DataAnnotations;

    using AutoHub.Common.Attributes;
    using AutoHub.Web.LanguageResources;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using static AutoHub.Common.ValidationConstraints;

    public class CreateAdvertInputModel
    {
        [Display(Name = nameof(Resource.Title))]
        [CustomStringLength(MinTitleLength, MaxTitleLength)]
        [CustomRequired]
        public string Title { get; set; }

        [Display(Name = nameof(Resource.Description))]
        [StringLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Display(Name = nameof(Resource.Price))]
        [CustomRange(MinPrice, MaxPrice)]
        public decimal? Price { get; set; }

        [ManufactureYear]
        [CustomRequired]
        [Display(Name = nameof(Resource.ManufactureYear))]
        public int ManufactureYear { get; set; }

        [CustomRequired]
        [Display(Name = nameof(Resource.ManufactureMonth))]
        [CustomRange(minimum: 1, maximum: 12, ErrorMessage = nameof(Resource.InvalidMonthErrorMessage))]
        public int ManufactureMonth { get; set; }

        [CustomRequired]
        [Display(Name = nameof(Resource.Kilometrage))]
        [CustomRange(minimum: 0, MaxKilometrage)]
        public long Kilometrage { get; set; }

        [CustomRequired]
        [Display(Name = nameof(Resource.HorsePowers))]
        [CustomRange(minimum: 1, maximum: MaxHorsePowers)]
        public int HorsePowers { get; set; }

        [CustomRequired]
        [Display(Name = nameof(Resource.IsExteriorMetallic))]
        public bool IsExteriorMetallic { get; set; }

        [CustomRequired]
        [Display(Name = nameof(Resource.IsNewImport))]
        public bool IsNewImport { get; set; }

        [CustomRequired]
        [Display(Name = nameof(Resource.HasFourDoors))]
        public bool HasFourDoors { get; set; }

        [CustomRequired]
        [Display(Name = nameof(Resource.Model))]
        public int ModelId { get; set; }

        [CustomRequired]
        [Display(Name = nameof(Resource.Engine))]
        public int EngineId { get; set; }

        [CustomRequired]
        [Display(Name = nameof(Resource.BodyStyle))]
        public int BodyStyleId { get; set; }

        [CustomRequired]
        [Display(Name = nameof(Resource.Transmission))]
        public int TransmissionId { get; set; }

        [CustomRequired]
        [Display(Name = nameof(Resource.Color))]
        public int ColorId { get; set; }

        [CustomRequired]
        [Display(Name = nameof(Resource.Town))]
        public int TownId { get; set; }

        [CustomRequired]
        [Display(Name = nameof(Resource.Condition))]
        public int ConditionId { get; set; }

        [CustomRequired]
        [Display(Name = nameof(Resource.EuroStandard))]
        public int EuroStandardId { get; set; }

        public IEnumerable<SelectListItem> EngineItems { get; set; }

        public IEnumerable<SelectListItem> TransmissionItems { get; set; }

        public IEnumerable<SelectListItem> BodyStyleItems { get; set; }

        public IEnumerable<SelectListItem> ColorItems { get; set; }

        public IEnumerable<SelectListItem> ConditionItems { get; set; }

        public IEnumerable<SelectListItem> BrandItems { get; set; }

        public IEnumerable<SelectListItem> RegionItems { get; set; }

        public IEnumerable<SelectListItem> EuroStandardItems { get; set; }

        public IEnumerable<SelectListItem> MonthItems { get; set; }

        public IEnumerable<SelectListItem> YearItems { get; set; }
    }
}
