namespace apbd_project;

using System.IO;
using System.Text;

/// <summary>
/// Class for managing actions with files
/// </summary>
public class FileManager
{
    /// <summary>
    /// Method to read all lines from the file and returns an array of lines
    /// </summary>
    /// <param name="filePath">A path to the file from which we read lines</param>
    /// <exception cref="FileNotFoundException"></exception>
    public string[] ReadLines(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("The input device file could not be found.");
        }

        return File.ReadAllLines(filePath);
    }

    /// <summary>
    /// Method to save devices from the list to certain file
    /// </summary>
    /// <param name="outputPath">Path to file in which lines will be saved</param>
    /// <param name="devices">List of devices which we will save in the file</param>
    public void SaveDevices(string outputPath, List<Device> devices)
    {
        StringBuilder devicesSb = new();

        foreach (var storedDevice in devices)
        {
            if (storedDevice is Smartwatch smartwatchCopy)
            {
                devicesSb.AppendLine($"{smartwatchCopy.Id},{smartwatchCopy.Name}," +
                                     $"{smartwatchCopy.IsEnabled},{smartwatchCopy.BatteryLevel}%");
            }
            else if (storedDevice is PersonalComputer pcCopy)
            {
                devicesSb.AppendLine($"{pcCopy.Id},{pcCopy.Name}," +
                                     $"{pcCopy.IsEnabled},{pcCopy.OperatingSystem}");
            }
            else if (storedDevice is Embedded embeddedCopy)
            {
                devicesSb.AppendLine($"{embeddedCopy.Id},{embeddedCopy.Name}," +
                                     $"{embeddedCopy.IsEnabled},{embeddedCopy.IpAddress}," +
                                     $"{embeddedCopy.NetworkName}");
            }
        }

        File.WriteAllLines(outputPath, devicesSb.ToString().Split('\n'));
    }
    
    /// <summary>
    /// A method which parses all the lines from the list of lines
    /// </summary>
    /// <param name="lines">A list of lines which were read from the file</param>
    /// <exception cref="ArgumentException"></exception>
    public List<Device> ParseDevices(string[] lines)
    {
        List<Device> devices = new List<Device>();
        DeviceParser deviceParser = new DeviceParser(); 

        for (int i = 0; i < lines.Length; i++)
        {
            try
            {
                Device parsedDevice;

                if (lines[i].StartsWith("P-"))
                {
                    parsedDevice = deviceParser.ParsePC(lines[i], i);
                }
                else if (lines[i].StartsWith("SW-"))
                {
                    parsedDevice = deviceParser.ParseSmartwatch(lines[i], i);
                }
                else if (lines[i].StartsWith("ED-"))
                {
                    parsedDevice = deviceParser.ParseEmbedded(lines[i], i);
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