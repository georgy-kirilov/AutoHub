namespace AutoHub.Web.ViewModels.Adverts
{
    using System.ComponentModel.DataAnnotations;

    using AutoHub.Common;
    using AutoHub.Common.Attributes;
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

        [ManufactureYear]
        [Display(Name = "Година")]
        public int ManufactureYear { get; set; }

        [Range(1, 12, ErrorMessage = "Невалиден месец")]
        [Display(Name = "Месец")]
        public int ManufactureMonth { get; set; }

        [Required]
        [Display(Name = "Пробег")]
        public long Kilometrage { get; set; }

        [HorsePowersRange(1, ValidationConstraints.MaxHorsePowers)]
        [Display(Name = "Конски сили")]
        public int HorsePowers { get; set; }

        [Required]
        [Display(Name = "Металик")]
        public bool IsExteriorMetallic { get; set; }

        [Required]
        [Display(Name = "Нов внос")]
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

        public IEnumerable<SelectListItem> MonthItems { get; set; }

        public IEnumerable<SelectListItem> YearItems { get; set; }
    }
}
