namespace DigitalHouseSystemApi.Services
{
    public interface IPerspectiveService
    {
        Task<float?> AnalyzeTextAsync(string text);
    }
}
