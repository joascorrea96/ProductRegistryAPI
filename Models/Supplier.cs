using System.ComponentModel.DataAnnotations;

namespace ProductRegistryAPI.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CNPJ { get; set; }
        [Required]
        public Address Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public ICollection<Product> Products { get; set; }
    }
}
