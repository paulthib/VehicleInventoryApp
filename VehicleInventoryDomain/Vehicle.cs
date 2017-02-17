using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventoryDomain
{
    public class Vehicle
    {
        public string VinNumber { get; set; }
        public VehicleMake Make { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string Color { get; private set; }
        public int Weight { get; private set; }
        public decimal Price { get; private set; }
        public int Miles { get;  set; }

        //Oil Change
        public DateTime LastOilChangeDate { get; set; }
        public int LastOilChangeMiles { get; set; }

        public virtual Manufacturer Mfg
        {
            get { return new Manufacturer("not supplied", "default", "default"); }
        }
        public virtual string Disclaimer { get; private set; }
        protected virtual int OilChangeRecommendedMiles { get { return 3000; } }
        protected virtual int OilChangeRecommendedDays { get { return 90; } }

        public Vehicle (string vinNumber, VehicleMake make, string model, int year, 
                        string color, int weight, decimal price,
                        int miles)
        {
            VinNumber = vinNumber;
            Make = make;
            Model = model;
            Year = year;
            Color = color;
            Weight = weight;
            Price = price;
            Miles = miles;
        }


        //oil change - option - build rules into the class, but this requires code change if the rule changes
        public virtual bool IsDueForOilChange()
        {
            // zero means no rule for this attribute
            if (OilChangeRecommendedDays == 0 && OilChangeRecommendedMiles == 0)
                return false;
            var daysSinceLastChange = DateTime.Now.Subtract(LastOilChangeDate).Days;
            if (daysSinceLastChange >= OilChangeRecommendedDays) return true;
            var milesSinceLastChange = Miles - LastOilChangeMiles;
            return (milesSinceLastChange >= OilChangeRecommendedMiles);
        }
    }


    public class Manufacturer
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }

        public Manufacturer(string name, string address, string phone)
        {
            Name = name;
            Address = address;
            Phone = phone;
        }

    }

}
