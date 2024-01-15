using PetStoreAuth.Dtos;

namespace PetStoreAuth.Interfaces
{
    public interface IAuthService
    {
        string GenerateJsonWebToken(string username);
        Task<bool> LoginUserAsync(UserDto user);
        Task<bool> RegisterUserAsync(UserDto user);
    }
}