using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductRegistryAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public UnitOfMeasure UnitOfMeasure { get; set; }
        [Required]
        public string PhotoUrl { get; set; }
    }

    public enum UnitOfMeasure
    {
        Unidade,
        Quilograma,
        Metro
    }
}
