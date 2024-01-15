using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PetStoreAuth.Dtos;
using PetStoreAuth.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetStoreAuth.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<IdentityUser> usermanager, IConfiguration config)
        {
            _usermanager = usermanager;
            _config = config;
        }

        //create user and add to database
        public async Task<bool> RegisterUserAsync(UserDto user)
        {
            var identityUser = new IdentityUser()
            {
                UserName = user.Username
            };

            //identity handles hashing and storing of passwords
            var result = await _usermanager.CreateAsync(identityUser, user.Password);

            //error logging
            if (!result.Succeeded) 
            {
                foreach (var error in result.Errors) 
                {
                    Console.WriteLine(error.Description);
                }
            }

            return result.Succeeded;
        }

        //Check credentials of user
        public async Task<bool> LoginUserAsync(UserDto user)
        {
            var identityUser = await _usermanager.FindByNameAsync(user.Username);

            if (identityUser is null)
            {
                return false;
            }

            return await _usermanager.CheckPasswordAsync(identityUser, user.Password);
        }

        //Create JWT
        public string GenerateJsonWebToken(string username)
        {
            //Make claims for the security token
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim("Username",username),
                new Claim("Role", "Admin")
            };

            //make security key for the signing credentials
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:privateKey").Value!));

            //make signing credentials for security token
            SigningCredentials signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //create jwt security token
            var securityToken = new JwtSecurityToken(
                claims:claims,
                audience: _config.GetSection("Jwt:audience").Value,
                issuer: _config.GetSection("Jwt:issuer").Value,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials:signingCredential

                );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
