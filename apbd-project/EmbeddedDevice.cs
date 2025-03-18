namespace apbd_project;
using System;
using System.Text.RegularExpressions;

public class EmbeddedDevice : Device
{
    private string _ip;
    private string _networkName;

    public string IP
    {
        get
        {
            return _ip;
        }
        set
        {
            if (!Regex.IsMatch(value,
                    @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$"))
            {
                throw new ArgumentException("Invalid IP address");
            }
            _ip = value;
        }
    }

    public string NetworkName
    {
        get
        {
            return _networkName;
        }
        set
        {
            _networkName = value;
        }
    }
    
    public void Connect()
    {
        if (_networkName.Contains("MD Ltd."))
        {
            throw ConnectionException();
        }
        Console.WriteLine("Connection succeed");
    }

    public void TurnOn()
    {
        Connect();
        IsDeviceOn = true;
        Console.WriteLine("Device turned on");
    }
    
    public Exception ConnectionException()
    {
        return new Exception("Connection failed");
    }
    
}