namespace Entities;

/// <summary>
/// Parent abstract class device from which all devices inherit
/// </summary>
public abstract class Device
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsEnabled { get; set; }
    public string? RV { get; set; }
    
    /// <summary>
    /// Device class constructor
    /// </summary>
    /// <param name="id">Id of the device</param>
    /// <param name="name">Name of the device</param>
    /// <param name="isEnabled">State of the device e.g. is it turned on or not</param>
    public Device(string id, string name, bool isEnabled)
    {
        Id = id;
        Name = name;
        IsEnabled = isEnabled;
    }

    /// <summary>
    /// Function used to turn on the device
    /// </summary>
    public virtual void TurnOn()
    {
        IsEnabled = true;
    }
    
    /// <summary>
    /// Function used to turn off the device
    /// </summary>
    public virtual void TurnOff()
    {
        IsEnabled = false;
    }
}
