using FileReaderLib.Core;
using FileReaderLib.Encryption;
using System.Security;

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
                    Core.File file = CreateFile(filePath,fileExtension);
                    SelectAndSetFileEncryption(file);
                    SelectAndSetRole(file);
                    
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
            catch (SecurityException)
            {
                Console.WriteLine("User has no access to the file.");
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
    /// <summary>
    /// Lets the user select a encryption strategy for a file.
    /// </summary>
    /// <param name="file"> File object for which a encryption strategy will be chosen. </param>
    private static void SelectAndSetFileEncryption(Core.File file)
    {
        while (true)
        {
            Console.WriteLine("Please choose if you want to enable encryption:");
            Console.WriteLine("[0] No encryption");
            Console.WriteLine("[1] Reverse data encryption");
            Console.WriteLine("[2] AES encryption");
            string? encryptionNumber = Console.ReadLine();

            switch (encryptionNumber)
            {
                case "0":
                    file.SetEncryptionStrategy(null);
                    return;

                case "1":
                    file.SetEncryptionStrategy(new ReverseEncryptionStrategy());
                    return;

                case "2":
                    file.SetEncryptionStrategy(new AesEncryptionStrategy());
                    return;

                default:
                    Console.WriteLine("Please select a valid option.");
                    break;

            }

        }
    }
    /// <summary>
    /// Lets the user set the user that will access the file.
    /// </summary>
    /// <param name="file"> File object for which a user will be chosen. </param>
    private static void SelectAndSetRole(Core.File file)
    {
        Console.WriteLine("Please enter a role if you want to enable role based access:");
        string? role = Console.ReadLine();
        if (role is not null && role is not "")
        {
            file.SetUser(role);
        }      
    }
}