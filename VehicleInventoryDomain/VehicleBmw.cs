using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventoryDomain
{
    public class VehicleBmw : Vehicle
    {
        public VehicleBmw(string vinNumber, string make, string model, int year, string color, int weight, decimal price, int miles, Manufacturer manufacturer) 
            : base(vinNumber, make, model, year, color, weight, price, miles, manufacturer)
        {
        }

        public override string Disclaimer {
            get
            {
                return @"© Copyright BMW AG, Munich, Germany";
            }
        }
    }
}
