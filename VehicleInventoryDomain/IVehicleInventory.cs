using System.Collections.Generic;

namespace VehicleInventoryDomain
{
    public interface IVehicleInventory
    {
        //IEnumerator<Vehicle> GetEnumerator();
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
        List<Vehicle> List();
        List<Vehicle> List(SortOrder sortOrder);
        string ListAsString(SortOrder sortOrder);
    }

    public enum SortOrder
    {
        Vin,
        MakeModel,
        Year
    }
}