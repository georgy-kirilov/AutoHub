namespace AutoHub.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using AutoHub.Data.Common.Models;

    public class Advert : BaseDeletableModel<Guid>
    {
        public Advert()
        {
            this.Images = new HashSet<Image>();
        }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ManufacturedOn { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }

        public long? Kilometrage { get; set; }

        public int? HorsePowers { get; set; }

        public bool? HasFourDoors { get; set; }

        public bool? IsNewImport { get; set; }

        public bool IsEuroStandardExact { get; set; }

        public bool IsExteriorMetallic { get; set; }

        public long LocalViewsCount { get; set; }

        public string RemoteId { get; set; }

        public int? RemoteProviderId { get; set; }

        public virtual RemoteProvider RemoteProvider { get; set; }

        public string AuthorId { get; set; }

        public virtual AppUser Author { get; set; }

        public int? BodyStyleId { get; set; }

        public virtual BodyStyle BodyStyle { get; set; }

        public int? BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public int? ColorId { get; set; }

        public virtual Color Color { get; set; }

        public int? ConditionId { get; set; }

        public virtual Condition Condition { get; set; }

        public int? EngineId { get; set; }

        public virtual Engine Engine { get; set; }

        public int? EuroStandardId { get; set; }

        public virtual EuroStandard EuroStandard { get; set; }

        public int? ModelId { get; set; }

        public virtual Model Model { get; set; }

        public int? RegionId { get; set; }

        public virtual Region Region { get; set; }

        public int? TownId { get; set; }

        public virtual Town Town { get; set; }

        public int? TransmissionId { get; set; }

        public virtual Transmission Transmission { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
