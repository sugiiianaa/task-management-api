using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models;
using TaskManagement.Application.Models.AppSettings;
using TaskManagement.Application.Models.Enums;
using TaskManagement.Application.Models.InputModel.Auth;
using TaskManagement.Domain.Dtos;
using TaskManagement.Domain.Enums;
using TaskManagement.Infrastructure.Interfaces;

namespace TaskManagement.Application.Services
{
    public class AuthService(
        JwtSettings jwtSettings,
        IUserRepository userRepository,
        IConfiguration configuration
        ) : IAuthService
    {

        private readonly JwtSettings _jwtSettings = jwtSettings;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;

        // Authentication Servies

        public async Task<ApplicationOutputGenericModel<string?>> LoginUserAsync(LoginUserInput input)
        {
            var user = await _userRepository.GetUserByEmailAsync(input.Email);

            if (user == null)
            {
                return new ApplicationOutputGenericModel<string?>
                {
                    ErrorMessage = ApplicationErrorMessage.NotFound
                };
            }

            if (!VerifyPassword(input.Password, user.PasswordHash))
            {
                return new ApplicationOutputGenericModel<string?>
                {
                    ErrorMessage = ApplicationErrorMessage.BadRequest
                };
            }

            var token = GenerateJwtToken(user.Id, user.Role);

            return new ApplicationOutputGenericModel<string?>
            {
                Data = token
            };
        }
        public async Task<ApplicationOutputGenericModel<Guid?>> RegisterUserAsync(RegisterUserInput input)
        {
            if (input.Password != input.ReTypePassword)
            {
                return new ApplicationOutputGenericModel<Guid?>
                {
                    ErrorMessage = ApplicationErrorMessage.BadRequest
                };
            }

            var hashedPassword = HashPassword(input.Password);

            var userEntity = new UserDto
            {
                Email = input.Email,
                Name = input.Name,
                Password = hashedPassword,
                Role = UserRoles.User,
            };

            var user = await _userRepository.GetUserByEmailAsync(input.Email);

            if (user != null)
            {
                return new ApplicationOutputGenericModel<Guid?>
                {
                    ErrorMessage = ApplicationErrorMessage.BadRequest
                };
            }

            var userId = await _userRepository.AddUserAsync(userEntity);

            return new ApplicationOutputGenericModel<Guid?>
            {
                Data = userId
            };
        }

        // Token Services

        public string GenerateJwtToken(Guid ownerId, UserRoles role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var RoleString = RoleHelper.GetRole(role);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, ownerId.ToString()),
                new Claim(ClaimTypes.Role, RoleString),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.TokenExpiryInHours),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Password Services

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        public string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            byte[] salt = GenerateSalt();

            int iteration = 4;
            int memorySize = 1024 * 16;
            int degreeOfParallelism = 4;

            using var hasher = new Argon2id(passwordBytes);
            hasher.Salt = salt;
            hasher.Iterations = iteration;
            hasher.MemorySize = memorySize;
            hasher.DegreeOfParallelism = degreeOfParallelism;

            // Hashing
            byte[] hash = hasher.GetBytes(32);

            // Combine salt and hash into a single string for storage
            byte[] result = new byte[salt.Length + hash.Length];
            Array.Copy(salt, 0, result, 0, salt.Length);
            Array.Copy(hash, 0, result, salt.Length, hash.Length);

            return Convert.ToBase64String(result);
        }
        public bool VerifyPassword(string providedPassword, string storedHashedPassword)
        {
            // Decode the stored hash and extract the salt and stored hash
            byte[] hashBytes = Convert.FromBase64String(storedHashedPassword);

            if (hashBytes.Length <= 16) // Ensure the length is at least the salt length
            {
                throw new FormatException("Invalid stored hash length.");
            }

            byte[] salt = new byte[16];  // Assuming the salt is 16 bytes
            byte[] storedHash = new byte[hashBytes.Length - salt.Length];

            Array.Copy(hashBytes, 0, salt, 0, salt.Length);
            Array.Copy(hashBytes, salt.Length, storedHash, 0, storedHash.Length);

            // Convert the provided password to bytes
            byte[] passwordBytes = Encoding.UTF8.GetBytes(providedPassword);

            // Set parameters for Argon2id
            int iterations = 4;
            int memorySize = 1024 * 16;
            int degreeOfParallelism = 4;

            // Hash the provided password with the same salt and parameters
            using (var hasher = new Argon2id(passwordBytes))
            {
                hasher.Salt = salt;
                hasher.Iterations = iterations;
                hasher.MemorySize = memorySize;
                hasher.DegreeOfParallelism = degreeOfParallelism;

                byte[] computedHash = hasher.GetBytes(32);

                // Compare the computed hash with the stored hash
                return computedHash.SequenceEqual(storedHash);
            }
        }


    }
}
