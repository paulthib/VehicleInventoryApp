namespace VehicleInventoryDomain
{
    public static class VehicleFactory
    {
        public static Vehicle CreateVehicle(string vinNumber, VehicleMake make, string model, int year,
                        string color, int weight, decimal price, int miles)
        {
            Vehicle vehicle = null;

            switch (make)
            {
                case VehicleMake.BMW:
                    vehicle = new VehicleBmw(vinNumber, make, model, year,
                         color, weight, price, miles);
                    break;
                case VehicleMake.Subaru:
                    vehicle = new VehicleSubaru(vinNumber, make, model, year,
                         color, weight, price, miles);
                    break;
                case VehicleMake.Tesla:
                    vehicle = new VehicleTesla(vinNumber, make, model, year,
                         color, weight, price, miles);
                    break;
                case VehicleMake.Volkswagen:
                    vehicle = new VehicleVolkswagen(vinNumber, make, model, year,
                         color, weight, price, miles);
                    break;
                default:
                    vehicle = new Vehicle(vinNumber, make, model, year,
                         color, weight, price, miles);
                    break;
            }
            return vehicle;
        }
    }
}
