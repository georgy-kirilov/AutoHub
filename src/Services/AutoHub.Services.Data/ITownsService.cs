namespace AutoHub.Services.Data
{
    public interface ITownsService
    {
        Task AddTownAsync(string regionName, string townName);
    }
}
