namespace AutoHub.Data.Models.Base
{
    using AutoHub.Data.Common.Models;

    public abstract class BaseOneToManyAdvertsModel : BaseDeletableModel<int>
    {
        protected BaseOneToManyAdvertsModel()
        {
            this.Adverts = new HashSet<Advert>();
        }

        public virtual ICollection<Advert> Adverts { get; set; }
    }
}
