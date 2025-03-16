using apbd_project;

var sw = new Smartwatch();
sw.Id = 1;
sw.Name = "Smartwatch";
sw.State = "On";
sw.RemainingBatteryCharge = 19;
sw.NotifyAboutLowPower();
sw.TurnOn();

var pc = new PC();
pc.Id = 2;
pc.Name = "MY_PC";
pc.State = "On";
pc.OS = "Windows 10";
pc.launchPC();
