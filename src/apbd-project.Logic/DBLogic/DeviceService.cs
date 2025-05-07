using Entities;
using Repository;

namespace Logic;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _repository;

    public DeviceService(string connectionString)
    {
        _repository = new DeviceRepository(connectionString);
    }

    public IEnumerable<object> GetAllDevices()
    {
        return _repository.GetAllDevices();
    }

    public Device? GetDevice(string id)
    {
        return _repository.GetDeviceById(id);
    }

    public bool AddDevice(Device device)
    {
        return _repository.AddDevice(device);
    }

    public bool UpdateDevice(Device device)
    {
        return _repository.UpdateDevice(device);
    }

    public bool DeleteDevice(string id)
    {
        return _repository.DeleteDevice(id);
    }
}