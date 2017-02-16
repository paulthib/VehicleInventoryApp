using Newtonsoft.Json;
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

        public List<Vehicle> List()
        {
            return vehicles.ToList();
        }
        public List<Vehicle> List(SortOrder sortOrder )
        {

            switch (sortOrder)
            {
                case SortOrder.Vin:
                    return vehicles.OrderBy(v => v.VinNumber).ToList();
                case SortOrder.MakeModel:
                    return vehicles.OrderBy(v => v.Make).ThenBy(v => v.Model).ToList();
                case SortOrder.Year:
                    return vehicles.OrderBy(v => v.Year).ToList();
                default:
                    return vehicles.ToList();
            }
        }
        public string ListAsString(SortOrder sortOrder  )
        {
            var list = List(sortOrder);
            var stringList = list.OfType<string>();
            string jsonObject = JsonConvert.SerializeObject(list);
            return jsonObject;
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

    // Extension methods to support special queries
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
        public static void AddToMileage(this Vehicle source, int miles)
        {
            source.Miles = source.Miles + miles ;
        }
        public static decimal AverageMSRP(this IEnumerable<Vehicle> source)
        {
            return source.Average(v => v.Price);
        }
        public static decimal MaxMSRP(this IEnumerable<Vehicle> source)
        {
            return source.Max(v => v.Price);
        }
        public static decimal MinMSRP(this IEnumerable<Vehicle> source)
        {
            return source.Min(v => v.Price);
        }
        public static double AverageMileage(this IEnumerable<Vehicle> source)
        {
            return source.Average(v => v.Miles);
        }
        public static double MaxMileage(this IEnumerable<Vehicle> source)
        {
            return source.Max(v => v.Miles);
        }
        public static double MinMileage(this IEnumerable<Vehicle> source)
        {
            return source.Min(v => v.Miles);
        }
        public static int CountDueForOilChange(this IEnumerable<Vehicle> source)
        {
            return source.Count(v => OilChangeRules.IsDueForOilChange(v) == true);
        }
    }
}
