﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventoryDomain
{
    // one option - keep all oil change rules in one place - preferable a database of some updateable lookup table
    public static class OilChangeRules
    {
        // Normally, this would be coded in a database or some kind of updateable lookup table
        // requires no code changes if the rules change
        private static IDictionary<string, OilChangeLimitRule> OilChangeDictionary
            = new Dictionary<string, OilChangeLimitRule>(StringComparer.OrdinalIgnoreCase)
                {
                    {"Subaru", new OilChangeLimitRule() { Miles = 7000, Days = 180  } },
                    {"Volkswagen", new OilChangeLimitRule() { Miles = 8000, Days = 210  } },
                    {"Tesla", new OilChangeLimitRule() { Miles = 0, Days = 0  } }
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