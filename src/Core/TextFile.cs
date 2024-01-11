using System.Text;

namespace FileReaderLib.Core;
/// <summary>
/// Provides base functionality to access a text file. 
/// </summary>
public class TextFile(String filePath) : File(filePath)
{
    /// <summary>
    /// Loads content of the file into memory.
    /// </summary>
    /// <returns>A string containing the content of the file.</returns>
    /// <exception cref="FileNotFoundException"> </exception>
    /// <exception cref="PathTooLongException"> </exception>
    /// <exception cref="IOException"> </exception>
    public new string LoadContent()
    {
        byte[] fileContent = base.LoadContent();
        return Encoding.UTF8.GetString(fileContent);
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
