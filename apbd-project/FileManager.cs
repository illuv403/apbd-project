using Entities;

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
    /// <returns>A list of strings with all lines from the file in it</returns>
    /// <exception cref="FileNotFoundException">Is being thrown if file was not found at specified path</exception>
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
}