namespace VehicleInventoryDomain
{
    public class VehicleSubaru : Vehicle
    {
        public VehicleSubaru(string vinNumber, VehicleMake make, string model, int year, string color, int weight, decimal price, int miles) 
            : base(vinNumber, make, model, year, color, weight, price, miles)
        {
        }

        public override Manufacturer Mfg
        {
            get { return new Manufacturer("Subaru", "Japan", "subaruphone"); }
        }
        protected override int OilChangeRecommendedMiles { get { return 7000; } }
        protected override int OilChangeRecommendedDays { get { return 180; } }
    }
}
