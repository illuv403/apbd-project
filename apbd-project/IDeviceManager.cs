namespace apbd_project;

public interface IDeviceManager
{
    void AddDevice(Device newDevice);
    void EditDeviceData(Device editDevice);
    void RemoveDeviceById(string deviceId);
    void TurnOnDevice(string deviceId);
    void TurnOffDevice(string deviceId);
    Device GetDeviceById(string deviceId);
    void ShowAllDevices();
    void SaveDevices(string outputPath);
    List<Device> GetListOfDevices();
}