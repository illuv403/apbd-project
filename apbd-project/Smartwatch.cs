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
                Console.WriteLine("Battery charge must be between 0 and 100");
            }
        }
    }

    public void NotifyAboutLowPower()
    {
        if (RemainingBatteryCharge < 20)
        {
            Console.WriteLine("Low battery");       
        }
    }
    
}