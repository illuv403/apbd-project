namespace apbd_project;

public class EmbeddedParser : IParser
{
    /// <summary>
    /// Parses Embedded device from line
    /// </summary>
    /// <param name="line">The string containing device information</param>
    /// <param name="lineNumber">The line number for error tracking</param>
    /// <returns>Returns new instance of Embedded class</returns>
    /// <exception cref="ArgumentException">Is thrown if parsing fails</exception>
    public static Device Parse(string line, int lineNumber)
    {
        const int IpAddressPosition = 3;
        const int NetworkNamePosition = 4;
        var infoSplits = line.Split(',');

        if (infoSplits.Length < 5) 
            throw new ArgumentException($"Corrupted line {lineNumber}: Expected at least 5 elements but got {infoSplits.Length}.", line);

        if (!bool.TryParse(infoSplits[2], out bool isEnabled))
            throw new ArgumentException($"Corrupted line {lineNumber}: can't parse enabled status.", line);

        return new Embedded(infoSplits[0], infoSplits[1], isEnabled, infoSplits[IpAddressPosition], infoSplits[NetworkNamePosition]);
    }
}