using System.Text;

namespace apbd_project;

public class DeviceManager : IDeviceManager
{
    private readonly DeviceParser _deviceParser = new DeviceParser();
    private string _inputDeviceFile;
    private const int MaxCapacity = 15;
    private List<Device> _devices = new(capacity: MaxCapacity);
    private FileManager _fileManager = new FileManager();

    public DeviceManager(string filePath)
    {
       var lines = _fileManager.ReadLines(filePath);
       _devices = _fileManager.ParseDevices(lines);
    }

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

    public void ShowAllDevices()
    {
        foreach (var storedDevices in _devices)
        {
            Console.WriteLine(storedDevices.ToString());
        }
    }

    public void SaveDevices(string outputPath)
    {
       _fileManager.SaveDevices(outputPath, _devices);
    }

    public List<Device> GetListOfDevices()
    {
        return _devices;
    }
}