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

    public IEnumerable<object> GetAllDevices() => _repository.GetAllDevices();
    public Device? GetDevice(string Id) => _repository.GetDeviceById(Id);
    public bool AddDevice(Device device) => _repository.AddDevice(device);
    public bool UpdateDevice(Device device) => _repository.UpdateDevice(device);
    public bool DeleteDevice(string Id) => _repository.DeleteDevice(Id);
}