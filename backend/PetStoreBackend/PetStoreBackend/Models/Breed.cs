using System.ComponentModel.DataAnnotations;

namespace PetStoreBackend.Models
{
    public class Breed
    {
        [Key]
        public int Id { get; set; }

        public string BreedName { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = null!;

    }
}
