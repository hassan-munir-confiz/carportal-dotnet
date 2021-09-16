namespace carportal.Models.Dtos
{
    public class UpdateCarDto
    {

        public int id { get; set; } = 1;
        public string description { get; set; } = "My Car Desc";

        public string name { get; set; } = "BMW";

        public int price { get; set; } = 123;

        public int rating { get; set; } = 4;

        public string imageUrl { get; set; } = "url";

        public string brand { get; set; } = "Toyota";
        
    }
}