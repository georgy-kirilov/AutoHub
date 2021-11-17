namespace AutoHub.Data.Models.Base
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseTypeableOneToManyAdvertsModel : BaseOneToManyAdvertsModel
    {
        [Required]
        public string Type { get; set; }
    }
}
