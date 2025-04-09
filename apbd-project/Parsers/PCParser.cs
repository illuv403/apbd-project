using Entities;

namespace apbd_project;


public class PCParser : IParser
{
    /// <summary>
    /// Parses PersonalComputer device from line
    /// </summary>
    /// <param name="line">The string containing device information</param>
    /// <param name="lineNumber">The line number for error tracking</param>
    /// <returns>Returns new instance of PersonalComputer class</returns>
    /// <exception cref="ArgumentException">Is being thrown if any problem exception with parsing of device is met</exception>
    public static Device Parse(string line, int lineNumber)
    {
        const int SystemPosition = 3;
        var infoSplits = line.Split(',');

        if (infoSplits.Length < 4)
            throw new ArgumentException($"Corrupted line {lineNumber}: Expected at least 4 elements but got {infoSplits.Length}.", line);

        if (!bool.TryParse(infoSplits[2], out bool isEnabled))
            throw new ArgumentException($"Corrupted line {lineNumber}: can't parse enabled status.", line);

        return new PersonalComputer(infoSplits[0], infoSplits[1], isEnabled, infoSplits[SystemPosition]);
    }
}