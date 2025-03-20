using apbd_project;

var sw = new Smartwatch();
sw.Id = "SW-2";
sw.Name = "Smartwatch";
sw.IsDeviceOn = false;
sw.RemainingBatteryCharge = 12;

DeviceManager dm = new DeviceManager("/Users/deb/Desktop/PJATK/APBD/apbd-project/input.txt");
dm.AddDevice(sw);
dm.RemoveDevice("ED-2");
dm.EditDeviceData("ED-1", "IP", "192.168.1.100");
dm.TurnOnDevice("SW-2");
dm.TurnOffDevice("SW-1");
dm.SaveListOfDevices("/Users/deb/Desktop/PJATK/APBD/apbd-project/input.txt");
dm.ShowAllDevices();
