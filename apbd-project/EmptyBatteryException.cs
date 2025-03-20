namespace apbd_project;

public class EmptyBatteryException : Exception
{
    public EmptyBatteryException() : base("Battery is empty, please charge up to turn on the device") { }
}