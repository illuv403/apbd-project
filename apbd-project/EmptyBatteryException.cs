namespace apbd_project;

public class EmptyBatteryException : Exception
{
    public EmptyBatteryException() : base("Battery level is too low to turn it on.") { }
}