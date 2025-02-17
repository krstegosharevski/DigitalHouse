using System.Text.Json;
using System.Text;
using DigitalHouseSystemApi.Helpers;
using Microsoft.Extensions.Options;

namespace DigitalHouseSystemApi.Services.Impl
{
    public class PerspectiveService : IPerspectiveService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public PerspectiveService(IOptions<PerspectiveApiSettings> settings)
        {
            _httpClient = new HttpClient();
            _apiKey = settings.Value.Key; 
        }

        public async Task<float?> AnalyzeTextAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;

            var requestBody = new
            {
                comment = new { text },
                languages = new[] { "en" },
                requestedAttributes = new { TOXICITY = new { } }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"https://commentanalyzer.googleapis.com/v1alpha1/comments:analyze?key={_apiKey}", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            return doc.RootElement
                .GetProperty("attributeScores")
                .GetProperty("TOXICITY")
                .GetProperty("summaryScore")
                .GetProperty("value")
                .GetSingle();
        }
    }
}
