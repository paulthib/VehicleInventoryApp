using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventoryDomain
{
    // another option - to keep all oil change rules in one place - preferably a database of some updateable lookup table
    public static class OilChangeRules
    {
        // Normally, this would be coded in a database or some kind of updateable lookup table
        // requires no code changes if the rules change
        private static IDictionary<VehicleMake, OilChangeLimitRule> OilChangeDictionary
            = new Dictionary<VehicleMake, OilChangeLimitRule>()
                {
                    {VehicleMake.Subaru, new OilChangeLimitRule() { Miles = 7000, Days = 180  } },
                    {VehicleMake.Volkswagen, new OilChangeLimitRule() { Miles = 8000, Days = 210  } },
                    {VehicleMake.Tesla, new OilChangeLimitRule() { Miles = 0, Days = 0  } }
                };

        private static OilChangeLimitRule defaultRule =  new OilChangeLimitRule() { Miles = 3000, Days = 90  };

        public static bool IsDueForOilChange(Vehicle vehicle)
        {
            OilChangeLimitRule oilChangeLimitRule;
            var ruleFound = OilChangeDictionary.TryGetValue(vehicle.Make, out oilChangeLimitRule);

            if (!ruleFound)
            {
                // use default
                oilChangeLimitRule = defaultRule;
            }

            // zero means no rule for this attribute
            if (oilChangeLimitRule.Days == 0 && oilChangeLimitRule.Miles == 0)
                return false;
            var daysSinceLastChange = DateTime.Now.Subtract(vehicle.LastOilChangeDate).Days;
            if (daysSinceLastChange >= oilChangeLimitRule.Days) return true;
            var milesSinceLastChange = vehicle.Miles - vehicle.LastOilChangeMiles;
            return (milesSinceLastChange >= oilChangeLimitRule.Miles);
         }
    }

    public class OilChangeLimitRule
    {
        public int Miles { get; set; }
        public int Days { get; set; }
    }
}
