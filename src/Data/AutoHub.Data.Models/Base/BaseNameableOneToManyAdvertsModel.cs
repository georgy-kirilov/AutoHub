namespace AutoHub.Data.Models.Base
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseNameableOneToManyAdvertsModel : BaseOneToManyAdvertsModel
    {
        [Required]
        public string Name { get; set; }
    }
}
