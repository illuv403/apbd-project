using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;
using apbd_project;

var sw = new Smartwatch();
sw.Id = 1;
sw.Name = "Smartwatch";
sw.State = "On";
sw.RemainingBatteryCharge = 19;
sw.NotifyAboutLowPower();
sw.TurnOn();
