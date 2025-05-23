using Entities;

namespace Logic;

public interface IDeviceService
{
    public IEnumerable<object> GetAllDevices();
    public object? GetDevice(string id);
    public bool AddDevice(Device device);
    public bool UpdateDevice(Device device);
    public bool DeleteDevice(string id);
}