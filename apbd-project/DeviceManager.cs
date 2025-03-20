using System.Text.RegularExpressions;

namespace apbd_project;

public class DeviceManager
{
    private string _filePath;
    private List<Device> _devices = new List<Device>();

    public DeviceManager(string filePath)
    {
        _filePath = filePath;
        LoadListOfDevices();
    }

    public void AddDevice(Device device)
    {
        if (_devices.Count >= 15)
        {
            Console.WriteLine("Maximum number of devices reached");
            return;
        }

        _devices.Add(device);
    }

    public void RemoveDevice(string typeId)
    {
        List<Device> devicesToRemove = new List<Device>();

        foreach (var device in _devices)
        {
            if (device.Id == typeId)
            {
                devicesToRemove.Add(device);
            }
        }

        foreach (var device in devicesToRemove)
        {
            _devices.Remove(device);
        }
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
                device.IsDeviceOn = state;
            }
        }
        else if (attributeName == "RemainingBatteryCharge")
        {
            if (value is int remainingBatteryCharge && device is Smartwatch sw)
            {
                sw.RemainingBatteryCharge = remainingBatteryCharge;
            }
        }
        else if (attributeName == "OS")
        {
            if (value is string OS && device is PC pc)
            {
                pc.OS = OS;
            }
        }
        else if (attributeName == "IP")
        {
            if (value is string IP && device is EmbeddedDevice ed)
            {
                ed.IP = IP;
            }
        }
        else if (attributeName == "NetworkName")
        {
            if (value is string networkName && device is EmbeddedDevice ed)
            {
                ed.NetworkName = networkName;
            }
        }
    }


    public void TurnOnDevice(string typeId)
    {
        foreach (var device in _devices)
        {
            if (device.Id == typeId)
            {
                if (device is Smartwatch sw)
                {
                    sw.TurnOnSmartwatch();
                }
                else if (device is PC pc)
                {
                    pc.launchPC();
                }
            }
        }
    }

    public void TurnOffDevice(string typeId)
    {
        foreach (var device in _devices)
        {
            if (device.Id == typeId)
            {
                device.TurnOff();
            }
        }
    }

    public void ShowAllDevices()
    {
        foreach (var device in _devices)
        {
            Console.WriteLine($"Device: {device}\n");
        }
    }

    public void SaveListOfDevices(string filePath)
    {
        var lines = new List<string>();
        foreach (var device in _devices)
        {
            if (device is Smartwatch sw)
            {
                lines.Add($"{sw.Id},{sw.Name},{sw.IsDeviceOn},{sw.RemainingBatteryCharge}%");
            }
            else if (device is PC pc)
            {
                lines.Add($"{pc.Id},{pc.Name},{pc.IsDeviceOn},{pc.OS}");
            }
            else if (device is EmbeddedDevice ed)
            {
                lines.Add($"{ed.Id},{ed.Name},{ed.IP},{ed.NetworkName}");
            }
            else
            {
                Console.WriteLine("Non-existing device type");
            }
        }

        File.WriteAllLines(filePath, lines);
    }

    public void LoadListOfDevices()
    {
        if (!File.Exists(_filePath))
        {
            Console.WriteLine("File not found");
            return;
        }

        string[] lines = File.ReadAllLines(_filePath);
        foreach (var line in lines)
        {
            string[] parts = line.Split(',');

            if (parts.Length <= 3)
                continue;


            var deviceTypeId = parts[0];
            var deviceName = parts[1];

            if (deviceTypeId.Contains("SW") && parts.Length == 4)
            {
                var deviceOn = Convert.ToBoolean(parts[2]);
                int remainingBattery = int.Parse(parts[3].TrimEnd('%'));
                var sw = new Smartwatch();
                sw.Id = deviceTypeId;
                sw.Name = deviceName;
                sw.IsDeviceOn = deviceOn;
                sw.RemainingBatteryCharge = remainingBattery;
                _devices.Add(sw);
            }
            else if (deviceTypeId.Contains("P") && parts.Length == 4)
            {
                var deviceOn = Convert.ToBoolean(parts[2]);
                string os = parts[3];
                var pc = new PC();
                pc.Id = deviceTypeId;
                pc.Name = deviceName;
                pc.IsDeviceOn = deviceOn;
                pc.OS = os;
                _devices.Add(pc);
            }
            else if (deviceTypeId.Contains("ED") && parts.Length == 4)
            {
                string ip = parts[2];
                string networkName = parts[3];
                
                if (!Regex.IsMatch(ip,
                        @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$"))
                {
                    continue;
                }
                
                var ed = new EmbeddedDevice();
                ed.Id = deviceTypeId;
                ed.Name = deviceName;
                ed.IP = ip;
                ed.NetworkName = networkName;
                _devices.Add(ed);
            }
        }
    }
    
    public List<Device> GetListOfDevices()
    {
        return _devices;
    }
}