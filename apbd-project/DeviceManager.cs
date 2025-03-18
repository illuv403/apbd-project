namespace apbd_project;

public class DeviceManager
{
    private string _filePath;
    private Device[] _devices;
    
    public DeviceManager(string filePath)
    {
        _filePath = filePath;
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
                var deviceType = parts[0];
                var deviceName = parts[1];
                var deviceOn = parts[2];

                if (deviceType.Contains("SW") && parts.Length == 4)
                {
                    int remainingBattery = int.Parse(parts[3].TrimEnd('%'));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        
        
    }
    
}