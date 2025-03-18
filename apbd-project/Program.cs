using apbd_project;

var sw = new Smartwatch();
sw.Id = "SW-1";
sw.Name = "Smartwatch";
sw.IsDeviceOn = true;
sw.RemainingBatteryCharge = 19;
sw.NotifyAboutLowPower();
sw.TurnOn();

var pc = new PC();
pc.Id = "PC-1";
pc.Name = "MY_PC";
pc.IsDeviceOn = true;
pc.OS = "Windows 10";
pc.launchPC();
Console.WriteLine(pc.GetType() == typeof(PC));

EmbeddedDevice ed = new EmbeddedDevice();
ed.IP = "255.255.255.256";