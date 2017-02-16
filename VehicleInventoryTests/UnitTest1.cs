using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleInventoryDomain;

namespace VehicleInventoryTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IVehicleInventory inventory = new VehicleInventory();
            var mfg = new Manufacturer("Toyota", "100 main", "800-999-9999");

            var vehicle = new Vehicle("vinnumber3", "make", "model", 2017, "yellow", 1500, 2500, 100, mfg);
            inventory.Add(vehicle);
            vehicle = new Vehicle("vinnumber1", "BMW", "model", 2016, "red", 2500, 4000, 100, mfg);
            inventory.Add(vehicle);
            var v1 = inventory.List(SortOrder.Vin);

            Assert.AreEqual(2, v1.Count );
            Assert.AreEqual(v1[0].VinNumber, "vinnumber1");

            inventory.Remove(vehicle);
            v1 = inventory.List(SortOrder.Vin);
            Assert.AreEqual(1, v1.Count);

            inventory.Add(vehicle);

            var v2 = inventory.List(SortOrder.Vin).FindByYear(2017);
            Assert.AreEqual(1, v2.Count);

            var v3 = inventory.List(SortOrder.Vin).FindByMake("BMW");
            Assert.AreEqual(1, v3.Count);

            v3.AddToMileage(99);
            var v3_1 = inventory.List().FindByMake("BMW");
            Assert.AreEqual(199, v3_1[0].Miles);

            var v4 = inventory.List().FindByMake("make_none");
            Assert.AreEqual(0, v4.Count);

            var stringList = inventory.ListAsString(SortOrder.Vin);
            Assert.IsTrue(stringList.Contains("vinnumber3"));
            Assert.IsTrue(stringList.Contains("vinnumber1"));
            Assert.IsTrue(stringList.Contains("Germany"));
        }

        [TestMethod]
        public void TestMethod_OilChange()
        {
            IVehicleInventory inventory = new VehicleInventory();
            var mfg = new Manufacturer("Subaru", "100 main", "800-999-9999");

            var vehicle = new Vehicle("vinnumber3", "Subaru", "Impreza", 2017, "yellow", 1500, 2500, 10000, mfg);
            vehicle.LastOilChangeDate = new DateTime(2016, 10, 1);
            vehicle.LastOilChangeMiles = 1000;
            var dueForOilChange = OilChangeRules.IsDueForOilChange(vehicle);
            Assert.IsTrue(dueForOilChange);

            vehicle.LastOilChangeMiles = 10000;
            vehicle.LastOilChangeDate = DateTime.Now;
            dueForOilChange = OilChangeRules.IsDueForOilChange(vehicle);
            Assert.IsFalse(dueForOilChange);


            vehicle = new Vehicle("vinnumberVW", "volkswagen", "Jetta", 2017, "yellow", 1500, 2500, 10000, mfg);
            vehicle.LastOilChangeDate = new DateTime(2016, 10, 1);
            vehicle.LastOilChangeMiles = 2100;

            dueForOilChange = OilChangeRules.IsDueForOilChange(vehicle);
            Assert.IsFalse(dueForOilChange);

            vehicle = new Vehicle("vinnumbertesla", "tesla", "Models", 2017, "yellow", 1500, 2500, 10000, mfg);
            vehicle.LastOilChangeDate = new DateTime(2016, 10, 1);
            vehicle.LastOilChangeMiles = 1000;

            dueForOilChange = OilChangeRules.IsDueForOilChange(vehicle);
            Assert.IsFalse(dueForOilChange);

            vehicle = new Vehicle("vinnumbertoyota", "toyota", "4runner", 2017, "yellow", 1500, 2500, 10000, mfg);
            vehicle.LastOilChangeDate = new DateTime(2016, 10, 1);
            vehicle.LastOilChangeMiles = 1000;

            dueForOilChange = OilChangeRules.IsDueForOilChange(vehicle);
            Assert.IsTrue(dueForOilChange);
        }

        [TestMethod]
        public void TestMethod_Statistics()
        {
            IVehicleInventory inventory = new VehicleInventory();
            var mfg = new Manufacturer("Toyota", "100 main", "800-999-9999");

            var vehicle = new Vehicle("vinnumber3", "make", "model", 2017, "yellow", 1500, 10000, 10000, mfg);
            inventory.Add(vehicle);
            vehicle = new Vehicle("vinnumber1", "BMW", "model", 2016, "red", 2500, 60000, 60000, mfg);
            inventory.Add(vehicle);
            vehicle = new Vehicle("vinnumber1", "toyota", "model", 2016, "red", 2500, 20000, 20000, mfg);
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
