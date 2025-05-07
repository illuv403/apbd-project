namespace Entities;

/// <summary>
/// Smartwatch device class which inherits from device class
/// </summary>
public class Smartwatch : Device, IPowerNotify
{
    private int _batteryLevel;

    /// <summary>
    /// Battery Level property which checks if new battery level is between 0 and 100 and notifies about low battery level if it is smaller than 20
    /// </summary>
    /// <exception cref="ArgumentException">Is being thrown if battery level is not between 0 and 100</exception>
    public int BatteryLevel
    {
        get => _batteryLevel;
        set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException("Invalid battery level value. Must be between 0 and 100.", nameof(value));
            }
            
            _batteryLevel = value;
            if (_batteryLevel < 20)
            {
                Notify();
            }
        }
    }
    
    ///<summary>
    /// Smartwatch class constructor which checks ID format and sets new Battery Level 
    /// </summary>
    /// <param name="id">Id of the device in format "SW-"</param>
    /// <param name="name">Name of the device</param>
    /// <param name="isEnabled">Is device turned on</param>
    /// <param name="batteryLevel">Battery level of the device (0-100)</param>
    /// <exception cref="ArgumentException">Is being thrown if device id is of the wrong format not "SW-""</exception>
    public Smartwatch(string id, string name, bool isEnabled, int batteryLevel) : base(id, name, isEnabled)
    {
        if (CheckId(id))
        {
            throw new ArgumentException("Invalid ID value. Required format: SW-1", id);
        }
        BatteryLevel = batteryLevel;
    }

    /// <summary>
    /// Notify level which is being implemented from IPowerNotifier interface
    /// </summary>
    public void Notify()
    {
        Console.WriteLine($"Battery level is low. Current level is: {BatteryLevel}");
    }

    /// <summary>
    /// Overrided TurnOn method with additional logic
    /// </summary>
    public override void TurnOn()
    {
        if (BatteryLevel < 11)
        {
            throw new EmptyBatteryException();
        }

        base.TurnOn();
        BatteryLevel -= 10;

        if (BatteryLevel < 20)
        {
            Notify();
        }
    }

    public override string ToString()
    {
        string enabledStatus = IsEnabled ? "enabled" : "disabled";
        return $"Smartwatch {Name} ({Id}) is {enabledStatus} and has {BatteryLevel}%";
    }
    
    private bool CheckId(string id) => id.Contains("E-");
}