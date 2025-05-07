namespace Entities;

/// <summary>
/// Personal computer class which inherits from device class
/// </summary>
public class PersonalComputer : Device
{
    public string? OperatingSystem { get; set; }
    
    /// <summary>
    /// PersonalComputer class constructor which checks if id is right and sets Operating System
    /// </summary>
    /// <param name="id">Id of the device in format "P-"</param>
    /// <param name="name">Name of the device</param>
    /// <param name="isEnabled">Is device turned on</param>
    /// <param name="operatingSystem">Operating system of the pc</param>
    /// <exception cref="ArgumentException">Is being thrown if Id is incorrectly formatted</exception>
    public PersonalComputer(string id, string name, bool isEnabled, string? operatingSystem) : base(id, name, isEnabled)
    {
        if (!CheckId(id)) 
        {
            throw new ArgumentException("Invalid ID value. Required format: P-1", id);
        }
        
        OperatingSystem = operatingSystem;
    }

    /// <summary>
    /// Overrided TurnOn method with additional logic
    /// </summary>
    public override void TurnOn()
    {
        if (OperatingSystem is null)
        {
            throw new EmptySystemException();
        }

        base.TurnOn();
    }

    public override string ToString()
    {
        string enabledStatus = IsEnabled ? "enabled" : "disabled";
        string osStatus = OperatingSystem is null ? "has not OS" : $"has {OperatingSystem}";
        return $"PC {Name} ({Id}) is {enabledStatus} and {osStatus}";
    }

    private bool CheckId(string id) => id.Contains("P-");
}
