using System.ComponentModel.DataAnnotations;

namespace ProductRegistryAPI.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Cep { get; set; }
        public string? PublicPlace { get; set; }
        public string? Complement { get; set; }
        public string? Neighborhood { get; set; }
        public string? Locality { get; set; }
        public string? Uf { get; set; }
        public long? Ibge { get; set; }
        public int? Gia { get; set; }
        public int? Ddd { get; set; }
        public int? Siafi { get; set; }
    }
}
