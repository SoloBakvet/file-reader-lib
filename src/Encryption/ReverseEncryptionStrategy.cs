namespace FileReaderLib.Encryption;

/// <summary>
/// Provides reverse input encryption and decryption.
/// </summary>
public class ReverseEncryptionStrategy : IEncryptionStrategy
{
    /// <summary>
    /// Decrypts input by reversing it.
    /// </summary>
    /// <param name="input"> Input to be decrypted.</param>
    /// <returns> Decrypted input.</returns>
    public byte[] Decrypt(byte[] input)
    {
        byte[] output = (byte[]) input.Clone();
        Array.Reverse(output);
        return output;
    }

    /// <summary>
    /// Encrypts input by reversing it.
    /// </summary>
    /// <param name="input"> Input to be encrypted.</param>
    /// <returns> Encrypted input.</returns>
    public byte[] Encrypt(byte[] input)
    {
        byte[] output = (byte[])input.Clone();
        Array.Reverse(output);
        return output;
    }
}

