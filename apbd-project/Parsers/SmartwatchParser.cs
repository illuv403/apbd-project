using Entities;

namespace apbd_project;

public class SmartwatchParser : IParser
{
    /// <summary>
    /// Parses Smartwatch device from line
    /// </summary>
    /// <param name="line">The string containing device information</param>
    /// <param name="lineNumber">The line number for error tracking</param>
    /// <returns>Returns new instance of Smartwatch class</returns>
    /// <exception cref="ArgumentException">Is thrown if parsing fails</exception>
    public static Device Parse(string line, int lineNumber)
    {
        const int BatteryPosition = 3;
        var infoSplits = line.Split(',');

        if (infoSplits.Length < 4)
            throw new ArgumentException($"Corrupted line {lineNumber}: Expected at least 4 elements but got {infoSplits.Length}.", line);

        if (!bool.TryParse(infoSplits[2], out bool isEnabled))
            throw new ArgumentException($"Corrupted line {lineNumber}: can't parse enabled status.", line);

        if (!int.TryParse(infoSplits[BatteryPosition].Replace("%", ""), out int batteryLevel))
            throw new ArgumentException($"Corrupted line {lineNumber}: can't parse battery level.", line);

        return new Smartwatch(infoSplits[0], infoSplits[1], isEnabled, batteryLevel);
    }
}