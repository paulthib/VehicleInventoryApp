using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventoryDomain
{
    public class VehicleInventory : IEnumerable<Vehicle>, IVehicleInventory
    {
        private List<Vehicle> vehicles = new List<Vehicle>();

        public void Add(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
        }

        public IEnumerator<Vehicle> GetEnumerator()
        {
            return vehicles.GetEnumerator();
        }

        public List<Vehicle> List(SortOrder sortOrder)
        {
            return vehicles.OrderBy(v => v.VinNumber).ToList();
        }

        public void Remove(Vehicle vehicle)
        {
            vehicles.Remove(vehicle);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return vehicles.GetEnumerator();
        }
    }

    public static class VehicleLookupQueries
    {
        public static List<Vehicle> FindByYear(this IEnumerable<Vehicle> source, int year )
        {
            return source.Where(v => v.Year == year).ToList();
        }
        public static List<Vehicle> FindByMake(this IEnumerable<Vehicle> source, string make)
        {
            return source.Where(v => v.Make == make).ToList();
        }
        public static void AddToMileage(this IEnumerable<Vehicle> source, int miles)
        {
            source.All(v => { v.Miles = v.Miles + miles; return true; });
        }
    }
}
