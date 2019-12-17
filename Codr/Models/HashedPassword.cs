using System;
using System.Security.Cryptography;

namespace Codr.Models {
    public class HashedPassword {
        public string Password { get; private set; }
        public HashedPassword(byte[] unhashedPassword) {
            // Generate the salt as a byte array using a cryptographic PRNG (different each time)
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(salt);

            // Generate the hash of the password using SHA 256, a secure hashing algorithm
            byte[] hash;
            using (var sha256 = SHA256.Create())
                hash = sha256.ComputeHash(unhashedPassword);

            // Combines the hash and the salt together for storage
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            // Converts the combined hash and salt to a string
            Password = Convert.ToBase64String(hashBytes);
        }

        public override string ToString() {
            return Password;
        }
    }
}
