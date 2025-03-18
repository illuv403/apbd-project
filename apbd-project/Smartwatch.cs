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

    public void TurnOn()
    {
        if (_remainingBatteryCharge <= 11)
        {
            throw EmptyBatteryException();
        }
        
        _remainingBatteryCharge -= 10;
        IsDeviceOn = true;
        Console.WriteLine("Device turned on");
    }

    private Exception EmptyBatteryException()
    {
        return new Exception("Battery is empty, please charge up to turn on the device");
    }
}