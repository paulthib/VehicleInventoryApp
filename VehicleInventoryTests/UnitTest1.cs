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
            vehicle = new Vehicle("vinnumber1", "make_2", "model", 2016, "red", 2500, 4000, 100, mfg);
            inventory.Add(vehicle);
            var v1 = inventory.List(SortOrder.Vin);

            Assert.AreEqual(v1.Count, 2);
            Assert.AreEqual(v1[0].VinNumber, "vinnumber1");

            var v2 = inventory.List(SortOrder.Vin).FindByYear(2017);
            Assert.AreEqual(v2.Count, 1);

            var v3 = inventory.List(SortOrder.Vin).FindByMake("make_2");
            Assert.AreEqual(v3.Count, 1);

            v3.AddToMileage(99);
            var v3_1 = inventory.List(SortOrder.Vin).FindByMake("make_2");
            Assert.AreEqual(v3_1[0].Miles, 199);

            var v4 = inventory.List(SortOrder.Vin).FindByMake("make_none");
            Assert.AreEqual(v4.Count, 0);

        }
    }
}
