using System.Xml;

namespace FileReaderLib.Core;

/// <summary>
/// Provides base functionality to access a XML file. 
/// </summary>
public class XmlFile(string filePath) : TextFile(filePath)
    {
    /// <summary>
    /// Loads content of the file into memory.
    /// </summary>
    /// <returns>A XmlDocument object containing the content of the file.</returns>
    /// <exception cref="FileNotFoundException"> </exception>
    /// <exception cref="PathTooLongException"> </exception>
    /// <exception cref="IOException"> </exception>
    /// <exception cref="XmlException"> </exception>
    public new XmlDocument LoadContent()
    {
        try {
            string fileContent = base.LoadContent();
            XmlDocument document = new();
            document.LoadXml(fileContent);
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
    /// <exception cref="XmlException"> </exception>
    public override void PrintContent()
    {
        XmlDocument fileContent = LoadContent();
        using (StringWriter sw = new ())
        {
            using (XmlTextWriter xmlWriter = new (sw))
            {
                xmlWriter.Formatting = Formatting.Indented;
                fileContent.WriteTo(xmlWriter);
                string str = sw.ToString();
                Console.WriteLine(str);
            }
        }
    }
}

