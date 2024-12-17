using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Services
{
    public class PasswordHasher : IPasswordHasher
    {
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

            using (var hasher = new Argon2id(passwordBytes))
            {
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
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            // Decode the stored hash and extract the salt
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            byte[] storedHash = new byte[hashBytes.Length - salt.Length];
            Array.Copy(hashBytes, 0, salt, 0, salt.Length);
            Array.Copy(hashBytes, salt.Length, storedHash, 0, storedHash.Length);

            // Convert the provided password to bytes
            byte[] passwordBytes = Encoding.UTF8.GetBytes(providedPassword);

            // Set paramters for Argon2id
            int iterations = 4;
            int memorySize = 1024 * 16;
            int degreeOfParallelism = 4;

            // Hash the provided password with the same salt and paramters
            using (var hasher = new Argon2id(passwordBytes))
            {
                hasher.Salt = salt;
                hasher.Iterations = iterations;
                hasher.MemorySize = memorySize;
                hasher.DegreeOfParallelism = degreeOfParallelism;

                byte[] computedHash = hasher.GetBytes(32);

                // Compare the computed hash with the stored hash
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    };
                }
            }

            return true;
        }
    }
}
