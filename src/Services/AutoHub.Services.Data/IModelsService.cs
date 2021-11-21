namespace AutoHub.Services.Data
{
    public interface IModelsService
    {
        Task AddModelAsync(string brandName, string modelName);
    }
}
