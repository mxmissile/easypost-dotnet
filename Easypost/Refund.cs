using Newtonsoft.Json;

namespace Easypost
{
    public class Refund : EasyPostBase
    {
        [JsonProperty("tracking_code")]
        public string TrackingCode { get; set; }

        [JsonProperty("confirmation_number")]
        public string ConfirmationNumber { get; set; }
        
        public string Carrier { get; set; }
        public string Status { get; set; }

        [JsonProperty("shipment_id")]
        public string ShipmentId { get; set; }
    }
}