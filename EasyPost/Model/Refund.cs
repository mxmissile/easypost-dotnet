using Newtonsoft.Json;

namespace EasyPost.Model
{
    /// <summary>
    /// USPS labels and postage can be refunded if requested within 10 days for domestic and international mail. 
    /// To qualify, the item must not have been scanned by the USPS and must be requested within 10 days of printing.
    /// UPS and FedEx shipments are paid for when they're processed, not when the label is generated, so refund requests for unused labels optional.
    /// </summary>
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