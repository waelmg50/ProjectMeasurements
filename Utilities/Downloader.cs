using System.Security.Cryptography;
using System.Text;

namespace Utilities
{
    public class Downloader
    {

        #region Methods

        /// <summary>
        /// Decrypt the string with word key.
        /// </summary>
        /// <param name="Text">The string to be decrypted.</param>
        /// <param name="Word">The key of the encryption.</param>
        /// <returns>Returns a tuple of true and the decrypted string if the encryption successded other wise it returns false and the error message.</returns>
        public static (bool, string) Download(string Text, string Word)
        {
            try
            {
                if (string.IsNullOrEmpty(Text))
                    return (true, string.Empty);
                // Convert the encrypted string to a byte array
                byte[] encryptedBytes = Convert.FromBase64String(Text);

                // Derive the password using the PBKDF2 algorithm
                Rfc2898DeriveBytes passwordBytes = new(Word, 20, 3, HashAlgorithmName.SHA512);

                // Use the password to decrypt the encrypted string
                Aes encryptor = Aes.Create();
                encryptor.Key = passwordBytes.GetBytes(32);
                encryptor.IV = passwordBytes.GetBytes(16);
                using MemoryStream ms = new();
                using (CryptoStream cs = new(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                }
                return (true, Encoding.UTF8.GetString(ms.ToArray()));
            }
            catch (FormatException ex)
            {
                if (ex.Message == "Invalid length for a Base-64 char array.")
                    return (false, "String is not encrypted.");
                else
                    return (false, ex.Message);
            }
            catch (CryptographicException ex)
            {
                if (ex.Message == "Length of the data to decrypt is invalid." || ex.Message == "Invalid character in a Base-64 string.")
                    return (false, "String is not encrypted.");
                else
                    return (false, ex.Message);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        /// <summary>
        /// Encrypt the string with word key.
        /// </summary>
        /// <param name="Text">The string to be encrypted.</param>
        /// <param name="Word">The key of the encryption.</param>
        /// <returns>Returns a tuple of true and the encrypted string if the encryption successded other wise it returns false and the error message.</returns>
        public static (bool, string) Upload(string Text, string Word)
        {
            try
            {
                // Convert the plaintext string to a byte array
                byte[] plaintextBytes = Encoding.UTF8.GetBytes(Text);

                // Derive a new password using the PBKDF2 algorithm and a random salt
                Rfc2898DeriveBytes passwordBytes = new(Word, 20, 3, HashAlgorithmName.SHA512);

                // Use the password to encrypt the plaintext
                Aes encryptor = Aes.Create();
                encryptor.Key = passwordBytes.GetBytes(32);
                encryptor.IV = passwordBytes.GetBytes(16);
                using MemoryStream ms = new();
                using (CryptoStream cs = new(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plaintextBytes, 0, plaintextBytes.Length);
                }
                return (true, Convert.ToBase64String(ms.ToArray()));
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        #endregion

    }
}