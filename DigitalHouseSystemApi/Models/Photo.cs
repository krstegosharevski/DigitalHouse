using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalHouseSystemApi.Models
{

    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
