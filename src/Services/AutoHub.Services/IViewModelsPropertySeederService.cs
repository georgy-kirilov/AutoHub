namespace AutoHub.Services
{
    using AutoHub.Web.ViewModels.Adverts;

    public interface IViewModelsPropertySeederService
    {
        void SeedCreateAdvertInputModel(CreateAdvertInputModel input);
    }
}
