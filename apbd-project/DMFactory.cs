namespace apbd_project;

/// <summary>
/// Device Manager Factory created for implementation of the factory pattern
/// </summary>
public class DMFactory
{
    /// <summary>
    /// The method to create new instance of device manager
    /// </summary>
    /// <param name="path">A path to the input file with devices</param>
    /// <returns>New instance of the DeviceManager</returns>
    public static IDeviceManager InitializeDeviceManager(string path)
    {
        return new DeviceManager(path);
    }
}