namespace apbd_project;

public class PC : Device
{
    private string? _os = null;
    public string OS
    {
        get
        {
            return _os;
        }
        set
        {
            _os = value;
        }
    }

    public void launchPC()
    {
        if (_os == null)
        {
            throw EmptySystemException();
        }
        IsDeviceOn = true;
        Console.WriteLine("PC launched");
    }

    private Exception EmptySystemException()
    {
        return new Exception("Please specify the OS");
    }
    
    public override string ToString()
    {
        return $"{Id}, Name: {Name}, Status: {(IsDeviceOn ? "On" : "Off")}, OS: {_os}";
    }
}