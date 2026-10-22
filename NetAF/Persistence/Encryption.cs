using System;
using System.Security.Cryptography;
using System.Text;

namespace NetAF.Persistence
{
    /// <summary>
    /// Provides basic encryption for save persistence.
    /// </summary>
    internal static class Encryption
    {
        #region StaticFields

        // these values don't need to be secure, they only obfuscate the data to prevent casual users from reading it

        // must be exactly 32 characters for AES-256
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("NetAFInsecureKey01234567890!!???");

        // must be exactly 16 characters
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("NetAFInsecureIV!");

        #endregion

        #region StaticMethods

        /// <summary>
        /// Encrypt plain text.
        /// </summary>
        /// <param name="plainText">The plain text to encrypt.</param>
        /// <returns>The encrypted text.</returns>
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            using Aes aes = Aes.Create();
            using ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);

            byte[] input = Encoding.UTF8.GetBytes(plainText);
            byte[] output = encryptor.TransformFinalBlock(input, 0, input.Length);

            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// Decrypt encrypted text.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            using Aes aes = Aes.Create();
            using ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);

            byte[] input = Convert.FromBase64String(cipherText);
            byte[] output = decryptor.TransformFinalBlock(input, 0, input.Length);

            return Encoding.UTF8.GetString(output);
        }

        #endregion
    }
}
