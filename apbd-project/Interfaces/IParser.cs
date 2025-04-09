using Entities;

namespace apbd_project;

/// <summary>
/// Interface creating for parsing of the devices
/// </summary>
public interface IParser
{
    static abstract Device Parse(string line, int lineNumber);
}