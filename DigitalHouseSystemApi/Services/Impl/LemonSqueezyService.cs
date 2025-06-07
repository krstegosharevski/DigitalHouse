using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DigitalHouseSystemApi.Services.Impl
{

    public class LemonSqueezyService : ILemonSqueezyService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public LemonSqueezyService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", config["LemonSqueezy:ApiKey"]);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.api+json"); 
            _config = config;
        }

        public async Task<string> CreateCheckoutAsync(string email, decimal amount, string username)
        {
            var storeId = _config["LemonSqueezy:StoreId"];
            var variantId = _config["LemonSqueezy:ProductId"];
            var returnUrl = _config["LemonSqueezy:ReturnUrl"];

            var data = new
            {
                data = new
                {
                    type = "checkouts",
                    attributes = new
                    {
                        checkout_data = new
                        {
                            email = email,
                            custom = new
                            {
                                username = username
                            }
                        },
                        custom_price = amount * 100,
                        product_options = new
                        {
                            redirect_url = returnUrl
                        }
                    },
                    relationships = new
                    {
                        store = new
                        {
                            data = new
                            {
                                type = "stores",
                                id = storeId
                            }
                        },
                        variant = new
                        {
                            data = new
                            {
                                type = "variants",
                                id = variantId
                            }
                        }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/vnd.api+json"); 

            var response = await _httpClient.PostAsync("checkouts", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Lemon Squeezy API error: {responseJson}");

            dynamic result = JsonConvert.DeserializeObject(responseJson);
            return result.data.attributes.url;
        }
    }
}
