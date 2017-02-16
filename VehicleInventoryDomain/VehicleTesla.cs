namespace VehicleInventoryDomain
{
    public class VehicleTesla : Vehicle
    {
        public VehicleTesla(string vinNumber, string make, string model, int year, string color, int weight, decimal price, int miles, Manufacturer manufacturer) 
            : base(vinNumber, make, model, year, color, weight, price, miles, manufacturer)
        {
        }
        public override bool IsDueForOilChange()
        {
            return false;
        }

    }
}
