namespace Entities;

/// <summary>
/// Exception which is being thrown when OperationSystem parameter is not set
/// </summary>
public class EmptySystemException : Exception
{
    public EmptySystemException() : base("Operation system is not installed.") { }
}