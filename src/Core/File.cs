using FileReaderLib.Encryption;

namespace FileReaderLib.Core;

/// <summary>
/// Provides base functionality to access a file. 
/// </summary>
public class File(string filePath)
{
    /// <summary>
    /// Path to the location of the file.
    /// </summary>
    public string FilePath { get; private set; } = filePath;
    /// <summary>
    /// Encryption algorithm used to encrypt and decrypt the file.
    /// </summary>
    public IEncryptionStrategy? EncryptionStrategy { get; private set; } = null;

    /// <summary>
    /// Loads content of the file into memory.
    /// </summary>
    /// <returns>An array of bytes containing the content of the file.</returns>
    /// <exception cref="FileNotFoundException"> </exception>
    /// <exception cref="PathTooLongException"> </exception>
    /// <exception cref="IOException"> </exception>
    public virtual byte[] LoadContent()
    {
        try
        {
            using FileStream fs = new (FilePath, FileMode.Open, FileAccess.Read);
            byte[] fileContent = new byte[fs.Length];
            int numBytesToRead = (int)fs.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                // Read may return anything from 0 to numBytesToRead.
                int n = fs.Read(fileContent, numBytesRead, numBytesToRead);

                // Break when the end of the file is reached.
                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }
            // Decrypt the file content if a strategy has been chosen.
            if (EncryptionStrategy is not null)
            {
                return EncryptionStrategy.Decrypt(fileContent);
            }
            else
            {
                return fileContent;
            }
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Prints the content of the file in hexadecimal format to the console.
    /// </summary>
    /// <exception cref="FileNotFoundException"> </exception>
    /// <exception cref="PathTooLongException"> </exception>
    /// <exception cref="IOException"> </exception>
    public virtual void PrintContent()
    {
        byte[] fileContent = LoadContent();
        foreach(byte b in fileContent)
        {
            Console.Write("{0:X2} ", b);
        }
        Console.WriteLine();
    }
}