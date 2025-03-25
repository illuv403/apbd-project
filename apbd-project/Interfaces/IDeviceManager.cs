namespace apbd_project;

/// <summary>
/// Device Manager interface created to implement factory pattern
/// </summary>
public interface IDeviceManager
{
    /// <summary>
    /// Method to add device to list of devices
    /// </summary>
    /// <param name="newDevice">New device object that we will add</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
    void AddDevice(Device newDevice);
    /// <summary>
    /// Method that edits device data using boxing/unboxing
    /// </summary>
    /// <param name="editDevice">An edited device object</param>
    /// <exception cref="ArgumentException"></exception>
    void EditDeviceData(Device editDevice);
    /// <summary>
    /// Method to remove device from the list of devices
    /// </summary>
    /// <param name="deviceId">Id of the device that needs to be removed</param>
    /// <exception cref="ArgumentException"></exception>
    void RemoveDeviceById(string deviceId);
    /// <summary>
    /// Method used to turn on device 
    /// </summary>
    /// <param name="id">Id of the device that we need to turn on</param>
    /// <exception cref="ArgumentException"></exception>
    void TurnOnDevice(string deviceId);
    /// <summary>
    /// Method used to turn off device 
    /// </summary>
    /// <param name="id">Id of the device that we need to turn off</param>
    /// <exception cref="ArgumentException"></exception>
    void TurnOffDevice(string deviceId);
    /// <summary>
    /// Get instance of some device by id
    /// </summary>
    /// <param name="id">Id of the device we want to find</param>
    /// <returns></returns>
    Device GetDeviceById(string deviceId);
    /// <summary>
    /// Method that just shows all the devices we store 
    /// </summary>
    void ShowAllDevices();
    /// <summary>
    /// Method that implements FileManager's save devices method and saves a list of devices with proper formatting to file in outputPath destination
    /// </summary>
    /// <param name="outputPath">A path to the file to which list of devices needs to be saved</param>
    void SaveDevices(string outputPath);
    /// <summary>
    /// Method that returns list of devices(Added for unit testing)
    /// </summary>
    List<Device> GetListOfDevices();
}