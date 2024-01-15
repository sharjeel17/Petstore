using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PetStoreBackend.Context;
using PetStoreBackend.Dtos;
using PetStoreBackend.Interfaces;
using PetStoreBackend.Models;

namespace PetStoreBackend.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductRepository(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _webHostEnvironment = environment;
        }


        //Create product and save to database
        public async Task<bool> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            return await SaveAsync();
        }

        //Get list of all Products as ProductDto
        public async Task<ICollection<ProductDto>> GetProductsDtoAsync(HttpRequest request)
        {
            var products = await _context.Products.Select( p => new ProductDto 
            {
                Id = p.Id,
                Name = p.Name,
                Animal = p.Animal,
                Breed = _context.Breeds.Where(b => b.Id == p.Breed.Id).Select(p => p.BreedName).FirstOrDefault(),
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                ImageSrc = $"{request.Scheme}://{request.Host}{request.PathBase}/Images/{p.FileName}"

            }).OrderByDescending(p => p.UpdatedAt).ToListAsync();

            return products;
        }

        //Get list of all Products
        public async Task<ICollection<Product>> GetProductsAsync()
        {
            return await _context.Products.OrderByDescending(p => p.UpdatedAt).ToListAsync();
        }

        //Get singular product from Product ID
        public async Task<Product> GetProductAsync(int Id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == Id);
        }

        //Update Product
        public async Task<bool> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            return await SaveAsync();
        }

        //Delete Product with matching Product ID
        public async Task<bool> DeleteProductAsync(int Id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == Id);
            DeleteImage(product.FileName);
            _context.Products.Remove(product);
            return await SaveAsync();
        }

        //Save to database for Create, Update and Delete
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        //Save file and return file name
        public async Task<string> SaveImageAsync(IFormFile formFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(formFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(formFile.FileName);
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

        public void DeleteImage(string imageName) 
        {
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);
            if(System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            return;
        }

        public async Task ProductDtoToProduct(ProductDto productDto, Product product) 
        {

            return;
        }
    }
}
