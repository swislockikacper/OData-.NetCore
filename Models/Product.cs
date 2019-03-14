using System.ComponentModel.DataAnnotations;

namespace OData.NetCore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
