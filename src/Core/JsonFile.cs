using System.Text.Json;

namespace FileReaderLib.Core;

/// <summary>
/// Provides base functionality to access a JSON file. 
/// </summary>
public class JsonFile(string filePath) : TextFile(filePath)
{
    /// <summary>
    /// Loads content of the file into memory.
    /// </summary>
    /// <exception cref="FileNotFoundException"> </exception>
    /// <exception cref="PathTooLongException"> </exception>
    /// <exception cref="IOException"> </exception>
    /// <exception cref="JsonException"> </exception>
    public new JsonDocument LoadContent()
    {
        try
        {
            string fileContent = base.LoadContent();
            JsonDocument document = JsonDocument.Parse(fileContent);
            return document;
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
    /// <exception cref="JsonException"> </exception>
    public override void PrintContent()
    {
        JsonDocument fileContent = LoadContent();
        Console.WriteLine(fileContent.RootElement.GetRawText());
    }
}
