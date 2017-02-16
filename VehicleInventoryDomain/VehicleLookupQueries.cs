using System.Collections.Generic;
using System.Linq;

namespace VehicleInventoryDomain
{
    // Extension methods to support special queries
    public static class VehicleLookupQueries
    {
        public static List<Vehicle> FindByYear(this IVehicleInventory source, int year)
        {
            return source.List().FindByYear(year);
        }
        public static List<Vehicle> FindByYear(this IEnumerable<Vehicle> source, int year)
        {
            return source.Where(v => v.Year == year).ToList();
        }
        public static List<Vehicle> FindByMake(this IVehicleInventory source, string make)
        {
            return source.List().FindByMake(make);
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
            source.Miles = source.Miles + miles;
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
            return source.Count(v => v.IsDueForOilChange() == true);
        }
    }
}
