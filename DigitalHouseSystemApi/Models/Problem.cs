namespace DigitalHouseSystemApi.Models
{
    public class Problem
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Context { get; set; }
        public DateTime CreatedAt { get; set; }
        public Photo? Photo { get; set; }

        public Problem(){}

        public Problem(string email, string name, string context)
        {
            Email = email;
            Name = name;
            Context = context;
            CreatedAt = DateTime.Now;
        }

    }
}
