namespace DigitalHouseSystemApi.Helpers
{
    public class ProductParams : PaginationParams
    {
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public ICollection<int>? BrandIds { get; set; }
    }
}
