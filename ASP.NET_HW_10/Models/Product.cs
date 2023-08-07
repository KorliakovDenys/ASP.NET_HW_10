using System.ComponentModel.DataAnnotations;

namespace ASP.NET_HW_9.Models {
    public class Product {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ushort Amount { get; set; }
    }
}