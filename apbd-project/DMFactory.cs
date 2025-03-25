namespace apbd_project;

/// <summary>
/// Device Manager Factory created for implementation of the factory pattern
/// </summary>
public class DMFactory
{
    public static IDeviceManager InitializeDeviceManager(string path)
    {
        return new DeviceManager(path);
    }
}