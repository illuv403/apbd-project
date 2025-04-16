namespace Logic;

using Entities;

/// <summary>
/// DeviceManager class as a part of Logic project is used to implement all of the API DeviceController features
/// </summary>
public class DeviceManager
{
    private readonly List<Device?> _devices;

    public DeviceManager()
    {
        _devices = new List<Device?>();
    }
    
    /// <summary>
    /// Method for GET request
    /// </summary>
    /// <returns>List of devices</returns>
    public IEnumerable<Device?> GetListOfDevices()
    {
        return _devices;
    }
    
    /// <summary>
    /// Method for GET request with specifying of the id in link 
    /// </summary>
    /// <param name="id">id specified in link</param>
    /// <returns>Device with id specified</returns>
    public Device? GetDeviceById(string id)
    {
        return _devices.FirstOrDefault(d => d.Id == id);
    }

    /// <summary>
    /// Adds device to the private list of devices by using POST
    /// </summary>
    /// <param name="device">Device to add</param>
    public void AddDevice(Device? device)
    {
        _devices.Add(device);
    }

    /// <summary>
    /// Method used to edit data of some device by using PUT request 
    /// </summary>
    /// <param name="device">Device which will be ued to change data of old one</param>
    public void EditDeviceData(Device device)
    {
        var foundDevice = _devices.FirstOrDefault(d => d.Id == device.Id);
        
        if (foundDevice == null)
        {
            return;
        }
        
        foundDevice.Name = device.Name;
        foundDevice.IsEnabled = device.IsEnabled;

        if (foundDevice is PersonalComputer foundPC && device is PersonalComputer newPC)
        {
            foundPC.OperatingSystem = newPC.OperatingSystem;
        }
        else if (foundDevice is Smartwatch foundSmartwatch && device is Smartwatch newSmartwatch)
        {
            foundSmartwatch.BatteryLevel = newSmartwatch.BatteryLevel;
        }
        else if (foundDevice is Embedded foundEmbedded && device is Embedded newEmbedded)
        {
            foundEmbedded.NetworkName = newEmbedded.NetworkName;
            foundEmbedded.IpAddress = newEmbedded.IpAddress;
        }
    }
    
    /// <summary>
    /// Method that is being used with DELETE request to delete some device by id
    /// </summary>
    /// <param name="id">ID of the device we want to delete</param>
    public void RemoveDeviceById(string id)
    {
        var device = _devices.FirstOrDefault(d => d.Id == id);
        if (device == null)
        {
            return;
        }
        _devices.Remove(device);
    }
}