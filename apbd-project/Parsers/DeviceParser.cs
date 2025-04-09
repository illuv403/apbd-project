using Entities;

namespace apbd_project;

/// <summary>
/// Provides method for parsing devices
/// </summary>
public class DeviceParser
{
    /// <summary>
    /// A method which parses all the lines from the list of lines
    /// </summary>
    /// <param name="lines">A list of lines which were read from the file</param>
    /// <exception cref="ArgumentException">Is being thrown if there is an exception while parsing of devices</exception>
    public List<Device> ParseDevices(string[] lines)
    {
        List<Device> devices = new List<Device>();

        for (int i = 0; i < lines.Length; i++)
        {
            try
            {
                Device parsedDevice;

                if (lines[i].StartsWith("P-"))
                {
                    parsedDevice = PCParser.Parse(lines[i], i);
                }
                else if (lines[i].StartsWith("SW-"))
                {
                    parsedDevice = SmartwatchParser.Parse(lines[i], i);
                }
                else if (lines[i].StartsWith("ED-"))
                {
                    parsedDevice = EmbeddedParser.Parse(lines[i], i);
                }
                else
                {
                    throw new ArgumentException($"Line {i} is corrupted.");
                }

                devices.Add(parsedDevice);
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong during parsing this line: {lines[i]}. The exception message: {ex.Message}");
            }
        }

        return devices;
    }
}
