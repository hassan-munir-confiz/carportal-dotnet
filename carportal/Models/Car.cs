using System;
using System.Collections.Generic;

namespace carportal.Models
{
    public class Car
    {
        public int id { get; set; }
        public string description { get; set; } = "My Car Desc";

        public string name { get; set; } = "BMW";

        public int price { get; set; } = 123;

        public int rating { get; set; } = 4;

        public string imageUrl { get; set; } = "url";

        public string brand { get; set; } = "Toyota";

        public static implicit operator List<object>(Car v)
        {
            throw new NotImplementedException();
        }
    }
}   