using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Codr.Models {
    [JsonConverter(typeof(HashedPasswordSerializer))]
    public class HashedPassword {
        public string Password { get; private set; }
        public HashedPassword(string unhashedPassword) {
            var bytes = Encoding.Default.GetBytes(unhashedPassword);
            // Generate the salt as a byte array using a cryptographic PRNG (different each time)
            var salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(salt);

            // Generate the hash of the password using SHA 256, a secure hashing algorithm
            byte[] hash;
            using (var sha256 = SHA256.Create())
                hash = sha256.ComputeHash(bytes);

            // Combines the hash and the salt together for storage
            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            // Converts the combined hash and salt to a string
            Password = Convert.ToBase64String(hashBytes);
        }

        public bool Verify(string inputPassword) {
            var inputBytes = Encoding.Default.GetBytes(inputPassword);
            // Extract the salt from the hashed password
            var hashBytes = Convert.FromBase64String(Password);
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            // Hash the input using the given salt
            byte[] inputHash;
            using (var sha256 = SHA256.Create())
                inputHash = sha256.ComputeHash(inputBytes);

            // Check if the hashes match
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != inputHash[i])
                    return false;
            return true;
        }

        public override string ToString() {
            return Password;
        }

        public static explicit operator string(HashedPassword hashedPassword) {
            return hashedPassword.ToString();
        }
    }
}
