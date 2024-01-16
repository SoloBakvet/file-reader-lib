namespace FileReaderLib.Encryption;

/// <summary>
/// Encryption strategy that is able to encrypt and decrypt data.
/// </summary>
public interface IEncryptionStrategy
{
    /// <summary>
    /// Encrypts the input using the strategy provided algorithm.
    /// </summary>
    /// <param name="input"> Input to be encrypted.</param>
    /// <returns> Encrypted input.</returns>
    public byte[] Encrypt(byte[] input);
    /// <summary>
    /// Decrypts the input using the strategy provided algorithm.
    /// </summary>
    /// <param name="input">Input to be decrypted.</param>
    /// <returns> Decrypted input.</returns>
    public byte[] Decrypt(byte[] input);
}
