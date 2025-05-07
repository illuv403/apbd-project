namespace Entities;

/// <summary>
/// Exception which is being thrown when battery level is too low to turn on the device
/// </summary>
public class EmptyBatteryException : Exception
{
    public EmptyBatteryException() : base("Battery level is too low to turn it on.") { }
}