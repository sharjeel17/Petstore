using Microsoft.EntityFrameworkCore;
using PetStoreBackend.Context;
using PetStoreBackend.Interfaces;
using PetStoreBackend.Models;

namespace PetStoreBackend.Repository
{
    public class BreedRepository : IBreedRepository
    {
        private readonly ApplicationDbContext _context;
        public BreedRepository(ApplicationDbContext context) 
        {
            _context = context;
        }


        public async Task<bool> CreateBreed(Breed breed)
        {
            _context.Breeds.Add(breed);
            return await SaveAsync();
        }

        public async Task<bool> DeleteBreed(int Id)
        {
            var breed = await _context.Breeds.FirstOrDefaultAsync(b => b.Id == Id);
            _context.Breeds.Remove(breed);
            return await SaveAsync();
        }

        public async Task<Breed> GetBreed(string name)
        {
            return await _context.Breeds.FirstOrDefaultAsync(b => b.BreedName.Trim().ToUpper() == name.Trim().ToUpper());
        }

        public async Task<Breed> GetBreed(int Id)
        {
            return await _context.Breeds.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<ICollection<Breed>> GetBreeds()
        {
            return await _context.Breeds.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateBreed(Breed breed)
        {
            _context.Breeds.Update(breed);
            return await SaveAsync();
        }
    }
}
