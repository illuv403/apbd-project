using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Logic;

namespace API.Controllers;

using Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/devices")]
[ApiController]
public class DevicesController : ControllerBase
{
    private readonly DeviceService _deviceService;

    public DevicesController(DeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    /// <summary>
    /// Retrieves a list of devices with short information.
    /// </summary>
    /// <returns>List of devices</returns>
    [HttpGet]
    public IResult GetAllDevices()
    {
        var devices = _deviceService.GetAllDevices();
        return Results.Ok(devices);
    }

    /// <summary>
    /// Retrieves a specific device by ID with all data.
    /// </summary>
    /// <param name="id">The ID of the device</param>
    /// <returns>Device details</returns>
    [HttpGet("{id}")]
    public IResult GetDevice(string id)
    {
        var device = _deviceService.GetDevice(id);
        if (device == null)
            return Results.NotFound();
        return Results.Ok(device);
    }

    /// <summary>
    /// Creates a new device from JSON or plain text data.
    /// </summary>
    /// <param name="request">The HTTP request containing the device data</param>
    /// <returns>Created device details</returns>
    [HttpPost]
    public async Task<IResult> CreateDevice()
    {
        try
        {
            string? contentType = HttpContext.Request.ContentType?.ToLower();
            switch (contentType)
            {
                case "application/json":
                {
                    using var reader = new StreamReader(HttpContext.Request.Body);
                    string rawJson = await reader.ReadToEndAsync();
                    var json = JsonNode.Parse(rawJson);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var deviceJson = json.ToJsonString();
                    if (string.IsNullOrEmpty(deviceJson))
                        return Results.BadRequest("Device data is missing.");

                    var deviceId = JsonSerializer.Deserialize<Dictionary<string, object>>(deviceJson, options);

                    var id = deviceId["id"].ToString();
                    Device device = null;
                    if (id.StartsWith("E-"))
                    {
                        device = JsonSerializer.Deserialize<Embedded>(deviceJson, options);
                    }
                    else if (id.StartsWith("SW-"))
                    {
                        device = JsonSerializer.Deserialize<Smartwatch>(deviceJson, options);
                    }
                    else if (id.StartsWith("P-"))
                    {
                        device = JsonSerializer.Deserialize<PersonalComputer>(deviceJson, options);
                    }

                    if (device == null)
                        return Results.BadRequest("Invalid device type.");

                    _deviceService.AddDevice(device);
                    return Results.Created();
                }
                case "text/plain":
                {
                    using var reader = new StreamReader(HttpContext.Request.Body);
                    var text = await reader.ReadToEndAsync();

                    var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var dict = lines.Select(line => line.Split(':'))
                        .Where(parts => parts.Length == 2)
                        .ToDictionary(parts => parts[0].Trim(), parts => parts[1].Trim());

                    var id = dict["Id"];
                    var name = dict["Name"];
                    var isEnabled = bool.Parse(dict["IsEnabled"]);

                    Device device = null;
                    if (id.StartsWith("E-"))
                    {
                        device = new Embedded(
                            id,
                            name,
                            isEnabled,
                            dict.GetValueOrDefault("IpAddress"),
                            dict.GetValueOrDefault("NetworkName"));
                    }
                    else if (id.StartsWith("SW-"))
                    {
                        device = new Smartwatch(
                            id,
                            name,
                            isEnabled,
                            int.Parse(dict["BatteryLevel"]));
                    }
                    else if (id.StartsWith("P-"))
                    {
                        device = new PersonalComputer(
                            id,
                            name,
                            isEnabled,
                            dict.GetValueOrDefault("OperatingSystem"));
                    }

                    if (device == null)
                        return Results.BadRequest();

                    _deviceService.AddDevice(device);
                    return Results.Created();
                }
                default:
                    return Results.Conflict();
            }
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Updates an existing device from JSON or plain text data.
    /// </summary>
    /// <returns>Success status</returns>
    [HttpPut]
    public async Task<IResult> UpdateDevice()
    {
        try
        {
            string? contentType = HttpContext.Request.ContentType?.ToLower();
            Device? device = null;

            switch (contentType)
            {
                case "application/json":
                {
                    using var reader = new StreamReader(HttpContext.Request.Body);
                    string rawJson = await reader.ReadToEndAsync();
                    var json = JsonNode.Parse(rawJson);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var deviceJson = json.ToJsonString();
                    if (string.IsNullOrEmpty(deviceJson))
                        return Results.BadRequest("Device data is missing.");

                    var deviceId = JsonSerializer.Deserialize<Dictionary<string, object>>(deviceJson, options);

                    var id = deviceId["id"].ToString();

                    if (id.StartsWith("E-"))
                    {
                        device = JsonSerializer.Deserialize<Embedded>(deviceJson, options);
                    }
                    else if (id.StartsWith("SW-"))
                    {
                        device = JsonSerializer.Deserialize<Smartwatch>(deviceJson, options);
                    }
                    else if (id.StartsWith("P-"))
                    {
                        device = JsonSerializer.Deserialize<PersonalComputer>(deviceJson, options);
                    }

                    break;
                }
                case "text/plain":
                {
                    using var reader = new StreamReader(HttpContext.Request.Body);
                    var text = await reader.ReadToEndAsync();

                    var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var dict = lines.Select(line => line.Split(':'))
                        .Where(parts => parts.Length == 2)
                        .ToDictionary(parts => parts[0].Trim(), parts => parts[1].Trim());

                    var id = dict["Id"];
                    var name = dict["Name"];
                    var isEnabled = bool.Parse(dict["IsEnabled"]);
                    string? rv = dict["RV"];
                    
                    if (id.StartsWith("E-"))
                    {
                        device = new Embedded(
                                id,
                                name,
                                isEnabled,
                                dict.GetValueOrDefault("IpAddress"),
                                dict.GetValueOrDefault("NetworkName"))
                        {
                            RV = rv
                        };
                    }
                    else if (id.StartsWith("SW-"))
                    {
                        device = new Smartwatch(
                            id,
                            name,
                            isEnabled,
                            int.Parse(dict["BatteryLevel"]))
                        {
                            RV = rv
                        };
                    }
                    else if (id.StartsWith("P-"))
                    {
                        device = new PersonalComputer(
                            id,
                            name,
                            isEnabled,
                            dict.GetValueOrDefault("OperatingSystem"))
                        {
                            RV = rv
                        };
                    }

                    break;
                }
                default:
                    return Results.Conflict();
            }

            var success = _deviceService.UpdateDevice(device);
            if (!success)
                return Results.NotFound();

            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a device by ID.
    /// </summary>
    /// <param name="id">The ID of the device to delete</param>
    /// <returns>Success message</returns>
    [HttpDelete("{id}")]
    public IResult DeleteDevice(string id)
    {
        bool isDeleted = _deviceService.DeleteDevice(id);
        if (!isDeleted)
            return Results.NotFound();
        return Results.Ok($"Device with ID {id} deleted successfully");
    }
}