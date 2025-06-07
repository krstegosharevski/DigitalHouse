namespace DigitalHouseSystemApi.Models
{
    public class LemonSqueezyCheckoutRequest
    {
        public string VariantId { get; set; }
        public string Email { get; set; }
        public string CustomDataJson { get; set; }
        public string RedirectUrl { get; set; }
    }

}
