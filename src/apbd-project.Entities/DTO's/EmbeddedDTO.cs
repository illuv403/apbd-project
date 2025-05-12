namespace Entities.DTO_s;

public class EmbeddedDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsEnabled { get; set; }
    public string NetworkName { get; set; }
    public string IpAddress { get; set; }

    public EmbeddedDTO(string id, string name, bool isEnabled, string networkName, string ipAddress)
    {
        Id = id;
        Name = name;
        IsEnabled = isEnabled;
        NetworkName = networkName;
        IpAddress = ipAddress;
    }
}