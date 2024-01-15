using System.ComponentModel.DataAnnotations;

namespace PetStoreBackend.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [StringLength(60)]
        public string Animal { get; set; } = null!;

        public string Breed { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //ImageSrc is the file path to the image in the server
        public string? ImageSrc { get; set; }

        //Image is the actual image in the form of a file
        public IFormFile? Image { get; set; }
    }
}
