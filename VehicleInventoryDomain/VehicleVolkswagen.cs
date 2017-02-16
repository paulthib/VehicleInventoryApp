namespace VehicleInventoryDomain
{
    public class VehicleVolkswagen : Vehicle
    {
        public VehicleVolkswagen(string vinNumber, string make, string model, int year, string color, int weight, decimal price, int miles) 
            : base(vinNumber, make, model, year, color, weight, price, miles)
        {
        }

        public override Manufacturer Mfg
        {
            get { return new Manufacturer("Volkswagen", "Germany", "germanphone"); }
        }

        protected override int OilChangeRecommendedMiles { get { return 8000; } }
        protected override int OilChangeRecommendedDays { get { return 210; } }
    }
}
