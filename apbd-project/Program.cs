using apbd_project;

var sw = new Smartwatch();
sw.Id = 1;
sw.Name = "Smartwatch";
sw.IsDeviceOn = true;
sw.RemainingBatteryCharge = 19;
sw.NotifyAboutLowPower();
sw.TurnOn();

var pc = new PC();
pc.Id = 2;
pc.Name = "MY_PC";
pc.IsDeviceOn = true;
pc.OS = "Windows 10";
pc.launchPC();

EmbeddedDevice ed = new EmbeddedDevice();
ed.IP = "255.255.255.256";