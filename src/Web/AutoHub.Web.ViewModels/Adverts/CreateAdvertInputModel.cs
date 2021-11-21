namespace AutoHub.Web.ViewModels.Adverts
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateAdvertInputModel
    {
        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Цена")]
        public decimal? Price { get; set; }

        [Range(1886, 2021)]
        [Display(Name = "Година")]
        public int ManufactureYear { get; set; }

        [Range(1, 12)]
        [Display(Name = "Месец")]
        public int ManufactureMonth { get; set; }

        [Required]
        [Display(Name = "Пробег")]
        public long Kilometrage { get; set; }

        [Required]
        [Display(Name = "Конски сили")]
        public int HorsePowers { get; set; }

        [Required]
        public bool IsExteriorMetallic { get; set; }

        [Required]
        public bool IsNewImport { get; set; }

        [Required]
        [Display(Name = "Модел")]
        public int ModelId { get; set; }

        [Required]
        [Display(Name = "Двигател")]
        public int EngineId { get; set; }

        [Required]
        [Display(Name = "Тип")]
        public int BodyStyleId { get; set; }

        [Required]
        [Display(Name = "Скоростна кутия")]
        public int TransmissionId { get; set; }

        [Required]
        [Display(Name = "Цвят")]
        public int ColorId { get; set; }

        [Required]
        [Display(Name = "Населено място")]
        public int TownId { get; set; }

        [Required]
        [Display(Name = "Състояние")]
        public int ConditionId { get; set; }

        [Required]
        [Display(Name = "Евро стандарт")]
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
