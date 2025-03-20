namespace apbd_project;

public class ConnectionException : Exception
{
    public ConnectionException() : base("Connection failed") { }
}