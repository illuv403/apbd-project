namespace apbd_project;

public class DMFactory
{
    public static IDeviceManager InitializeDeviceManager(string path)
    {
        return new DeviceManager(path);
    }
}