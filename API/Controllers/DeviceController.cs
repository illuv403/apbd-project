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
            d.Name,
            d.IsEnabled
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

    /// <summary>
    /// Adds new PC.
    /// </summary>
    /// <param name="pc">The pc to add</param>
    /// <returns>Success message</returns>
    [HttpPost("pc")]
    public IResult CreatePc([FromBody] PersonalComputer? pc)
    {
        _deviceManager.AddDevice(pc);
        return Results.Created();
    }
    
    /// <summary>
    /// Adds new Smartwatch.
    /// </summary>
    /// <param name="sw">The smartwatch to add</param>
    /// <returns>Success message</returns>
    [HttpPost("smartwatch")]
    public IResult CreateSmartwatch([FromBody] Smartwatch? sw)
    {
        _deviceManager.AddDevice(sw);
        return Results.Created();
    }
    
    /// <summary>
    /// Adds new Embedded device.
    /// </summary>
    /// <param name="ed">The embedded device to add</param>
    /// <returns>Success message</returns>
    [HttpPost("embedded")]
    public IResult CreateEmbedded([FromBody] Embedded? ed)
    {
        _deviceManager.AddDevice(ed);
        return Results.Created();
    }
    
    /// <summary>
    /// Edits an existing pc.
    /// </summary>
    /// <param name="pc">The updated pc data</param>
    /// <returns>Success message</returns>
    [HttpPut("pc")]
    public IResult EditPc([FromBody] PersonalComputer pc)
    {
        _deviceManager.EditDeviceData(pc);
        return Results.Ok($"Device with ID {pc.Id} updated successfully");
    }
    
    /// <summary>
    /// Edits an existing smartwatch.
    /// </summary>
    /// <param name="sw">The updated smartwatch data</param>
    /// <returns>Success message</returns>
    [HttpPut("smartwatch")]
    public IResult EditSmartwatch([FromBody] Smartwatch sw)
    {
        _deviceManager.EditDeviceData(sw);
        return Results.Ok($"Device with ID {sw.Id} updated successfully");
    }
    
    /// <summary>
    /// Edits an existing embedded device.
    /// </summary>
    /// <param name="ed">The updated embedded device data</param>
    /// <returns>Success message</returns>
    [HttpPut("embedded")]
    public IResult EditEmbedded([FromBody] Embedded ed)
    {
        _deviceManager.EditDeviceData(ed);
        return Results.Ok($"Device with ID {ed.Id} updated successfully");
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