namespace Entities.DTO_s;

public class SmartwatchDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsEnabled { get; set; }
    public int BatteryLevel { get; set; }

    public SmartwatchDTO(string id, string name, bool isEnabled, int batteryLevel)
    {
        Id = id;
        Name = name;
        IsEnabled = isEnabled;
        BatteryLevel = batteryLevel;
    }
}