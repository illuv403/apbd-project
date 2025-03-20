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
            throw new EmptySystemException();
        }
        TurnOn();
    }
    
    public override string ToString()
    {
        return $"{Id}, Name: {Name}, Status: {(IsDeviceOn ? "On" : "Off")}, OS: {_os}";
    }
}