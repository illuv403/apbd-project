namespace Entities;

/// <summary>
/// Exception which is being thrown when the network name is wrong
/// </summary>
public class ConnectionException : Exception
{
    public ConnectionException() : base("Wrong network name.") { }
}