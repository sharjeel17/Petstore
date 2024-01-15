using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetStoreBackend.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [StringLength(60)]
        public string Animal { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public Breed Breed { get; set; } = null!;

        //This is the FileName of the file/image, not its path.
        //The path is calculated in the repository.
        [MaxLength(300)]
        public string FileName { get; set; } = null!;
    }
}
