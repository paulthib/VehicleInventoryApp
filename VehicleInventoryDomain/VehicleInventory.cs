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

}
