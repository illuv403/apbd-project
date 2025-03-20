namespace apbd_project;

public class EmptySystemException : Exception
{
    public EmptySystemException() : base("Please specify the OS") { }
}