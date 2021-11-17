namespace AutoHub.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using AutoHub.Data.Models.Base;

    public class RemoteProvider : BaseNameableOneToManyAdvertsModel
    {
        [Required]
        public string AdvertUrlFormat { get; set; }
    }
}
