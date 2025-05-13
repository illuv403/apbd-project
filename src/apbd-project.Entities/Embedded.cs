namespace Entities;

using System.Text.RegularExpressions;

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
    /// <exception cref="ArgumentException">Is being thrown if IpAddress is not in IPV4 format</exception>
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
    /// <param name="id">ID of embedded device in format of "E-[number]"</param>
    /// <param name="name">Just the name of the embedded device</param>
    /// <param name="isEnabled">Is embedded device on</param>
    /// <param name="ipAddress">Ip address of the ED</param>
    /// <param name="networkName">The name of the network (must contain "MD Ltd.")</param>
    /// <exception cref="ArgumentException">Is being thrown if the id is not in the right format</exception>
    public Embedded(string id, string name, bool isEnabled, string ipAddress, string networkName) : base(id, name, isEnabled)
    {
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
    /// <exception cref="ConnectionException">Is being thrown if network name does not contain "MD Ltd."</exception>
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
}