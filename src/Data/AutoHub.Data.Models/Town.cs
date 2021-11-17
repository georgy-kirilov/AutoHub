namespace AutoHub.Data.Models
{
    using AutoHub.Data.Models.Base;

    public class Town : BaseNameableOneToManyAdvertsModel
    {
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }
    }
}
