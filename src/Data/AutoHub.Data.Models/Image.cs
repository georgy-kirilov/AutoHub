namespace AutoHub.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using AutoHub.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        [Required]
        public string Path { get; set; }

        public bool IsRemote { get; set; }

        public Guid AdvertId { get; set; }

        public virtual Advert Advert { get; set; }
    }
}
