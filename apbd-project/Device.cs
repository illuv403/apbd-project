namespace apbd_project;

/// <summary>
/// Abstract class device from which all devices inherit
/// </summary>
public abstract class Device
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Device class constructor
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="isEnabled"></param>
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