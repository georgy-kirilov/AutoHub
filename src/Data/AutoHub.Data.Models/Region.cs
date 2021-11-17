namespace AutoHub.Data.Models
{
    using AutoHub.Data.Models.Base;

    public class Region : BaseNameableOneToManyAdvertsModel
    {
        public Region()
        {
            this.Towns = new HashSet<Town>();
        }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
