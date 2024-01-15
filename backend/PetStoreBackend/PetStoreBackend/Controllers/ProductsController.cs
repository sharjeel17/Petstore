using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStoreBackend.Dtos;
using PetStoreBackend.Interfaces;
using PetStoreBackend.Models;

namespace PetStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IBreedRepository _breedRepository;
        public ProductsController(IProductRepository productRepository, 
            IBreedRepository breedRepository)
        {
            _productRepository = productRepository;
            _breedRepository = breedRepository;
        }


        [HttpPost("productCreation")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateProduct([FromForm] ProductDto inputProduct) 
        {
            if (inputProduct == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //make new product
            //automapper not used here
            Product product = new Product()
            {
                Animal = inputProduct.Animal,
                Name = inputProduct.Name,
                FileName = await _productRepository.SaveImageAsync(inputProduct.Image),
                Breed = await _breedRepository.GetBreed(inputProduct.Breed)
            };

            if (!await _productRepository.CreateProductAsync(product)) 
            {
                ModelState.AddModelError("Create Error", "Something went wrong while creating");
                return StatusCode(500, ModelState);

            }

            return Ok(product);
        }


        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(ICollection<ProductDto>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ICollection<ProductDto>>> GetProducts()
        {
            var products = await _productRepository.GetProductsDtoAsync(Request);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
        {
            var product = await _productRepository.GetProductAsync(id);

            if (product is null)
            {
                return NotFound("Product Not Found");
            }

            return Ok(product);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromForm] ProductDto inputProduct) 
        {
            var product = await _productRepository.GetProductAsync(id);

            if (product is null) 
            {
                return NotFound("Product Not Found");
            }

            product.Name = inputProduct.Name;
            product.Animal = inputProduct.Animal;
            product.Breed = await _breedRepository.GetBreed(inputProduct.Breed);
            product.UpdatedAt = DateTime.Now;


            if(!(inputProduct.Image is null))
            {
                Console.WriteLine("RUNS IN IMAGE AREA");
                _productRepository.DeleteImage(product.FileName);
                product.FileName = await _productRepository.SaveImageAsync(inputProduct.Image);
            }

            if (!await _productRepository.UpdateProductAsync(product)) 
            {
                ModelState.AddModelError("Update Error", "Something went wrong while updating product");
                return StatusCode(500, ModelState);
            }

            return Ok(product);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id) 
        {

            if (!await _productRepository.DeleteProductAsync(id)) 
            {
                ModelState.AddModelError("Delete Error", "Something went wrong while deleting product");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        
    }
}
