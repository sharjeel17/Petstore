using PetStoreBackend.Models;

namespace PetStoreBackend.Interfaces
{
    public interface IBreedRepository
    {
        public Task<Breed> GetBreed(string name);
        public Task<Breed> GetBreed(int Id);
        public Task<ICollection<Breed>> GetBreeds();
        public Task<bool> CreateBreed(Breed breed);
        public Task<bool> UpdateBreed(Breed breed);
        public Task<bool> DeleteBreed(int Id);
        public Task<bool> SaveAsync();
    }
}
