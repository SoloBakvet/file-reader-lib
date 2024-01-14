using FileReaderLib.Encryption;
using System.Data;
using System.Text;

namespace FileReaderLib.Core;
/// <summary>
/// Provides base functionality to access a text file. 
/// </summary>
public class TextFile(String filePath) : File(filePath)
{

    public Encoding FileEncoding { get; private set; } = Encoding.UTF8;

    /// <summary>
    /// Loads content of the file into memory.
    /// </summary>
    /// <returns>A string containing the content of the file.</returns>
    /// <exception cref="FileNotFoundException"> </exception>
    /// <exception cref="PathTooLongException"> </exception>
    /// <exception cref="IOException"> </exception>
    public new string LoadContent()
    {
        try
        {
            byte[] fileContent = base.LoadContent();
            return FileEncoding.GetString(fileContent);
        }
        catch
        {
            throw;
        }

    }

    /// <summary>
    /// Prints the content of the file as text to the console.
    /// </summary>
    /// <exception cref="FileNotFoundException"> </exception>
    /// <exception cref="PathTooLongException"> </exception>
    /// <exception cref="IOException"> </exception>
    public override void PrintContent()
    {
        string fileContent = LoadContent();
        Console.WriteLine(fileContent);
    }
}
