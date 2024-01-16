namespace FileReaderLib.AccessControl;

/// <summary>
/// Handles access control to files.
/// </summary>
public sealed class AccessManager
{
    /// <summary>
    /// Singleton instance of the class.
    /// </summary>
    private static AccessManager? _Instance = null;

    /// <summary>
    /// Retrieves the singleton instance of the class. !! Not thread-safe !!
    /// </summary>
    public static AccessManager Instance { 
        get
        {
            if (_Instance is null)
            {
                _Instance = new();
            }
            return _Instance;
        } 
    }

    /// <summary>
    /// Checks if a user has access to a file.
    /// </summary>
    /// <param name="filePath"> Path to the file. </param>
    /// <param name="user"> User that wants to access the file. </param>
    /// <returns> Bool containing if the user has the right to access the file. </returns>
    public bool HasAccesRights(string filePath, string user)
    {
        return true;
    }
}

