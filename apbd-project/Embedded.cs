using System.Text.RegularExpressions;

namespace apbd_project;

/// <summary>
/// Embedded device class which inherits from device class
/// </summary>
public class Embedded : Device
{
    public string NetworkName { get; set; }
    private string _ipAddress;
    private bool _isConnected = false;

    /// <summary>
    /// IpAddress property which checks for right IP pattern when setting new one 
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public string IpAddress
    {
        get => _ipAddress;
        set
        {
            if (!Regex.IsMatch(value,
                    @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$"))
            {
                throw new ArgumentException("Wrong IP address format.");  
            }
            _ipAddress = value;

              
        }
    }
    
    /// <summary>
    /// Embedded device class constructor which sets network name and IP address
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="isEnabled"></param>
    /// <param name="ipAddress"></param>
    /// <param name="networkName"></param>
    /// <exception cref="ArgumentException"></exception>
    public Embedded(string id, string name, bool isEnabled, string ipAddress, string networkName) : base(id, name, isEnabled)
    {
        if (CheckId(id))
        {
            throw new ArgumentException("Invalid ID value. Required format: E-1", id);
        }

        IpAddress = ipAddress;
        NetworkName = networkName;
    }

    /// <summary>
    /// Overrided TurnOn method with additional logic
    /// </summary>
    public override void TurnOn()
    {
        Connect();
        base.TurnOn();
    }
    /// <summary>
    /// Overrided TurnOff method with additional logic
    /// </summary>
    public override void TurnOff()
    {
        _isConnected = false;
        base.TurnOff();
    }

    public override string ToString()
    {
        string enabledStatus = IsEnabled ? "enabled" : "disabled";
        return $"Embedded device {Name} ({Id}) is {enabledStatus} and has IP address {IpAddress}";
    }

    /// <summary>
    /// Connect method which checks if network name contains MD Ltd. and connects device
    /// </summary>
    /// <exception cref="ConnectionException"></exception>
    private void Connect()
    {
        if (NetworkName.Contains("MD Ltd."))
        {
            _isConnected = true;
        }
        else
        {
            throw new ConnectionException();
        }
    }
    
    private bool CheckId(string id) => id.Contains("E-");
}
