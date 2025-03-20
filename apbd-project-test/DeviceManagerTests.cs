using apbd_project;
namespace apbd_project_test;

public class DeviceManagerTests
{
    [Fact]
    public void Test1()
    {
        var sw = new Smartwatch();
        sw.Id = "SW-3";
        sw.Name = "Smartwatch";
        sw.IsDeviceOn = false;
        sw.RemainingBatteryCharge = 12;

        var dm = new DeviceManager("/Users/deb/Desktop/PJATK/APBD/apbd-project/input.txt");
        dm.AddDevice(sw);

        Assert.Contains(sw, dm.GetListOfDevices());
    }
    
    [Fact]
    public void Test2()
    {
        var sw = new Smartwatch();
        sw.Id = "SW-3";
        sw.Name = "Smartwatch";
        sw.IsDeviceOn = false;
        sw.RemainingBatteryCharge = 12;
        
        var dm = new DeviceManager("/Users/deb/Desktop/PJATK/APBD/apbd-project/input.txt");
        dm.AddDevice(sw);
        dm.RemoveDevice("SW-3");
        
        Assert.DoesNotContain(sw, dm.GetListOfDevices());
    }
    
    [Fact]
    public void Test3()
    {
        var sw = new Smartwatch();
        sw.Id = "SW-3";
        sw.Name = "Smartwatch";
        sw.IsDeviceOn = false;
        sw.RemainingBatteryCharge = 12;
        
        var dm = new DeviceManager("Your Path");
        dm.AddDevice(sw);
        dm.EditDeviceData("SW-3", "IsTurnedOn", true);
        
        Assert.True(sw.IsDeviceOn);
    }
    
    [Fact]
    public void Test4()
    {
        var sw = new Smartwatch();
        sw.Id = "SW-3";
        sw.Name = "Smartwatch";
        sw.IsDeviceOn = false;
        sw.RemainingBatteryCharge = 12;
        
        var dm = new DeviceManager("/Users/deb/Desktop/PJATK/APBD/apbd-project/input.txt");
        dm.AddDevice(sw);
        dm.TurnOnDevice("SW-3");
        
        Assert.True(sw.IsDeviceOn);
    }
    
    [Fact]
    public void Test5()
    {
        var sw = new Smartwatch();
        sw.Id = "SW-3";
        sw.Name = "Smartwatch";
        sw.IsDeviceOn = false;
        sw.RemainingBatteryCharge = 12;
        
        var dm = new DeviceManager("/Users/deb/Desktop/PJATK/APBD/apbd-project/input.txt");
        dm.AddDevice(sw);
        dm.TurnOffDevice("SW-3");
        
        Assert.False(sw.IsDeviceOn);
    }
    
}