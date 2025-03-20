namespace apbd_project;

public class Smartwatch : Device, IPowerNotifier
{
    private int _remainingBatteryCharge;
    public int RemainingBatteryCharge
    {
        get
        {
            return _remainingBatteryCharge;
        }
        set
        {
            if (value >= 0 & value <= 100)
            {
                _remainingBatteryCharge = value;
            }
            else
            {
                throw new Exception("Battery charge must be between 0 and 100");
            }
        }
    }

    public void NotifyAboutLowPower()
    {
        if (RemainingBatteryCharge < 20)
        {
            Console.WriteLine("Low battery, please charge up");       
        }
    }

    public void TurnOnSmartwatch()
    {
        if (RemainingBatteryCharge <= 11)
        {
            throw new EmptyBatteryException();
        }
        
        RemainingBatteryCharge -= 10;
        TurnOn();
        Console.WriteLine("Device turned on");
    }
    
    public override string ToString()
    {
        return $"{Id}, Name: {Name}, Status: {(IsDeviceOn ? "On" : "Off")}, Battery: {_remainingBatteryCharge}%";
    }
}