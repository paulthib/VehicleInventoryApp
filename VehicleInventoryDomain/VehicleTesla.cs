namespace VehicleInventoryDomain
{
    public class VehicleTesla : Vehicle
    {
        public VehicleTesla(string vinNumber, VehicleMake make, string model, int year, string color, int weight, decimal price, int miles) 
            : base(vinNumber, make, model, year, color, weight, price, miles)
        {
        }
        public override Manufacturer Mfg
        {
            get { return new Manufacturer("Tesla", "Usa", "Teslaphone"); }
        }
        public override bool IsDueForOilChange()
        {
            return false;
        }

    }
}
