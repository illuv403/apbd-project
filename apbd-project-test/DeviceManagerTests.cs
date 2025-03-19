using apbd_project;
namespace apbd_project_test;

public class DeviceManagerTests
{
    [Fact]
    public void Test1()
    {
        var sw = new Smartwatch();
        sw.Id = "SW-2";
        sw.Name = "Smartwatch";
        sw.IsDeviceOn = false;
        sw.RemainingBatteryCharge = 12;
    }
}