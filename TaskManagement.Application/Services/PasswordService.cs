using System.Security.Cryptography;
using Konscious.Security.Cryptography;
using System.Text;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Services
{
    public class PasswordService : IPasswordService
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

        public bool VerifyPassword(string providedPassword, string storedHashedPassword)
        {
            try
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
            catch (FormatException ex)
            {
                // Log the exception or handle it as necessary
                throw new InvalidOperationException("Error while verifying password.", ex);
            }
        }
    } 
}
