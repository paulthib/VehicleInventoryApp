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
        public string Make { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string Color { get; private set; }
        public int Weight { get; private set; }
        public decimal Price { get; private set; }
        public int Miles { get;  set; }
        public Manufacturer Manufacturer { get; private set; }

        public Vehicle (string vinNumber, string make, string model, int year, 
                        string color, int weight, decimal price,
                        int miles, Manufacturer manufacturer)
        {
            VinNumber = vinNumber;
            Make = make;
            Model = model;
            Year = year;
            Color = color;
            Weight = weight;
            Price = price;
            Miles = miles;
            Manufacturer = manufacturer;
        }

    }


    public class Manufacturer
    {
        public string Name { get; set; }
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
