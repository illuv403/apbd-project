using System.Linq.Expressions;
using apbd_project;

var sw = new Smartwatch();
sw.Id = 1;
sw.Name = "Smartwatch";
sw.State = "On";
sw.RemainingBatteryCharge = 18;
sw.NotifyAboutLowPower();
