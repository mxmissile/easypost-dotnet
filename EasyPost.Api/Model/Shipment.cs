using System.Collections.Generic;
using System.Net.Http;
using Easypost.Internal;
using Newtonsoft.Json;

namespace EasyPost.Model
{
    /// <summary>
    /// Shipments are made up of "to" and "from" addresses, the parcel being shipped, and any customs forms required for international deliveries. 
    /// Once created a Shipment object can be used to retrieve shipping rates and purchase a postage label.
    /// Shipments created with a valid to address, from address, and parcel will automatically retrieve available shipping rate options in the "rates" attribute.
    /// </summary>
    public class Shipment : EasyPostBase, IEncodable
    {
        public Shipment()
        {
            Options = new Dictionary<string, string>();
        }

        [JsonProperty("to_address")]
        public Address ToAddress { get; set; }

        [JsonProperty("from_address")]
        public Address FromAddress { get; set; }

        public Parcel Parcel { get; set; }

        [JsonProperty("customs_info")]
        public CustomsInfo CustomsInfo { get; set; }

        [JsonProperty("scan_form")]
        public ScanForm ScanForm { get; set; }

        public string Reference { get; set; }
        
        public Dictionary<string, string> Options { get; set; }

        // everything below here is not posted, only retreived

        public List<CarrierRate> Rates { get; set; }

        [JsonProperty("selected_rate")]
        public CarrierRate SelectedRate { get; set; }

        [JsonProperty("postage_label")]
        public PostageLabel PostageLabel { get; set; }

        public string Mode { get; set; }

        public double? Insurance { get; set; }

        [JsonProperty("batch_status")]
        public string BatchStatus { get; set; }

        [JsonProperty("batch_message")]
        public string BatchMessage { get; set; }

        [JsonProperty("refund_status")]
        public string RefundStatus { get; set; }

        [JsonProperty("tracking_code")]
        public string TrackingCode { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder().AddShipment("shipment", this);
            return collection.AsFormUrlEncodedContent();
        }
    }
}