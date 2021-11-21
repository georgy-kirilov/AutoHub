namespace AutoHub.Web.ViewModels.Adverts
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateAdvertInputModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        [Range(1886, 2021)]
        public int ManufactureYear { get; set; }

        [Range(1, 12)]
        public int ManufactureMonth { get; set; }

        [Required]
        public long Kilometrage { get; set; }

        [Required]
        public bool IsExteriorMetallic { get; set; }

        [Required]
        public bool IsNewImport { get; set; }

        [Required]
        public int HorsePowers { get; set; }

        [Required]
        public int ModelId { get; set; }

        [Required]
        public int EngineId { get; set; }

        [Required]
        public int BodyStyleId { get; set; }

        [Required]
        public int TransmissionId { get; set; }

        [Required]
        public int ColorId { get; set; }

        [Required]
        public int TownId { get; set; }

        [Required]
        public int ConditionId { get; set; }

        [Required]
        public int EuroStandardId { get; set; }

        public IEnumerable<SelectListItem> EngineItems { get; set; }

        public IEnumerable<SelectListItem> TransmissionItems { get; set; }

        public IEnumerable<SelectListItem> BodyStyleItems { get; set; }

        public IEnumerable<SelectListItem> ColorItems { get; set; }

        public IEnumerable<SelectListItem> ConditionItems { get; set; }

        public IEnumerable<SelectListItem> BrandItems { get; set; }

        public IEnumerable<SelectListItem> RegionItems { get; set; }

    }
}
