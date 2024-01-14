using FileReaderLib.Core;

namespace FileReaderLib.CLI;

/// <summary>
/// Simple CLI to read the content of a file using the library.
/// </summary>
public class CommandLineInterface
{
    /// <summary>
    /// Starts the CLI.
    /// </summary>
    public static void Main() {
        Console.WriteLine("Welcome to the file-reader-lib CLI!");
        Console.WriteLine("Press CTRL+C to exit the application.");
        Console.WriteLine(new String('-', 35));
        while (true)
        {
            try
            {
                Console.WriteLine("Please enter the path of a file you want to read:");
                string? filePath = Console.ReadLine();
                if (filePath is not null && filePath is not "")
                {
                    string fileExtension = Path.GetExtension(filePath);
                    Console.WriteLine(fileExtension);
                    Core.File file = CreateFile(filePath,fileExtension);
                    file.PrintContent();
                }
                else
                {
                    Console.WriteLine("Empty or invalid file path provided.");
                    continue;
                }

            }
            catch (FileNotFoundException){
                Console.WriteLine("File was not found.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("File type is not supported");
            }
            catch
            {
                Console.WriteLine("Something went wrong. Please try again.");
            }
            finally {
                Console.WriteLine(new String('-', 35));
            }
        }
    }

    /// <summary>
    /// Creates a file object depending on the file extension.
    /// </summary>
    /// <param name="filePath"> Path to the location of the file. </param>
    /// <param name="fileExtension"> Extension type of the file. </param>
    /// <returns> A file object.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static Core.File CreateFile(string filePath, string fileExtension)
    {
        switch (fileExtension)
        {
            case ".txt":
                return new TextFile(filePath);

            case ".xml":
                return new XmlFile(filePath);

            default:
                throw new ArgumentOutOfRangeException(nameof(fileExtension), "File type is not supported.");

        }
    }

}