using apbd_project;
namespace apbd_project_test;

public class DeviceManagerTests
{
    [Fact]
    public void Test1()
    {
        var sw = new Smartwatch("SW-3", "Smartwatch", false, 12);

        var dm = DMFactory.InitializeDeviceManager("/Users/deb/Desktop/PJATK/APBD/apbd-project/input.txt");
        dm.AddDevice(sw);

        Assert.Contains(sw, dm.GetListOfDevices());
    }
    
    [Fact]
    public void Test2()
    {
        var sw = new Smartwatch("SW-3", "Smartwatch", false, 12);

        var dm = DMFactory.InitializeDeviceManager("/Users/deb/Desktop/PJATK/APBD/apbd-project/input.txt");
        dm.AddDevice(sw);
        dm.RemoveDeviceById("SW-3");
        
        Assert.DoesNotContain(sw, dm.GetListOfDevices());
    }
    
    [Fact]
    public void Test3()
    {
        var sw = new Smartwatch("SW-3", "Smartwatch", false, 12);

        var dm = DMFactory.InitializeDeviceManager("/Users/deb/Desktop/PJATK/APBD/apbd-project/input.txt");
        dm.AddDevice(sw);
        var editSmartwatch = new Smartwatch("SW-3", "Smartwatch", true, 12);
        dm.EditDeviceData(editSmartwatch);
        
        Assert.True(sw.IsEnabled);
    }
    
    [Fact]
    public void Test4()
    {
        var sw = new Smartwatch("SW-3", "Smartwatch", false, 12);
        
        var dm = DMFactory.InitializeDeviceManager("/Users/deb/Desktop/PJATK/APBD/apbd-project/input.txt");
        dm.AddDevice(sw);
        dm.TurnOnDevice("SW-3");
        
        Assert.True(sw.IsEnabled);
    }
    
    [Fact]
    public void Test5()
    {
        var sw = new Smartwatch("SW-3", "Smartwatch", false, 12);
        
        var dm = DMFactory.InitializeDeviceManager("/Users/deb/Desktop/PJATK/APBD/apbd-project/input.txt");
        dm.AddDevice(sw);
        dm.TurnOffDevice("SW-3");
        
        Assert.False(sw.IsEnabled);
    }
    
}