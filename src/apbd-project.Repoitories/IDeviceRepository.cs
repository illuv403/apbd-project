using Entities;

namespace Repository;

public interface IDeviceRepository
{
    IEnumerable<object> GetAllDevices();
    object? GetDeviceById(string id);
    bool AddDevice(Device device);
    bool UpdateDevice(Device device);
    bool DeleteDevice(string id);
}