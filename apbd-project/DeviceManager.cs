using System.Text;

namespace apbd_project;

/// <summary>
/// Device manager class that implements IDeviceManager interface
/// </summary>
public class DeviceManager : IDeviceManager
{
    private readonly DeviceParser _deviceParser = new DeviceParser();
    private string _inputDeviceFile;
    private const int MaxCapacity = 15;
    private List<Device> _devices = new(capacity: MaxCapacity);
    private FileManager _fileManager = new FileManager();

    /// <summary>
    /// DeviceManager class constructor which reads all the lines from file and parses them
    /// </summary>
    /// <param name="filePath">Path to the file from which we will read all the lines</param>
    public DeviceManager(string filePath)
    {
       var lines = _fileManager.ReadLines(filePath);
       _devices = _fileManager.ParseDevices(lines);
    }

    /// <summary>
    /// Method to add device to list of devices
    /// </summary>
    /// <param name="newDevice">New device object that we will add</param>
    /// <exception cref="ArgumentException">Is being thrown is device with some Id is already being stored</exception>
    /// <exception cref="Exception">Is being thrown if storage is full e.g. there is more than 15 devices</exception>
    public void AddDevice(Device newDevice)
    {
        foreach (var storedDevice in _devices)
        {
            if (storedDevice.Id.Equals(newDevice.Id))
            {
                throw new ArgumentException($"Device with ID {storedDevice.Id} is already stored.", nameof(newDevice));
            }
        }

        if (_devices.Count >= MaxCapacity)
        {
            throw new Exception("Device storage is full.");
        }

        _devices.Add(newDevice);
    }

    /// <summary>
    /// Method that edits device data using boxing/unboxing
    /// </summary>
    /// <param name="editDevice">An edited device object</param>
    /// <exception cref="ArgumentException">Is being thrown if there is any exception with device passed as argument</exception>
    public void EditDeviceData(Device editDevice)
    {
        var targetDeviceIndex = -1;
        for (var index = 0; index < _devices.Count; index++)
        {
            var storedDevice = _devices[index];
            if (storedDevice.Id.Equals(editDevice.Id))
            {
                targetDeviceIndex = index;
                break;
            }
        }

        if (targetDeviceIndex == -1)
        {
            throw new ArgumentException($"Device with ID {editDevice.Id} is not stored.", nameof(editDevice));
        }

        if (editDevice is Smartwatch newSmartwatch)
        {
            if (_devices[targetDeviceIndex] is Smartwatch existingSmartwatch)
            {
                existingSmartwatch.IsEnabled = newSmartwatch.IsEnabled;
                existingSmartwatch.BatteryLevel = newSmartwatch.BatteryLevel;
            }
            else
            {
                throw new ArgumentException($"Type mismatch between devices. " +
                                            $"Target device has type {_devices[targetDeviceIndex].GetType().Name}");
            }
        }
        else if (editDevice is PersonalComputer newPC)
        {
            if (_devices[targetDeviceIndex] is PersonalComputer existingPC)
            {
                existingPC.IsEnabled = newPC.IsEnabled;
                existingPC.OperatingSystem = newPC.OperatingSystem;
            }
            else
            {
                throw new ArgumentException($"Type mismatch between devices. " +
                                            $"Target device has type {_devices[targetDeviceIndex].GetType().Name}");
            }
        }
        else if (editDevice is Embedded newEmbedded)
        {
            if (_devices[targetDeviceIndex] is Embedded existingEmbedded)
            {
                existingEmbedded.IsEnabled = newEmbedded.IsEnabled;
                existingEmbedded.IpAddress = newEmbedded.IpAddress;
                existingEmbedded.NetworkName = newEmbedded.NetworkName;
            }
            else
            {
                throw new ArgumentException($"Type mismatch between devices. " +
                                            $"Target device has type {_devices[targetDeviceIndex].GetType().Name}");
            }
        }
    }

    /// <summary>
    /// Method to remove device from the list of devices
    /// </summary>
    /// <param name="deviceId">Id of the device that needs to be removed</param>
    /// <exception cref="ArgumentException">Is being thrown if device with id specified doesn't exist</exception>
    public void RemoveDeviceById(string deviceId)
    {
        Device? targetDevice = null;
        foreach (var storedDevice in _devices)
        {
            if (storedDevice.Id.Equals(deviceId))
            {
                targetDevice = storedDevice;
                break;
            }
        }

        if (targetDevice == null)
        {
            throw new ArgumentException($"Device with ID {deviceId} is not stored.", nameof(deviceId));
        }

        _devices.Remove(targetDevice);
    }
    
    /// <summary>
    /// Method used to turn on device 
    /// </summary>
    /// <param name="id">Id of the device that we need to turn on</param>
    /// <exception cref="ArgumentException">Is being thrown if device with id specified doesn't exist</exception>
    public void TurnOnDevice(string id)
    {
        foreach (var storedDevice in _devices)
        {
            if (storedDevice.Id.Equals(id))
            {
                storedDevice.TurnOn();
                return;
            }
        }

        throw new ArgumentException($"Device with ID {id} is not stored.", nameof(id));
    }

    /// <summary>
    /// Method used to turn off device 
    /// </summary>
    /// <param name="id">Id of the device that we need to turn off</param>
    /// <exception cref="ArgumentException">Is being thrown if device with id specified doesn't exist</exception>
    public void TurnOffDevice(string id)
    {
        foreach (var storedDevice in _devices)
        {
            if (storedDevice.Id.Equals(id))
            {
                storedDevice.TurnOff();
                return;
            }
        }

        throw new ArgumentException($"Device with ID {id} is not stored.", nameof(id));
    }

    /// <summary>
    /// Get instance of some device by id
    /// </summary>
    /// <param name="id">Id of the device we want to find</param>
    /// <returns>Device with id specified</returns>
    public Device? GetDeviceById(string id)
    {
        foreach (var storedDevice in _devices)
        {
            if (storedDevice.Id.Equals(id))
            {
                return storedDevice;
            }
        }

        return null;
    }

    /// <summary>
    /// Method that just shows all the devices we store 
    /// </summary>
    public void ShowAllDevices()
    {
        foreach (var storedDevices in _devices)
        {
            Console.WriteLine(storedDevices.ToString());
        }
    }

    /// <summary>
    /// Method that implements FileManager's save devices method and saves a list of devices with proper formatting to file in outputPath destination
    /// </summary>
    /// <param name="outputPath">A path to the file to which list of devices needs to be saved</param>
    public void SaveDevices(string outputPath)
    {
       _fileManager.SaveDevices(outputPath, _devices);
    }

    /// <summary>
    /// Method that returns list of devices(Added for unit testing)
    /// </summary>
    public List<Device> GetListOfDevices()
    {
        return _devices;
    }
}