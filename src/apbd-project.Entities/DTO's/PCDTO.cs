namespace Entities.DTO_s;

public class PCDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsEnabled { get; set; }
    public string? OperatingSystem { get; set; }

    public PCDTO(string id, string name, bool isEnabled, string? operatingSystem)
    {
        Id = id;
        Name = name;
        IsEnabled = isEnabled;
        OperatingSystem = operatingSystem;
    }
}