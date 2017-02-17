using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VehicleInventoryDomain
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VehicleMake
    {
        BMW,
        Toyota,
        Tesla,
        Volkswagen,
        Subaru,
        Ford
    }
}
