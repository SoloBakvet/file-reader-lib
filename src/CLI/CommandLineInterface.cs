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
                    XmlFile file = new(filePath);
                    file.PrintContent();
                }
                else
                {
                    Console.WriteLine("Empty or invalid file path provided.");
                }
            }
            catch (FileNotFoundException){
                Console.WriteLine("File was not found.");
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
}