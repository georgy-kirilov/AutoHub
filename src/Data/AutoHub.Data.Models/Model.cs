namespace AutoHub.Data.Models
{
    using AutoHub.Data.Models.Base;

    public class Model : BaseNameableOneToManyAdvertsModel
    {
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }
    }
}
