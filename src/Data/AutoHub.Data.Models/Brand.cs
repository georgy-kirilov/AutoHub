namespace AutoHub.Data.Models
{
    using AutoHub.Data.Models.Base;

    public class Brand : BaseNameableOneToManyAdvertsModel
    {
        public Brand()
        {
            this.Models = new HashSet<Model>();
        }

        public virtual ICollection<Model> Models { get; set; }
    }
}
