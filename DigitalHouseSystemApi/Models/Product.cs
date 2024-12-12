namespace DigitalHouseSystemApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int Price { get; set; }
        public string Description { get; set; }
        public Photo? Photo { get; set; }

       // Kluc kon photo;
       //Kluc kon category;
       // najverovatno i do shopping card

    }
}
