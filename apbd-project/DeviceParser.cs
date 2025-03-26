namespace apbd_project;

/// <summary>
/// Provides methods for parsing devices
/// </summary>
public class DeviceParser
{
    private const int MinimumRequiredElements = 4;

    private const int IndexPosition = 0;
    private const int DeviceNamePosition = 1;
    private const int EnabledStatusPosition = 2;

    /// <summary>
    /// Parses PersonalComputer device from line
    /// </summary>
    /// <param name="line">The string containing device information</param>
    /// <param name="lineNumber">The line number for error tracking</param>
    /// <returns>Returns new instance of PersonalComputer class</returns>
    /// <exception cref="ArgumentException">Is being thrown if any problem exception with parsing of device is met</exception>
    public PersonalComputer ParsePC(string line, int lineNumber)
    {
        const int SystemPosition = 3;

        var infoSplits = line.Split(',');

        if (infoSplits.Length < MinimumRequiredElements)
        {
            throw new ArgumentException($"Corrupted line {lineNumber}: Expected at least {MinimumRequiredElements} elements but got {infoSplits.Length}.", line);
        }

        if (!bool.TryParse(infoSplits[EnabledStatusPosition], out bool isEnabled))
        {
            throw new ArgumentException($"Corrupted line {lineNumber}: can't parse enabled status for computer.", line);
        }

        return new PersonalComputer(infoSplits[IndexPosition], infoSplits[DeviceNamePosition], 
            isEnabled, infoSplits[SystemPosition]);
    }

    /// <summary>
    /// Parses Smartwatch device from line
    /// </summary>
    /// <param name="line">The string containing device information</param>
    /// <param name="lineNumber">The line number for error tracking</param>
    /// <returns>Returns new instance of Smartwatch class</returns>
    /// <exception cref="ArgumentException">Is being thrown if any problem exception with parsing of device is met</exception>
    public Smartwatch ParseSmartwatch(string line, int lineNumber)
    {
        const int BatteryPosition = 3;

        var infoSplits = line.Split(',');

        if (infoSplits.Length < MinimumRequiredElements)
        {
            throw new ArgumentException($"Corrupted line {lineNumber}: Expected at least {MinimumRequiredElements} elements but got {infoSplits.Length}.", line);
        }

        if (!bool.TryParse(infoSplits[EnabledStatusPosition], out bool isEnabled))
        {
            throw new ArgumentException($"Corrupted line {lineNumber}: can't parse enabled status for smartwatch.", line);
        }

        if (!int.TryParse(infoSplits[BatteryPosition].Replace("%", ""), out int batteryLevel))
        {
            throw new ArgumentException($"Corrupted line {lineNumber}: can't parse battery level for smartwatch.", line);
        }

        return new Smartwatch(infoSplits[IndexPosition], infoSplits[DeviceNamePosition], 
            isEnabled, batteryLevel);
    }

    /// <summary>
    /// Parses Embedded device from line
    /// </summary>
    /// <param name="line">The string containing device information</param>
    /// <param name="lineNumber">The line number for error tracking</param>
    /// <returns>Returns new instance of Embedded class</returns>
    /// <exception cref="ArgumentException">Is being thrown if any problem exception with parsing of device is met</exception>
    public Embedded ParseEmbedded(string line, int lineNumber)
    {
        const int IpAddressPosition = 3; 
        const int NetworkNamePosition = 4; 

        var infoSplits = line.Split(',');

        if (infoSplits.Length < MinimumRequiredElements) 
        {
            throw new ArgumentException($"Corrupted line {lineNumber}: Expected at least {MinimumRequiredElements + 1} elements but got {infoSplits.Length}.", line);
        }

        if (!bool.TryParse(infoSplits[EnabledStatusPosition], out bool isEnabled))
        {
            throw new ArgumentException($"Corrupted line {lineNumber}: can't parse enabled status for embedded device.", line);
        }

        return new Embedded(infoSplits[IndexPosition], infoSplits[DeviceNamePosition], 
            isEnabled, infoSplits[IpAddressPosition], 
            infoSplits[NetworkNamePosition]);
    }
}
