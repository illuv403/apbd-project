using Logic;

namespace API.Controllers;

using Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/devices")]
[ApiController]
public class DeviceController : ControllerBase
{
    private readonly DeviceManager _deviceManager;

    public DeviceController()
    {
        _deviceManager = new DeviceManager();
    }

    /// <summary>
    /// Retrieves a list of devices with short information.
    /// </summary>
    /// <returns>List of devices</returns>
    [HttpGet]
    public IResult GetAllDevices()
    {
        var devices = _deviceManager.GetListOfDevices();
        var devicesInformation = devices.Select(d => new
        {
            d.Id,
            d.Name
        });
        return Results.Ok(devicesInformation);
    }

    /// <summary>
    /// Retrieves a specific device by ID with all data.
    /// </summary>
    /// <param name="id">The ID of the device</param>
    /// <returns>Device details</returns>
    [HttpGet("{id}")]
    public IResult GetDevice(string id)
    {
        var device = _deviceManager.GetDeviceById(id);
        return Results.Ok(device);
    }
        // I am a creep, I am weirdo, what the hell am I doing here?
        // I dont belong here...
    //  <summary>
    //Creates a new device.
    /// </summary>
    /// <param name="device">The device to create</param>
    /// <returns>Success message</returns>
    [HttpPost]
    public IResult CreateDevice([FromBody] Device device)
    {
        _deviceManager.AddDevice(device);
        return Results.Created();
    }

    /// <summary>
    /// Edits an existing device.
    /// </summary>
    /// <param name="device">The updated device data</param>
    /// <returns>Success message</returns>
    [HttpPut]
    public IResult EditDevice([FromBody] Device device)
    {
        _deviceManager.EditDeviceData(device);
        return Results.Ok($"Device with ID {device.Id} updated successfully");
    }

    /// <summary>
    /// Deletes a device by ID.
    /// </summary>
    /// <param name="id">The ID of the device to delete</param>
    /// <returns>Success message</returns>
    [HttpDelete("{id}")]
    public IResult DeleteDevice(string id)
    {
        _deviceManager.RemoveDeviceById(id);
        return Results.Ok($"Device with ID {id} deleted successfully");
    }
}