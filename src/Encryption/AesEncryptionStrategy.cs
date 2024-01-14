using System.Security.Cryptography;

namespace FileReaderLib.Encryption;

/// <summary>
/// Provides AES encryption and decryption.
/// !! Can only decrypt data that is encrypted by the same instance. !!
/// </summary>
public class AesEncryptionStrategy : IEncryptionStrategy
{
    public byte[] Key;
    public byte[] IV;

    // TODO: Load key and/or IV from a file.
    /// <summary>
    /// Default contructor.
    /// </summary>
    public AesEncryptionStrategy()
    {
        using Aes aes = Aes.Create();
        aes.GenerateKey();
        aes.GenerateIV();
        Key = aes.Key;
        IV = aes.IV;
    }
    /// <summary>
    /// Encrypts input by using the AES algorithm.
    /// </summary>
    /// <param name="input"> Input to be encrypted.</param>
    /// <returns> Encrypted input.</returns>
    public byte[] Encrypt(byte[] input)
    {
        using Aes aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;

        using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        return PerformCryptographicTransformation(input, encryptor);
    }
    /// <summary>
    /// Decrypts input by using the AES algorithm.
    /// </summary>
    /// <param name="input"> Input to be decrypted.</param>
    /// <returns> Decrypted input.</returns>
    public byte[] Decrypt(byte[] input)
    {
        using Aes aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;

        using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        return PerformCryptographicTransformation(input, decryptor);
    }

    /// <summary>
    /// Applies the provided cryptographic transformation on the input.
    /// </summary>
    /// <param name="input"> Input to be transformed.</param>
    /// <param name="cryptoTransform"> Cryptographic transformation to apply.</param>
    /// <returns>Transformed input.</returns>
    private byte[] PerformCryptographicTransformation(byte[] input, ICryptoTransform cryptoTransform)
    {
        using MemoryStream memoryStream = new();
        using CryptoStream cryptoStream = new(memoryStream, cryptoTransform, CryptoStreamMode.Write);
        cryptoStream.Write(input, 0, input.Length);
        cryptoStream.FlushFinalBlock();

        return memoryStream.ToArray();
    }
} 

