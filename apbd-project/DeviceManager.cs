namespace apbd_project;

public class DeviceManager
{
    private string _filePath;
    private List<Device> _devices;
    
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
        foreach (var device in _devices)
        {
            if (device.Id == typeId)
            {
                _devices.Remove(device);
            }
        }
    }

    public void EditDeviceData(string typeId, string name, bool isDeviceOn)
    {
        
    }
    
    public void LoadListOfDevices()
    {
        try
        {
            File.Exists(_filePath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
        string[] lines = File.ReadAllLines(_filePath);
        foreach (var line in lines)
        {
            string[] parts = line.Split(',');

            try
            {
                var deviceTypeId = parts[0];
                var deviceName = parts[1];
                bool deviceOn = Convert.ToBoolean(parts[2]);

                if (deviceTypeId.Contains("SW") && parts.Length == 4)
                {
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
                    string os = parts[3];
                    var pc = new PC();
                    pc.Id = deviceTypeId;
                    pc.Name = deviceName;
                    pc.IsDeviceOn = deviceOn;
                    pc.OS = os;
                    _devices.Add(pc);
                }
                else if (deviceTypeId.Contains("ED") && parts.Length == 5)
                {
                    string ip = parts[3];
                    string networkName = parts[4];
                    var ed = new EmbeddedDevice();
                    ed.Id = deviceTypeId;
                    ed.Name = deviceName;
                    ed.IP = ip;
                    ed.NetworkName = networkName;
                    _devices.Add(ed);
                }
                else
                {
                    Console.WriteLine("Line corrupted");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        
        
    }
    
}