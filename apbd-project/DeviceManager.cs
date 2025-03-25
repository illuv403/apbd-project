using System.Text;

namespace apbd_project;

public class DeviceManager
{
    private readonly DeviceParser _deviceParser = new DeviceParser();
    private string _inputDeviceFile;
    private const int MaxCapacity = 15;
    private List<Device> _devices = new(capacity: MaxCapacity);

    public DeviceManager(string filePath)
    {
        _inputDeviceFile = filePath;

        if (!File.Exists(_inputDeviceFile))
        {
            throw new FileNotFoundException("The input device file could not be found.");
        }

        var lines = File.ReadAllLines(_inputDeviceFile);
        ParseDevices(lines);
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

    public void EditDeviceData(string typeId, string attributeName, object value)
    {
        Device? device = null;

        foreach (var dev in _devices)
        {
            if (dev.Id == typeId)
            {
                device = dev;
            }
        }

        if (device == null)
        {
            Console.WriteLine("Device not found");
            return;
        }
        
        if (attributeName == "Name")
        {
            if (value is string name)
            {
                device.Name = name;
            }
        }
        else if (attributeName == "IsTurnedOn")
        {
            if (value is bool state)
            {
                device.IsEnabled = state;
            }
        }
        else if (attributeName == "RemainingBatteryCharge")
        {
            if (value is int remainingBatteryCharge && device is Smartwatch sw)
            {
                sw.BatteryLevel = remainingBatteryCharge;
            }
        }
        else if (attributeName == "OS")
        {
            if (value is string OS && device is PersonalComputer pc)
            {
                pc.OperatingSystem = OS;
            }
        }
        else if (attributeName == "IP")
        {
            if (value is string IP && device is Embedded ed)
            {
                ed.IpAddress = IP;
            }
        }
        else if (attributeName == "NetworkName")
        {
            if (value is string networkName && device is Embedded ed)
            {
                ed.NetworkName = networkName;
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
        StringBuilder devicesSb = new();

        foreach (var storedDevice in _devices)
        {
            if (storedDevice is Smartwatch smartwatchCopy)
            {
                devicesSb.AppendLine($"{smartwatchCopy.Id},{smartwatchCopy.Name}," +
                                     $"{smartwatchCopy.IsEnabled},{smartwatchCopy.BatteryLevel}%");
            }
            else if (storedDevice is PersonalComputer pcCopy)
            {
                devicesSb.AppendLine($"{pcCopy.Id},{pcCopy.Name}," +
                                     $"{pcCopy.IsEnabled},{pcCopy.OperatingSystem}");
            }
            else
            {
                var embeddedCopy = storedDevice as Embedded;
                devicesSb.AppendLine($"{embeddedCopy.Id},{embeddedCopy.Name}," +
                                     $"{embeddedCopy.IsEnabled},{embeddedCopy.IpAddress}," +
                                     $"{embeddedCopy.NetworkName}");
            }
        }
        
        File.WriteAllLines(outputPath, devicesSb.ToString().Split('\n'));
    }

    private void ParseDevices(string[] lines)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            try
            {
                Device parsedDevice;

                if (lines[i].StartsWith("P-"))
                {
                    parsedDevice = _deviceParser.ParsePC(lines[i], i);
                }
                else if (lines[i].StartsWith("SW-"))
                {
                    parsedDevice = _deviceParser.ParseSmartwatch(lines[i], i);
                }
                else if (lines[i].StartsWith("ED-"))
                {
                    parsedDevice = _deviceParser.ParseEmbedded(lines[i], i);
                }
                else
                {
                    throw new ArgumentException($"Line {i} is corrupted.");
                }

                AddDevice(parsedDevice);
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Something went wrong during parsing this line: {lines[i]}. The exception message: {ex.Message}");
            }
        }
    }

    public List<Device> GetListOfDevices()
    {
        return _devices;
    }
}
