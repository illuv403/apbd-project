namespace apbd_project;

public class EmptySystemException : Exception
{
    public EmptySystemException() : base("Operation system is not installed.") { }
}