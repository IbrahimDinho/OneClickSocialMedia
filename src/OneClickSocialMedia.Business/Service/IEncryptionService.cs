namespace OneClickSocialMedia.Business.Service
{
    public interface IEncryptionService
    {
        /// <summary>
        /// Encrypts a value using the specified provider name.
        /// </summary>
        /// <param name="providerName">
        /// The name of the provider used to create the data protection scope (e.g. "Twitter").
        /// </param>
        /// <param name="value">
        /// The plain text value to encrypt.
        /// </param>
        /// <returns>
        /// The encrypted representation of the input value.
        /// </returns>
        string Encrypt(string providerName, string value);

        /// <summary>
        /// Decrypts a value using the specified provider name.
        /// </summary>
        /// <param name="providerName">
        /// The name of the provider used to create the data protection scope (e.g. "Twitter").
        /// Must match the provider name used during encryption.
        /// </param>
        /// <param name="value">
        /// The encrypted value to decrypt.
        /// </param>
        /// <returns>
        /// The decrypted (plain text) value.
        /// </returns>
        string Decrypt(string providerName, string value);
    }
}