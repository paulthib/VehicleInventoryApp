namespace VehicleInventoryDomain
{
    public class VehicleSubaru : Vehicle
    {
        public VehicleSubaru(string vinNumber, string make, string model, int year, string color, int weight, decimal price, int miles, Manufacturer manufacturer) 
            : base(vinNumber, make, model, year, color, weight, price, miles, manufacturer)
        {
        }

        protected override int OilChangeRecommendedMiles { get { return 7000; } }
        protected override int OilChangeRecommendedDays { get { return 180; } }
    }
}
