using FileReaderLib.AccessControl;
using FileReaderLib.Encryption;
using System.Security;

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
    /// User who will access the file. 
    /// </summary>
    public string User { get; private set; } = "";
    /// <summary>
    /// Cryptographic algorithm used to encrypt and decrypt the file.
    /// </summary>
    public IEncryptionStrategy? EncryptionStrategy { get; private set; } = null;

    /// <summary>
    /// Loads content of the file into memory.
    /// </summary>
    /// <returns>An array of bytes containing the content of the file.</returns>
    /// <exception cref="FileNotFoundException"> </exception>
    /// <exception cref="PathTooLongException"> </exception>
    /// <exception cref="IOException"> </exception>
    /// <exception cref="SecurityException"> </exception>
    public virtual byte[] LoadContent()
    {
        try
        {
            // Checks if the user has access to the file.
            AccessManager accessManager = AccessManager.Instance;
            if(User is not "" && accessManager.HasAccesRights(FilePath, User) is false){
                throw new SecurityException("Specified user has no access to the file.");
            }

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
                return fileContent;
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
    /// <exception cref="SecurityException"> </exception>
    public virtual void PrintContent()
    {
        byte[] fileContent = LoadContent();
        foreach(byte b in fileContent)
        {
            Console.Write("{0:X2} ", b);
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Sets cryptographic algorithm to use when operating on the file.
    /// </summary>
    /// <param name="strategy"> Cryptographic algorithm to use with the file. </param>
    public void SetEncryptionStrategy(IEncryptionStrategy? strategy)
    {
        EncryptionStrategy = strategy;
    }

    /// <summary>
    /// Sets user to identify who will access the file.
    /// </summary>
    /// <param name="user"> The user that will access the file. </param>
    public void SetUser(string user)
    {
        User = user;
    }
}