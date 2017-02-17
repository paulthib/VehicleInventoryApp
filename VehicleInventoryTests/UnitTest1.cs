using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleInventoryDomain;

namespace VehicleInventoryTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod_BasicFeatures()
        {
            IVehicleInventory inventory = new VehicleInventory();
            var vehicle = VehicleFactory.CreateVehicle("vinnumber3", VehicleMake.Tesla, "model", 2017, "yellow", 1500, 2500, 100);
            //var temp = vehicle.Mfg;
            inventory.Add(vehicle);
            vehicle = VehicleFactory.CreateVehicle("vinnumber1", VehicleMake.BMW, "model", 2016, "red", 2500, 4000, 100);
            inventory.Add(vehicle);
            var v1 = inventory.List(SortOrder.Vin);

            Assert.AreEqual(2, v1.Count );
            Assert.AreEqual(v1[0].VinNumber, "vinnumber1");

            inventory.Remove(vehicle);
            v1 = inventory.List(SortOrder.Vin);
            Assert.AreEqual(1, v1.Count);

            inventory.Add(vehicle);

            var v2 = inventory.FindByYear(2017);
            Assert.AreEqual(1, v2.Count);

            var v3 = inventory.List(SortOrder.Vin).FindByMake(VehicleMake.BMW);
            Assert.AreEqual(1, v3.Count);
            Assert.IsTrue(v3[0].Disclaimer.Contains("Copyright BMW AG, Munich, Germany"));

            v3.AddToMileage(99);
            var v3_1 = inventory.List().FindByMake(VehicleMake.BMW);
            Assert.AreEqual(199, v3_1[0].Miles);

            var v4 = inventory.List().FindByMake(VehicleMake.Ford);
            Assert.AreEqual(0, v4.Count);

            var stringList = inventory.ListAsString(SortOrder.Vin);
            Assert.IsTrue(stringList.Contains("vinnumber3"));
            Assert.IsTrue(stringList.Contains("vinnumber1"));
            // verify that the BMW has the disclaimer
            Assert.IsTrue(stringList.Contains("Copyright BMW AG, Munich, Germany"));

            // test for tesla manufacturer info
            Assert.IsTrue(stringList.Contains("Teslaphone"));
        }

        [TestMethod]
        public void TestMethod_OilChange()
        {
            IVehicleInventory inventory = new VehicleInventory();
            var mfg = new Manufacturer("Subaru", "100 main", "800-999-9999");

            var vehicle = VehicleFactory.CreateVehicle("vinnumber3", VehicleMake.Subaru, "Impreza", 2017, "yellow", 1500, 2500, 10000);
            vehicle.LastOilChangeDate = new DateTime(2016, 10, 1);
            vehicle.LastOilChangeMiles = 1000;
            Assert.IsTrue(vehicle.IsDueForOilChange());

            vehicle.LastOilChangeMiles = 10000;
            vehicle.LastOilChangeDate = DateTime.Now;
            Assert.IsFalse(vehicle.IsDueForOilChange());


            vehicle = VehicleFactory.CreateVehicle("vinnumberVW", VehicleMake.Volkswagen, "Jetta", 2017, "yellow", 1500, 2500, 10000);
            vehicle.LastOilChangeDate = new DateTime(2016, 10, 1);
            vehicle.LastOilChangeMiles = 2100;
            Assert.IsFalse(vehicle.IsDueForOilChange());

            vehicle = VehicleFactory.CreateVehicle("vinnumbertesla", VehicleMake.Tesla, "Models", 2017, "yellow", 1500, 2500, 10000);
            Assert.IsFalse(vehicle.IsDueForOilChange());

            vehicle = VehicleFactory.CreateVehicle("vinnumbertoyota", VehicleMake.Toyota, "4runner", 2017, "yellow", 1500, 2500, 10000);
            Assert.IsTrue(vehicle.IsDueForOilChange());
            vehicle.LastOilChangeDate = new DateTime(2017, 1, 1);
            vehicle.LastOilChangeMiles = 11000;
            Assert.IsFalse(vehicle.IsDueForOilChange());

            // alternative - use separate class to check for oil change
            var dueForOilChange = OilChangeRules.IsDueForOilChange(vehicle);
            Assert.IsFalse(dueForOilChange);

        }

        [TestMethod]
        public void TestMethod_Statistics()
        {
            IVehicleInventory inventory = new VehicleInventory();
            var mfg = new Manufacturer("Toyota", "100 main", "800-999-9999");

            var vehicle = VehicleFactory.CreateVehicle("vinnumber3", VehicleMake.Ford, "model", 2017, "yellow", 1500, 10000, 10000);
            inventory.Add(vehicle);
            vehicle = VehicleFactory.CreateVehicle("vinnumber1", VehicleMake.BMW, "model", 2016, "red", 2500, 60000, 60000);
            inventory.Add(vehicle);
            vehicle = VehicleFactory.CreateVehicle("vinnumber1", VehicleMake.Toyota, "model", 2016, "red", 2500, 20000, 20000);
            inventory.Add(vehicle);

            var vehicleList = inventory.List();

            Assert.AreEqual(30000, vehicleList.AverageMSRP());
            Assert.AreEqual(10000, vehicleList.MinMSRP());
            Assert.AreEqual(60000, vehicleList.MaxMSRP());

            Assert.AreEqual(30000, vehicleList.AverageMileage());
            Assert.AreEqual(10000, vehicleList.MinMileage());
            Assert.AreEqual(60000, vehicleList.MaxMileage());

            Assert.AreEqual(3, vehicleList.CountDueForOilChange());

            //do an oil change, then re-check count
            vehicle.LastOilChangeDate = DateTime.Now;
            vehicle.LastOilChangeMiles = 20000;
            Assert.AreEqual(2, vehicleList.CountDueForOilChange());
        }

    }
}
