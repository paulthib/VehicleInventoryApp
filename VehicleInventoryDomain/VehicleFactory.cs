namespace VehicleInventoryDomain
{
    public static class VehicleFactory
    {
        public static Vehicle CreateVehicle(string vinNumber, string make, string model, int year,
                        string color, int weight, decimal price, int miles, Manufacturer manufacturer)
        {
            Vehicle vehicle = null;

            switch (make.ToLower())
            {
                case "bmw":
                    vehicle = new VehicleBmw(vinNumber, make, model, year,
                         color, weight, price, miles, manufacturer);
                    break;
                case "subaru":
                    vehicle = new VehicleSubaru(vinNumber, make, model, year,
                         color, weight, price, miles, manufacturer);
                    break;
                case "tesla":
                    vehicle = new VehicleTesla(vinNumber, make, model, year,
                         color, weight, price, miles, manufacturer);
                    break;
                case "volkswagen":
                    vehicle = new VehicleVolkswagen(vinNumber, make, model, year,
                         color, weight, price, miles, manufacturer);
                    break;
                default:
                    vehicle = new Vehicle(vinNumber, make, model, year,
                         color, weight, price, miles, manufacturer);
                    break;
            }
            return vehicle;
        }
    }
}
