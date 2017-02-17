using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventoryDomain;

namespace VehicleInventoryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoVehicleInventory();
        }

        private static void DemoVehicleInventory()
        {
            IVehicleInventory inventory = new VehicleInventory();
            var vehicle = VehicleFactory.CreateVehicle("vinnumber3", VehicleMake.Tesla, "Model S", 2017, "yellow", 1500, 2500, 100);
            //var temp = vehicle.Mfg;
            inventory.Add(vehicle);
            vehicle = VehicleFactory.CreateVehicle("vinnumber1", VehicleMake.BMW, "325i", 2016, "red", 2500, 4000, 100);
            inventory.Add(vehicle);

            inventory.Remove(vehicle);

            inventory.Add(vehicle);
            var v1 = inventory.List(SortOrder.Vin);

            Console.WriteLine("List of Vehicles");
            v1.ForEach(v => Console.WriteLine($"{v.VinNumber}, {v.Make}, {v.Model}"));

            var v2 = inventory.FindByYear(2017);

            var v3 = inventory.List(SortOrder.Vin).FindByMake(VehicleMake.BMW);

            v3.AddToMileage(99);
            var v3_1 = inventory.List().FindByMake(VehicleMake.BMW);

            var v4 = inventory.List().FindByMake(VehicleMake.Ford);

            var stringList = inventory.ListAsString(SortOrder.Vin);
            Console.WriteLine("List of Vehicles - in Json(string) format");
            Console.WriteLine($"{stringList}");
        }
    }
}
