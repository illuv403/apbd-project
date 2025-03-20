namespace apbd_project;

public abstract class Device
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsDeviceOn { get; set; }
    
    public void TurnOn()
    {
        IsDeviceOn = true;
        Console.WriteLine("Device turned on");
    }
    
    public void TurnOff()
    {
        IsDeviceOn = false;
        Console.WriteLine("Device turned off");
    }
    
}